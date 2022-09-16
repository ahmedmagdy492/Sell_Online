using Microsoft.EntityFrameworkCore;
using Sell_Online.Data;
using Sell_Online.IServices;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Services
{
    public class ImageService : IImageService
    {
        private readonly AppDBContext _context;

        public ImageService(AppDBContext context)
        {
            _context = context;
        }

        public List<PostImages> GetImagesOfPost(string postId)
        {
            return _context.PostImages.Where(i => i.PostID == postId).ToList();
        }

        public PostImages GetImageByID(string imageId, string include)
        {
            if (string.IsNullOrWhiteSpace(include))
                return _context.PostImages.FirstOrDefault(i => i.ID == imageId);
            var result = _context.PostImages.Include(include).FirstOrDefault(i => i.ID == imageId);

            if (result != null && result.Post != null)
            {
                result.Post.PostImages = null;
            }

            return result;
        }

        public async Task<bool> RemoveImageOfPost(PostImages postImages)
        {
            _context.PostImages.Remove(postImages);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddImages(PostImages postImage)
        {
            _context.PostImages.Add(postImage);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
