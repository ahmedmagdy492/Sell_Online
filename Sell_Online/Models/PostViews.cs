using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class PostViews
    {
        [Key]
        public int PostViewID { get; set; }
        public string ViewerID { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}
