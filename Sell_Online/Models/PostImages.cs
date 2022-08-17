using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class PostImages
    {

        [Key]
        public string ID { get; set; }
        public string ImageURL { get; set; }
        public string PostID { get; set; }

        public virtual Post Post { get; set; }
    }
}
