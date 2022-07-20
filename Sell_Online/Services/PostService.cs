using Microsoft.EntityFrameworkCore;
using Sell_Online.Data;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Services
{
    public class PostService
    {
        private readonly AppDBContext _context;

        public PostService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePost(Post post, string userId)
        {
            post.UserID = userId;
            _context.Posts.Add(post);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// this method will get the latest by creation date posts
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="include"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Post> GetPostsBy(Func<Post, bool> condition, string include, int pageNo, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(include))
                return _context.Posts.Where(condition).ToList();

            var posts = _context.Posts;
            IQueryable<Post> postsWithInclude = posts;

            if (include.Contains(','))
            {
                string[] includes = include.Split(',');
                foreach(string s in includes)
                {
                    postsWithInclude = postsWithInclude.Include(s);
                }
            }
            else
            {
                postsWithInclude = postsWithInclude.Include(include);
            }

            var result = postsWithInclude.OrderByDescending(i => i.CreationDate).Where(condition).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            result.ForEach(i =>
            {
                if (i.PostViews != null)
                {
                    i.PostViews.ToList().ForEach(v =>
                    {
                        v.Post = null;
                        v.User = null;
                    });
                }

                if(i.User != null)
                {
                    i.User.Posts = null;
                    i.User.Notifications = null;
                    i.User.PhoneNumbers = null;
                    i.User.Chats = null;
                }

                if(i.PostImages != null)
                {
                    i.PostImages.ToList().ForEach(p =>
                    {
                        p.Post = null;
                    });
                }

                if(i.PostCategory != null)
                {
                    i.PostCategory.Posts = null;
                }
            });

            return result;
        }

        public async Task<bool> UpdateStatus(Post post, short? stateId)
        {
            post.PostStatesStateID = stateId;
            _context.Entry(post).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddImages(Post post, PostImages postImage)
        {
            if(post.PostImages != null)
            {
                post.PostImages.Add(postImage);
            }
            else
            {
                post.PostImages = new List<PostImages>();
                post.PostImages.Add(postImage);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public List<PostImages> GetImagesOfPost(string postId)
        {
            return _context.PostImages.Where(i => i.PostID == postId).ToList();
        }

        public PostImages GetImageByID(string imageId, string include)
        {
            if(string.IsNullOrWhiteSpace(include))
                return _context.PostImages.FirstOrDefault(i => i.ID == imageId);
            var result = _context.PostImages.Include(include).FirstOrDefault(i => i.ID == imageId);

            if(result != null && result.Post != null)
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

        public int HasUserViewedPost(string userId, string postId)
        {
            return _context.PostViews.Where(i => i.ViewerID == userId && i.PostID == postId).Count();
        }

        public async Task<bool> ViewPost(Post post, string viewerId)
        {
            if(post.PostViews != null)
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
