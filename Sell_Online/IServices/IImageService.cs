using Sell_Online.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IImageService
    {
        PostImages GetImageByID(string imageId, string include);
        List<PostImages> GetImagesOfPost(string postId);
        Task<bool> RemoveImageOfPost(PostImages postImages);
        Task<bool> AddImages(Post post, PostImages postImage);
    }
}