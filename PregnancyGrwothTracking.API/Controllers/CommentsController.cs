using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrwothTracking.API.Controllers
{
    [Authorize] //  Bắt buộc đăng nhập
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ICommentLikeService _commentLikeService;


        public CommentsController(ICommentService commentService, ICommentLikeService commentLikeService)
        {
            _commentService = commentService;
            _commentLikeService = commentLikeService;

        }


        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto request)
        {
            try
            {
                //  Lấy UserId từ JWT Claims
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(new { message = "Invalid User ID from token" });
                }

                var createdComment = await _commentService.CreateCommentAsync(request, userId);

                return Ok(new
                {
                    CommentId = createdComment.CommentId,
                    PostId = createdComment.PostId,
                    UserId = createdComment.UserId,
                    Comment = createdComment.Comment,
                    CreatedDate = createdComment.CreatedDate
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", error = ex.Message });
            }
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
        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentDto request)
        {
            try
            {
                //  Lấy UserId từ JWT Token
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

                if (userId == 0)
                    return Unauthorized(new { message = "Bạn chưa đăng nhập!" });

                var result = await _commentService.UpdateCommentAsync(commentId, userId, request);

                if (result)
                    return Ok(new { message = "Cập nhật bình luận thành công!" });

                return BadRequest(new { message = "Cập nhật thất bại." });
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
