using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IPostService
    {
        List<Post> GetTrendingPosts(int pageNo, int pageSize);
        List<Post> SearchPosts(string query, int pageNo, int pageSize);
        Post GetPostById(string postId);
        Post GetPostByIdWithInclude(string postId);
        List<Post> GetPostsByUserID(string userId, int pageNo, int pageSize);
        Task<bool> CreatePost(Post post, string userId);
        Task<bool> DeletePost(Post post);
        List<Post> GetPostsBy(Func<Post, bool> condition, string include, int pageNo, int pageSize);
        Task<bool> UpdatePost(Post post);
        Task<bool> UpdateStatus(Post post, short? stateId);
    }
}