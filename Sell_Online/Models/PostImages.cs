using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class PostImages
    {
        public PostImages()
        {
            ID = Guid.NewGuid().ToString();
        }

        public string ID { get; set; }
        public string ImageURL { get; set; }
        public string PostID { get; set; }

        public Post Post { get; set; }
    }
}
