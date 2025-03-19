using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using System;
using System.Collections.Generic;
using PregnancyGrowthTracking.DAL.DTOs;

namespace PregnancyGrwothTracking.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PregnancyGrowthTracking.BLL.Services;
    using System;
    using System.Threading.Tasks;

    namespace PregnancyGrwothTracking.API.Controllers
    {
        [ApiController]
        [Route("api/posts")]
        public class PostLikeController : ControllerBase
        {
            private readonly IPostLikeService _postLikeService;

            public PostLikeController(IPostLikeService postLikeService)
            {
                _postLikeService = postLikeService;
            }

            [HttpPost("{postId}/like")]
            public async Task<IActionResult> LikePost(int postId, [FromBody] LikePostRequest request)
            {
                try
                {
                    if (postId <= 0)
                        return BadRequest("ID bài viết không hợp lệ");

                    if (request == null || request.UserId <= 0)
                        return BadRequest("ID người dùng không hợp lệ");

                    await _postLikeService.LikePostAsync(postId, request.UserId);
                    int likeCount = await _postLikeService.GetLikesCountAsync(postId);
                    return Ok(new { Success = true, Message = "Đã thích bài viết", LikeCount = likeCount });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Lỗi: {ex.Message}");
                }
            }

            [HttpPost("{postId}/unlike")]
            public async Task<IActionResult> UnlikePost(int postId, [FromBody] LikePostRequest request)
            {
                try
                {
                    if (postId <= 0)
                        return BadRequest("ID bài viết không hợp lệ");

                    if (request == null || request.UserId <= 0)
                        return BadRequest("ID người dùng không hợp lệ");

                    await _postLikeService.UnlikePostAsync(postId, request.UserId);
                    int likeCount = await _postLikeService.GetLikesCountAsync(postId);
                    return Ok(new { Success = true, Message = "Đã bỏ thích bài viết", LikeCount = likeCount });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(ex.Message);
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
}
