using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class PostCategory
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
