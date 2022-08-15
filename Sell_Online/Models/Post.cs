using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class Post
    {
        public string PostID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? IsEdited { get; set; }
        public DateTime? EditDate { get; set; }
        public short? PostStatesStateID { get; set; }
        public DateTime? SoldDate { get; set; }
        public string UserID { get; set; }
        public long? PostCategoryID { get; set; }

        public User User { get; set; }

        public PostCategory PostCategory { get; set; }
        public PostStates PostStates { get; set; }
        public ICollection<PostImages> PostImages { get; set; }
        public ICollection<PostViews> PostViews { get; set; }

        public object GetPostBasicInfo()
        {
            return new
            {
                PostID,
                PostStatesStateID,
                PostCategoryID,
                PostCategory,
                PostViews,
                UserID,
                Content,
                CreationDate,
                EditDate,
                IsEdited,
                SoldDate,
                Title,
                User = new
                {
                    User?.Email,
                    User.DisplayName,
                    UserID
                }
            };
        }
    }
}
