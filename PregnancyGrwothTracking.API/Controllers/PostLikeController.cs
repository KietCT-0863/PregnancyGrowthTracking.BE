using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Repositories;
using System;

namespace PregnancyGrwothTracking.API.Controllers
{
        [ApiController]
        [Route("api/posts")]
        public class PostLikeController : ControllerBase
        {
            private readonly IPostLikeService _postLikeService;
            private readonly IPostLikeRepository _postLikeRepository;

            public PostLikeController(IPostLikeService postLikeService, IPostLikeRepository postLikeRepository)
            {
                _postLikeService = postLikeService;
                _postLikeRepository = postLikeRepository;
            }

        [HttpPost("{postId}/toggle-like")]
        public async Task<IActionResult> ToggleLikePost(int postId)
        {
            try
            {
                if (postId <= 0)
                    return BadRequest("ID bài viết không hợp lệ");

                int likeCount = await _postLikeService.ToggleLikePostAsync(postId, User);
                return Ok(new { Success = true, Message = "Cập nhật trạng thái thích thành công", LikeCount = likeCount });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }

        [HttpGet("{postId}/likes/count")]
            public async Task<IActionResult> GetLikesCount(int postId)
            {
                try
                {
                    if (postId <= 0)
                        return BadRequest("ID bài viết không hợp lệ");

                    int count = await _postLikeService.GetLikesCountAsync(postId);
                    return Ok(new { LikeCount = count });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Lỗi: {ex.Message}");
                }
            }
        }
}
