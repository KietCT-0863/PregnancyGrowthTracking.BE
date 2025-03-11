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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PregnancyGrowthTracking.API.Controllers
{
    [Route("api/user-notes")]
    [ApiController]
    public class UserNoteController : ControllerBase
    {
        private readonly IUserNoteService _userNoteService;
        private readonly IConfiguration _configuration;
        private readonly PregnancyGrowthTrackingDbContext _context;

        public UserNoteController(
            IUserNoteService userNoteService,
            IConfiguration configuration,
            PregnancyGrowthTrackingDbContext context)
        {
            _userNoteService = userNoteService;
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "vip")]
        public async Task<IActionResult> GetAll(int userId)
        {
            var notes = await _userNoteService.GetNotesByUserIdAsync(userId);
            if (notes == null || !notes.Any())
                return NotFound("No notes found for this user.");

            return Ok(notes);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "vip")]
        public async Task<ActionResult<UserNote>> GetById(int id)
        {
            var note = await _userNoteService.GetNoteByIdAsync(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        [HttpPost]
        [Authorize(Roles = "vip")]
        public async Task<IActionResult> Create([FromForm] CreateUserNoteDto request)
        {
            if (request == null)
                return BadRequest("Invalid request");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.File == null)
                return BadRequest("UserNotePhoto is required.");

            const long maxFileSize = 10485760; 
            if (request.File.Length > maxFileSize)
            {
                return BadRequest($"File {request.File.FileName} exceeds the maximum allowed size of 10 MB.");
            }

            
            var note = new UserNote
            {
                UserId = request.UserId,
                Diagnosis = request.Diagnosis,
                Note = request.Note,
                Detail = request.Detail,
                Date = request.Date,
               
            };
          
            if (request.File.Length > 0)
            {
                var photoUrl = await UploadPhotoToS3(request.File);
                note.UserNotePhoto = photoUrl; 
            }
           
            await _userNoteService.AddNoteAsync(note);
            return CreatedAtAction(nameof(GetById), new { id = note.NoteId }, note);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "vip")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateUserNoteDto updatedNote, IFormFile? file)
        {           
            if (updatedNote == null && file == null)
                return BadRequest("No data provided for update.");

            var existingNote = await _context.UserNotes
                .Where(n => n.NoteId == id)
                .Select(n => new UserNote
                {
                    NoteId = n.NoteId,
                    UserId = n.UserId,
                    Diagnosis = n.Diagnosis,
                    Note = n.Note,
                    Detail = n.Detail,
                    Date = n.Date,
                    UserNotePhoto = n.UserNotePhoto
                })
                .FirstOrDefaultAsync();

            if (existingNote == null)
                return NotFound("Note not found.");

            if (updatedNote != null)
            {
                if (!string.IsNullOrEmpty(updatedNote.Diagnosis))
                    existingNote.Diagnosis = updatedNote.Diagnosis;

                if (!string.IsNullOrEmpty(updatedNote.Note))
                    existingNote.Note = updatedNote.Note;

                if (!string.IsNullOrEmpty(updatedNote.Detail))
                    existingNote.Detail = updatedNote.Detail;
            }

            // Upload ảnh mới lên S3 và lấy URL
            if (file != null)
            {
                const long maxFileSize = 10485760; 
                if (file.Length > maxFileSize)
                {
                    return BadRequest($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
                }

                var newPhotoUrl = await UploadPhotoToS3(file);
                existingNote.UserNotePhoto = newPhotoUrl;
            }

            _context.UserNotes.Update(existingNote);
            await _context.SaveChangesAsync();

            return Ok(existingNote);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "vip")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _userNoteService.GetNoteByIdAsync(id);
            if (note == null)
                return NotFound("Note not found.");


            await _userNoteService.DeleteNoteAsync(id);
            return NoContent();
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