using Sell_Online.DTO;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Mappers
{
    public static class PostMapper
    {
        public static Post MapCreatePost(CreatePostDTO model)
        {
            return new Post
            {
                PostID = Guid.NewGuid().ToString(),
                Content = model.Content,
                Title = model.Title,
                PostCategoryID = model.CategoryID,
                PostStatesStateID = (short)PostStateEnum.Open
            };
        }

        public static Post MapUpdatePost(Post post, UpdatePostDTO model)
        {
            post.IsEdited = true;
            post.EditDate = DateTime.Now;
            post.Title = model.Title;
            post.Content = model.Content;
            post.PostCategoryID = model.CategoryID;

            return post;
        }

        public static PostImages MapPostImage(UploadImageDTO model)
        {
            return new PostImages
            {
                ID = Guid.NewGuid().ToString(),
                PostID = model.PostID,
                ImageURL = model.Base64
            };
        }
    }
}
