using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;

namespace PregnancyGrwothTracking.API.Controllers
{
    [Authorize] //  Bắt buộc đăng nhập
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ICommentLikeService _commentLikeService;
        private readonly IS3Service _s3Service;
        private readonly ICommentRepository _commentRepository;



        public CommentsController(
     ICommentService commentService,
     ICommentLikeService commentLikeService,
     IS3Service s3Service,
     ICommentRepository commentRepository)
        {
            _commentService = commentService;
            _commentLikeService = commentLikeService;
            _s3Service = s3Service;
            _commentRepository = commentRepository;
        }



        [HttpPost("with-image")]
        [Authorize]
        public async Task<IActionResult> CreateCommentWithImage([FromForm] CreateCommentDto request)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            if (userId == 0)
                return Unauthorized(new { message = "Bạn chưa đăng nhập!" });

            string? imageUrl = null;

            if (request.Image != null && request.Image.Length > 0)
            {
                // Lưu ảnh vào folder comments/{postId}
                imageUrl = await _s3Service.UploadFileAsync(request.Image, $"comments/{request.PostId}");
            }

            var newComment = new PostComment
            {
                PostId = request.PostId,
                UserId = userId,
                Comment = request.Comment,
                CreatedDate = DateTime.UtcNow,
                ParentCommentId = request.ParentCommentId,
                CommentImageUrl = imageUrl
            };

            await _commentRepository.AddCommentAsync(newComment);

            return Ok(new
            {
                Message = "Bình luận đã được tạo thành công",
                CommentId = newComment.CommentId,
                ImageUrl = imageUrl
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var comments = await _commentService.GetAllCommentsAsync();
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByPostIdAsync(postId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }
        [HttpPut("{commentId}/with-image")]
        public async Task<IActionResult> UpdateCommentWithImage(int commentId, [FromForm] UpdateCommentDto request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
                if (userId == 0)
                    return Unauthorized(new { message = "Bạn chưa đăng nhập!" });

                var success = await _commentService.UpdateCommentWithImageAsync(commentId, userId, request);
                if (!success)
                    return BadRequest(new { message = "Cập nhật thất bại!" });

                return Ok(new { message = "Cập nhật bình luận thành công!" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                //  Lấy UserId và Role từ JWT Token
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
                var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "User";
                bool isAdmin = role == "Admin"; //  Admin có quyền xóa tất cả bình luận

                if (userId == 0)
                    return Unauthorized(new { message = "Bạn chưa đăng nhập!" });

                var result = await _commentService.DeleteCommentAsync(commentId, userId, isAdmin);

                if (result)
                    return Ok(new { message = "Xóa bình luận thành công!" });

                return BadRequest(new { message = "Xóa bình luận thất bại." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }
        [HttpPost("{commentId}/like")]
        public async Task<IActionResult> ToggleLike(int commentId)
        {
            try
            {
                // Lấy UserId từ JWT Token
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

                if (userId == 0)
                    return Unauthorized(new { message = "Bạn chưa đăng nhập!" });

                var isLiked = await _commentLikeService.ToggleLikeAsync(commentId, userId);

                if (isLiked)
                    return Ok(new { message = "Bạn đã thích bình luận này!" });
                else
                    return Ok(new { message = "Bạn đã bỏ thích bình luận này!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
        }
    }
}
