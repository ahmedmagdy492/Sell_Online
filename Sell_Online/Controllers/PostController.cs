using Microsoft.AspNetCore.Authorization;
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
        private readonly NotificationService _notificationService;
        private readonly ImagesServices _imagesServices;

        public PostController(PostService postService, IConfiguration configuration, NotificationService notificationService, ImagesServices imagesServices)
        {
            _postService = postService;
            _configuration = configuration;
            _notificationService = notificationService;
            _imagesServices = imagesServices;
        }

        [Authorize]
        [HttpGet("{postId}")]
        public IActionResult GetPostByID([FromRoute]string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
                return BadRequest(new { Message = "Invalid Post ID" });

            var posts = _postService.GetPostsBy(p => p.PostID == postId, "User,PostCategory,PostImages,PostViews", 1, 10);
            if (posts == null)
                return NotFound(new { Message = "Post Not Found" });

            return Ok(new { Message = "Success", Data = posts });
        }

        [Authorize]
        [HttpGet("Trending")]
        public IActionResult GetTrendingPosts(int pageNo = 1, int pageSize = 10)
        {
            var trendingPosts = _postService.GetPostsBy(i => i.PostStatesStateID == (short)PostStateEnum.Open, "User,PostCategory,PostViews", pageNo, pageSize).Select(p => new { 
                p.PostID, 
                p.PostStatesStateID,
                p.PostCategoryID,
                p.PostCategory,
                p.PostViews,
                p.UserID,
                p.Content,
                p.CreationDate,
                p.EditDate,
                p.IsEdited,
                p.SoldDate,
                p.Title,
                User = new 
                {
                    p.User?.Email,
                    p.User.DisplayName,
                    p.UserID
                }
            });

            return Ok(new { Message = "Success", Data = trendingPosts });
        }

        [Authorize]
        [HttpGet("MyPosts")]
        public IActionResult GetMyPosts(int pageNo = 1, int pageSize = 10)
        {
            var userId = User.Claims.ToList()[0].Value;
            var posts = _postService.GetPostsBy(i => i.UserID == userId, "User,PostCategory,PostViews", pageNo, pageSize).Select(p => new {
                p.PostID,
                p.PostStatesStateID,
                p.PostCategoryID,
                p.PostCategory,
                p.PostViews,
                p.UserID,
                p.Content,
                p.CreationDate,
                p.EditDate,
                p.IsEdited,
                p.SoldDate,
                p.Title,
                User = new
                {
                    p.User?.Email,
                    p.User.DisplayName,
                    p.UserID
                }
            });

            return Ok(new { Message = "Success", Data = posts });
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePost(string postID)
        {
            if (string.IsNullOrWhiteSpace(postID))
                return BadRequest(new { Message = "Invalid Post ID" });

            Post post = _postService.GetPostsBy(p => p.PostID == postID, "", 1, 10)
                .FirstOrDefault();

            if (post == null)
                return NotFound(new { Message = "Invalid Post ID" });

            var userId = User.Claims.ToList()[0].Value;
            if (userId != post.UserID)
                return Forbid();

            var deleteResult = await _postService.DeletePost(post);
            return Ok(new { Message = "Post has been deleted Successfully" });
        }

        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ValidateInput(ModelState.Values));

            var userId = User.Claims.ToList()[0].Value;

            var mapping = PostMapper.MapCreatePost(model);
            var createPost = await _postService.CreatePost(mapping, userId);
            if (!createPost)
                return BadRequest(new { Message = "Post is not created due to a problem" });

            return Created("", new { Message = "Post has been created successfully", PostID = mapping.PostID });
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

            var imagesOfPost = _postService.GetImagesOfPost(model.PostID);
            if (imagesOfPost.Count == 5)
                return BadRequest(new { Message = "You can only upload 5 images at most per post" });

            Base64Converter base64Converter = new Base64Converter();
            var imageBytes = base64Converter.ConvertFromBase64(model.Base64);
            string fullPath = System.IO.Path.Combine(_configuration["AppSettings:ImagePath"], $"{Guid.NewGuid()}.{model.ImageType}");

            System.IO.File.WriteAllBytes(fullPath, imageBytes);

            var uploadResult = await _postService.AddImages(post, PostMapper.MapPostImage(model));

            return Ok(new { Message = "Image has been uploaded successfully" });
        }

        [HttpDelete("Images/Remove")]
        [Authorize]
        public async Task<IActionResult> RemoveImage(string imageId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
                return BadRequest(new { Message = "Invalid Image ID" });

            var postImage = _postService.GetImageByID(imageId, "Post");
            if (postImage == null)
                return NotFound(new { Message = "Invalid Image ID or Not Found" });

            var userId = User.Claims.ToList()[0].Value;

            if (postImage.Post.UserID != userId)
                return Forbid("You are not allowed to remove this image");

            var postImageResult = await _postService.RemoveImageOfPost(postImage);
            if (!postImageResult)
                return BadRequest(new { Message = "Image is not removed due to a problem" });

            return Ok(new { Message = "Image has been removed successfully" });
        }


        [HttpPost("Views/Add")]
        [Authorize]
        public async Task<IActionResult> AddView(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
                return BadRequest(new { Message = "Invalid Post ID" });

            var post = _postService.GetPostsBy(i => i.PostID == postId, "PostViews", 1, 10).FirstOrDefault();
            if (post == null)
                return NotFound(new { Message = "Invalid post ID or Not Found" });

            var userId = User.Claims.ToList()[0].Value;

            if (userId == post.UserID)
                return Ok(new { Message = "No View was added" });

            var hasViewedPost = _postService.HasUserViewedPost(userId, postId);
            if (hasViewedPost != 0)
                return Ok(new { Message = "No view was added" });

            await _notificationService.CreateNotification(NotificationMapper.MapNotification(new CreateNotificationDTO
            {
                Content = $"Someone has viewed your Post {post.Title}",
                Title = $"Someone has viewed your Post",
                UserID = post.UserID
            }));

            var addView = await _postService.ViewPost(post, userId);
            return Ok(new { Message = "View is Added", Views = hasViewedPost+1 });
        }

        [Authorize]
        [HttpGet("Images")]
        public IActionResult GetImagesOfPost(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
                return BadRequest(new { Message = "Invalid Post ID" });

            var postImages = _imagesServices.GetImagesByPostID(postId);
            return Ok(new { Message = "Success", Data = postImages });
        }
    }
}
