using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sell_Online.DTO;
using Sell_Online.Filters;
using Sell_Online.Helpers;
using Sell_Online.IServices;
using Sell_Online.Mappers;
using Sell_Online.Models;
using Sell_Online.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Posts/")]
    [ExecptionCatcherFilter]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IConfiguration _configuration;
        private readonly INotificationService _notificationService;
        private readonly IViewsService _viewsService;
        private readonly IUserService _userService;

        public PostController(IPostService postService, IConfiguration configuration, INotificationService notificationService, IViewsService viewsService, IUserService userService)
        {
            _postService = postService;
            _configuration = configuration;
            _notificationService = notificationService;
            _viewsService = viewsService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("{postId}")]
        public IActionResult GetPostByID([FromRoute]string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
                return BadRequest(new { Message = "Invalid Post ID" });

            var post = _postService.GetPostByIdWithInclude(postId);
            if (post == null)
                return NotFound(new { Message = "Post Not Found" });

            var postImages = post.PostImages ?? new List<PostImages>();
            var fileReader = new FileReader(_configuration);
            var base64Converter = new Base64Converter();

            foreach (var image in postImages)
            {
                try
                {
                    image.ImageURL = base64Converter.ConvertToBase64(fileReader.ReadFile(image.ImageURL));
                }
                catch (Exception)
                {
                    image.ImageURL = null;
                }
            }

            return Ok(new { Message = "Success", Data = new List<Post> { post } });
        }

        // searching by either content or title
        [Authorize]
        [HttpGet("Search")]
        public IActionResult Search(string query, int pageNo = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { Message = "Invalid Search query" });

            if (query.Length <= 2)
                return BadRequest(new { Message = "Please Provide at least 3 characters in search query" });

            var posts = _postService.SearchPosts(query, pageNo, pageSize);

            return Ok(new { Message = "Success", Data = posts });
        }

        [Authorize]
        [HttpGet("Trending")]
        public IActionResult GetTrendingPosts(int pageNo = 1, int pageSize = 10)
        {
            var trendingPosts = _postService.GetTrendingPosts(pageNo, pageSize);

            return Ok(new { Message = "Success", Data = trendingPosts });
        }

        [Authorize]
        [HttpGet("MyPosts")]
        public IActionResult GetMyPosts(int pageNo = 1, int pageSize = 10)
        {
            var userId = User.Claims.ToList()[0].Value;
            var posts = _postService.GetPostsByUserID(userId, pageNo, pageSize);

            return Ok(new { Message = "Success", Data = posts });
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePost(string postID)
        {
            if (string.IsNullOrWhiteSpace(postID))
                return BadRequest(new { Message = "Invalid Post ID" });

            Post post = _postService.GetPostById(postID);

            if (post == null)
                return NotFound(new { Message = "Invalid Post ID" });

            var userId = User.Claims.ToList()[0].Value;
            if (userId != post.UserID)
                return Forbid();

            var deleteResult = await _postService.DeletePost(post);
            if (!deleteResult)
                return BadRequest(new { Message = "Post is not deleted due to a problem" });

            return Ok(new { Message = "Post has been deleted Successfully" });
        }

        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ExtractErrMsgs(ModelState.Values));

            var userId = User.Claims.ToList()[0].Value;
            var user = _userService.GetUserBy(u => u.UserID == userId).FirstOrDefault();
            if (user.IsVerified == false)
                return BadRequest(new { Message = "Please Verify your Email First; Post is not Created" });

            var mappedPostObject = PostMapper.MapCreatePost(model);
            var createPost = await _postService.CreatePost(mappedPostObject, userId);
            if (!createPost)
                return BadRequest(new { Message = "Post is not created due to a problem" });

            return Created("", new { Message = "Post has been created successfully", PostID = mappedPostObject.PostID });
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

            var post = _postService.GetPostById(postId);

            if (post == null)
                return NotFound(new { Message = "Invalid Post ID or Not Found" });

            if (status == (short)PostStateEnum.Closed)
            {
                // update sold date in post
                post.SoldDate = DateTime.Now;
            }

            var changeStatusResult = await _postService.UpdateStatus(post, status);
            if (!changeStatusResult)
                return BadRequest(new { Message = "Post status is not updated due to a problem" });

            return Ok(new { Message = "Post status has been updated successfully" });
        }

        [HttpPatch("Update")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(UpdatePostDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ExtractErrMsgs(ModelState.Values));

            var post = _postService.GetPostById(model.PostID);

            if (post == null)
                return NotFound(new { Message = "Invalid Post ID or Not Found" });

            var updatePost = await _postService.UpdatePost(PostMapper.MapUpdatePost(post, model));
            if (!updatePost)
                return BadRequest(new { Message = "Post is not updated due to a problem" });

            return Ok(new { Message = "Post has been updated successfully" });
        }


        [HttpPost("Views/Add")]
        [Authorize]
        public async Task<IActionResult> AddView(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
                return BadRequest(new { Message = "Invalid Post ID" });

            var post = _postService.GetPostById(postId);
            if (post == null)
                return NotFound(new { Message = "Invalid post ID or Not Found" });

            var userId = User.Claims.ToList()[0].Value;

            if (userId == post.UserID)
                return Ok(new { Message = "No View was added" });

            var hasViewedPost = _viewsService.HasUserViewedPost(userId, postId);
            if (hasViewedPost != 0)
                return Ok(new { Message = "No view was added" });

            var addView = await _viewsService.ViewPost(post.PostID, userId);
            if (!addView)
                return BadRequest(new { Message = "No view was added due to a problem" });

            await _notificationService.CreateNotification(NotificationMapper.MapNotification(new CreateNotificationDTO
            {
                Content = $"Someone has viewed your Post {post.Title}",
                Title = $"Someone has viewed your Post",
                UserID = post.UserID
            }));
            
            return Ok(new { Message = "View is Added", isAdded = addView });
        }
    }
}
