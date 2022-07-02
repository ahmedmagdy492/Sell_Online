﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sell_Online.DTO;
using Sell_Online.Filters;
using Sell_Online.Helpers;
using Sell_Online.Mappers;
using Sell_Online.Models;
using Sell_Online.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Posts/")]
    [ExecptionCatcherFilter]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly IConfiguration _configuration;

        public PostController(PostService postService, IConfiguration configuration)
        {
            _postService = postService;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("Trending")]
        public IActionResult GetTrendingPosts(int pageNo = 1, int pageSize = 10)
        {
            var trendingPosts = _postService.GetPostsBy(i => i.PostStatesStateID == (short)PostStateEnum.Open, "User,PostCategory,PostImages,PostViews", pageNo, pageSize);

            return Ok(new { Message = "Success", Data = trendingPosts });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ValidateInput(ModelState.Values));

            var userId = User.Claims.ToList()[0].Value;

            var createPost = await _postService.CreatePost(PostMapper.MapCreatePost(model), userId);
            if (!createPost)
                return BadRequest(new { Message = "Post is not created due to a problem" });

            return Created("", new { Message = "Post has been created successfully" });
        }


        [HttpPatch("Status/Update")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(string postId, short? status)
        {
            if (string.IsNullOrWhiteSpace(postId))
                return BadRequest(new { Message = "Invalid Post ID" });

            if (status == null)
                return BadRequest(new { Message = "Invalid Status" });

            if (status != (short)PostStateEnum.Open && status != (short)PostStateEnum.Closed)
                return BadRequest(new { Message = "Invalid Status ID" });

            var post = _postService.GetPostsBy(i => i.PostID == postId, "", 1, 10).FirstOrDefault();

            if (post == null)
                return NotFound(new { Message = "Invalid Post ID or Not Found" });

            var changeStatusResult = await _postService.UpdateStatus(post, status);
            if (!changeStatusResult)
                return BadRequest(new { Message = "Post status is not updated due to a problem" });

            if (status == (short)PostStateEnum.Closed)
            {
                // update sold date in post id
                post.SoldDate = DateTime.Now;
                await _postService.UpdatePost(PostMapper.MapUpdatePost(post, new UpdatePostDTO
                {
                    CategoryID = post.PostCategoryID,
                    Content = post.Content,
                    Title = post.Title,
                }));
            }

            return Ok(new { Message = "Post status has been updated successfully" });
        }

        [HttpPatch("Update")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(UpdatePostDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ValidateInput(ModelState.Values));

            var post = _postService.GetPostsBy(i => i.PostID == model.PostID, "", 1, 10).FirstOrDefault();

            if (post == null)
                return NotFound(new { Message = "Invalid Post ID or Not Found" });

            var updatePost = await _postService.UpdatePost(PostMapper.MapUpdatePost(post, model));
            if (!updatePost)
                return BadRequest(new { Message = "Post is not updated due to a problem" });

            return Ok(new { Message = "Post has been updated successfully" });
        }

        [HttpPost("Images/Upload")]
        [Authorize]
        public async Task<IActionResult> UploadImages(UploadImageDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ValidateInput(ModelState.Values));

            var post = _postService.GetPostsBy(i => i.PostID == model.PostID, "", 1, 10).FirstOrDefault();

            if (post == null)
                return NotFound(new { Message = "Invalid Post ID or Not Found" });

            Base64Converter base64Converter = new Base64Converter();
            var imageBytes = base64Converter.ConvertFromBase64(model.Base64);
            string fullPath = System.IO.Path.Combine(_configuration["AppSettings:ImagePath"], $"{Guid.NewGuid()}.{model.ImageType}");

            System.IO.File.WriteAllBytes(fullPath, imageBytes);

            var uploadResult = await _postService.AddImages(post, PostMapper.MapPostImage(model));

            return Ok(new { Message = "Image has been uploaded successfully" });
        }
    }
}
