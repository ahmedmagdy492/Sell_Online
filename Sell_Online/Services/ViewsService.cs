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
    public class ViewsService : IViewsService
    {
        private readonly AppDBContext _context;

        public ViewsService(AppDBContext context)
        {
            _context = context;
        }

        public int HasUserViewedPost(string userId, string postId)
        {
            return _context.PostViews.Where(i => i.ViewerID == userId && i.PostID == postId).Count();
        }

        public async Task<bool> ViewPost(Post post, string viewerId)
        {
            if (post.PostViews != null)
            {
                post.PostViews.Add(new PostViews
                {
                    PostID = post.PostID,
                    ViewerID = viewerId,
                });
            }
            else
            {
                post.PostViews = new List<PostViews>();
                post.PostViews.Add(new PostViews
                {
                    PostID = post.PostID,
                    ViewerID = viewerId,
                });
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
