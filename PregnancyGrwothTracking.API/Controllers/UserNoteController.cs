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
using PregnancyGrowthTracking.DAL.DTOs;

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
        public async Task<IActionResult> Create([FromForm] CreateUserNoteDto request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            const long maxFileSize = 10485760; // 10 MB
            if (request.Files != null && request.Files.Count > 0)
            {
                foreach (var file in request.Files)
                {
                    if (file.Length > maxFileSize)
                    {
                        return BadRequest($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
                    }
                }
            }

            var note = new UserNote
            {
                UserId = request.UserId,
                Diagnosis = request.Diagnosis,
                Note = request.Note,
                Detail = request.Detail,
                Date = request.Date,
                UserNotePhoto = null
            };

            if (request.Files != null && request.Files.Count > 0)
            {
                var photoUrls = new List<string>();
                foreach (var file in request.Files)
                {
                    if (file.Length > 0)
                    {
                        var photoUrl = await UploadPhotoToS3(file);
                        photoUrls.Add(photoUrl);
                    }
                }
                note.UserNotePhoto = string.Join(",", photoUrls); // Lưu các URL ảnh dưới dạng chuỗi phân cách bằng dấu phẩy
            }

            await _userNoteService.AddNoteAsync(note);
            return CreatedAtAction(nameof(GetById), new { id = note.NoteId }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserNote updatedNote)
        {
            if (updatedNote == null)
                return BadRequest("Invalid note data.");

            var existingNote = await _userNoteService.GetNoteByIdAsync(id);
            if (existingNote == null)
                return NotFound("Note not found.");

           
            existingNote.Diagnosis = updatedNote.Diagnosis ?? existingNote.Diagnosis;
            existingNote.Note = updatedNote.Note ?? existingNote.Note;
            existingNote.Detail = updatedNote.Detail ?? existingNote.Detail;
            existingNote.UserNotePhoto = updatedNote.UserNotePhoto ?? existingNote.UserNotePhoto;

            await _userNoteService.UpdateNoteAsync(existingNote); // Gọi phương thức cập nhật
            return Ok(existingNote);
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
    }
}