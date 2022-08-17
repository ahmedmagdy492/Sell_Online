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
        public string PostID { get; set; }
        public string ViewerID { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
