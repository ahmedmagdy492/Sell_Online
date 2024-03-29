﻿using Microsoft.EntityFrameworkCore;
using Sell_Online.Data;
using Sell_Online.IServices;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sell_Online.Services
{
    public class PostService : IPostService
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

        public List<Post> SearchPosts(string query, int pageNo, int pageSize)
        {
            var result = _context.Posts.Include(p => p.User).Include(p => p.PostViews).Include(p => p.PostCategory).Where(p => p.PostStatesStateID == (short)PostStateEnum.Open && (p.Title.Contains(query) || p.Content.Contains(query))).Skip((pageNo - 1) * pageSize).Take(pageSize)
            .ToList();
            foreach (var post in result)
            {
                post.User.ProfileImageURL = null;
                post.User.Posts = null;
                post.PostCategory.Posts = null;

                foreach (var view in post.PostViews)
                {
                    view.Post = null;
                    view.User = null;
                }
            }

            return result;
        }

        public List<Post> GetTrendingPosts(int pageNo, int pageSize)
        {
            var result = _context.Posts.Include(p => p.User).Include(p => p.PostViews).Include(p => p.PostCategory).Where(p => p.PostStatesStateID == (short)PostStateEnum.Open).Skip((pageNo - 1) * pageSize).Take(pageSize)
                .ToList();

            foreach (var post in result)
            {
                post.User.Posts = null;
                post.PostImages = null;
                post.PostCategory.Posts = null;

                foreach (var view in post.PostViews)
                {
                    view.Post = null;
                    view.User = null;
                }
            }

            return result;
        }

        public List<Post> GetPostsByUserID(string userId, int pageNo, int pageSize)
        {
            var result = _context.Posts.Include(p => p.User).Include(p => p.PostViews).Include(p => p.PostCategory).Where(p => p.UserID == userId).Skip((pageNo - 1) * pageSize).Take(pageSize)
                .ToList();

            foreach(var post in result)
            {
                post.User.Posts = null;
                post.User.MyViews = null;
                post.PostCategory.Posts = null;

                foreach (var view in post.PostViews)
                {
                    view.Post = null;
                    view.User = null;
                }
            }

            return result;
        }

        public Post GetPostByIdWithInclude(string postId)
        {
            var result = _context.Posts.Include(p => p.User).Include(p => p.PostCategory).Include(p => p.PostImages).Include(p => p.PostViews);

            foreach(var post in result)
            {
                post.User.Posts = null;
                post.User.MyViews = null;
                post.PostCategory.Posts = null;

                foreach(var view in post.PostViews)
                {
                    view.Post = null;
                    view.User = null;
                }

                foreach (var img in post.PostImages)
                {
                    img.Post = null;
                }
            }

            return result.FirstOrDefault(p => p.PostID == postId);
        }

        public Post GetPostById(string postId)
        {
            return _context.Posts.FirstOrDefault(p => p.PostID == postId);
        }

        /// <summary>
        /// gets the latest posts by creation date
        /// </summary>
        /// <param name="condition">condition used to filter the data source</param>
        /// <param name="include">name of the related entities seperated by comma, if there is none send empty string</param>
        /// <param name="pageNo">the number of the current page</param>
        /// <param name="pageSize">the count of record per page</param>
        /// <returns>list of posts ordered by creation date descendingly and limited by pagination</returns>
        public List<Post> GetPostsBy(Func<Post, bool> condition, string include, int pageNo, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(include))
                return _context.Posts.Where(condition).ToList();

            var posts = _context.Posts;
            IQueryable<Post> postsWithInclude = posts;

            if (include.Contains(','))
            {
                string[] includes = include.Split(',');
                foreach (string s in includes)
                {
                    postsWithInclude = postsWithInclude.Include(s);
                }
            }
            else
            {
                postsWithInclude = postsWithInclude.Include(include);
            }

            var result = posts.OrderByDescending(i => i.CreationDate).Where(condition).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
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

                if (i.User != null)
                {
                    i.User.Posts = null;
                    i.User.Notifications = null;
                    i.User.PhoneNumbers = null;
                }

                if (i.PostImages != null)
                {
                    i.PostImages.ToList().ForEach(p =>
                    {
                        p.Post = null;
                    });
                }

                if (i.PostCategory != null)
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
    }
}
