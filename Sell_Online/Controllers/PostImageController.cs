using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sell_Online.DTO;
using Sell_Online.Filters;
using Sell_Online.Helpers;
using Sell_Online.IServices;
using Sell_Online.Mappers;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Posts/")]
    [ExecptionCatcherFilter]
    public class PostImageController : ControllerBase
    {
        private readonly IImageService _imagesServices;
        private readonly IConfiguration _configuration;

        public PostImageController(IImageService imagesServices, IConfiguration configuration)
        {
            _imagesServices = imagesServices;
            _configuration = configuration;
        }

        [HttpPost("Images/Upload")]
        [Authorize]
        public async Task<IActionResult> UploadImages(UploadImageDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationHelper.ExtractErrMsgs(ModelState.Values));

            var imagesOfPost = _imagesServices.GetImagesOfPost(model.PostID);
            if (imagesOfPost.Count == 5)
                return BadRequest(new { Message = "You can only upload 5 images at most per post" });

            Base64Converter base64Converter = new Base64Converter();
            var imageBytes = base64Converter.ConvertFromBase64(model.Base64);

            string fileName = Guid.NewGuid().ToString();

            model.Base64 = $"{fileName}.{model.ImageType}";
            var uploadResult = await _imagesServices.AddImages(PostMapper.MapPostImage(model));
            if (!uploadResult)
                return BadRequest(new { Message = "Image is not uploaded due to a problem" });

            var fileSaver = new FileSaver(_configuration);
            fileSaver.SaveFile(fileName, imageBytes, model.ImageType);

            return Ok(new { Message = "Image has been uploaded successfully" });
        }

        [HttpDelete("Images/Remove")]
        [Authorize]
        public async Task<IActionResult> RemoveImage(string imageId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
                return BadRequest(new { Message = "Invalid Image ID" });

            var postImage = _imagesServices.GetImageByID(imageId, "Post");
            if (postImage == null)
                return NotFound(new { Message = "Invalid Image ID or Not Found" });

            var userId = User.Claims.ToList()[0].Value;

            if (postImage.Post.UserID != userId)
                return Forbid("You are not allowed to remove this image");

            var postImageResult = await _imagesServices.RemoveImageOfPost(postImage);
            if (!postImageResult)
                return BadRequest(new { Message = "Image is not removed due to a problem" });

            return Ok(new { Message = "Image has been removed successfully" });
        }

        [Authorize]
        [HttpGet("Images")]
        public IActionResult GetImagesOfPost(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
                return BadRequest(new { Message = "Invalid Post ID" });

            var postImages = _imagesServices.GetImagesOfPost(postId);
            var fileReader = new FileReader(_configuration);
            var base64Converter = new Base64Converter();

            foreach(var image in postImages)
            {
                try
                {
                    image.ImageURL = base64Converter.ConvertToBase64(fileReader.ReadFile(image.ImageURL));
                }
                catch(Exception)
                {
                    image.ImageURL = null;
                }
            }

            return Ok(new { Message = "Success", Data = postImages });
        }
    }
}
