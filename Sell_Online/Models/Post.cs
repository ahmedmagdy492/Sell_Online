using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class Post
    {
        public Post()
        {
            PostID = Guid.NewGuid().ToString();
        }

        public string PostID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? IsEdited { get; set; }
        public DateTime? EditDate { get; set; }
        public short? StateID { get; set; }
        public DateTime? SoldDate { get; set; }

        public PostStates PostStates { get; set; }
        public ICollection<PostImages> PostImages { get; set; }
        public ICollection<PostViews> PostViews { get; set; }
    }
}
