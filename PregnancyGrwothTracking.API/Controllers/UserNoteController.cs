using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System;
using Amazon;

namespace PregnancyGrowthTracking.API.Controllers
{
    [Route("api/user-notes")]
    [ApiController]
    public class UserNoteController : ControllerBase
    {
        private readonly IUserNoteService _userNoteService;
        private readonly IConfiguration _configuration;

        public UserNoteController(IUserNoteService userNoteService, IConfiguration configuration)
        {
            _userNoteService = userNoteService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserNote>>> GetAll()
        {
            return Ok(await _userNoteService.GetAllNotesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserNote>> GetById(int id)
        {
            var note = await _userNoteService.GetNoteByIdAsync(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUserNoteRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            var note = new UserNote
            {
                UserId = request.UserId,
                Detail = request.Detail,
                Date = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            if (request.File != null && request.File.Length > 0)
            {
                var photoUrl = await UploadPhotoToS3(request.File);
                note.UserNotePhoto = photoUrl;
            }

            await _userNoteService.AddNoteAsync(note);
            return CreatedAtAction(nameof(GetById), new { id = note.NoteId }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserNote note)
        {
            if (id != note.NoteId) return BadRequest("Note ID mismatch");
            await _userNoteService.UpdateNoteAsync(note);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userNoteService.DeleteNoteAsync(id);
            return NoContent();
        }

        [HttpPost("upload-photo/{noteId}")]
        public async Task<IActionResult> UploadPhoto(int noteId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            var photoUrl = await UploadPhotoToS3(file);

            // Cập nhật URL ảnh vào ghi chú
            var note = await _userNoteService.GetNoteByIdAsync(noteId);
            if (note == null) return NotFound();

            note.UserNotePhoto = photoUrl;
            await _userNoteService.UpdateNoteAsync(note);

            return Ok(new { PhotoUrl = photoUrl });
        }

        private async Task<string> UploadPhotoToS3(IFormFile file)
        {
            var bucketName = _configuration["UserNote:BucketName"];
            var accessKey = _configuration["UserNote:AccessKey"];
            var secretKey = _configuration["UserNote:SecretKey"];
            var region = _configuration["UserNote:Region"];

            using var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));

            var key = $"user-notes/{Guid.NewGuid()}-{file.FileName}";

            using var stream = file.OpenReadStream();
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = stream,
                ContentType = file.ContentType
            };

            await s3Client.PutObjectAsync(request);
            return $"https://{bucketName}.s3.amazonaws.com/{key}";
        }

        public class CreateUserNoteRequest
        {
            public int UserId { get; set; }
            public string Detail { get; set; }
            public IFormFile? File { get; set; }
        }
    }
}