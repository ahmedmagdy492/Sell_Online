using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class UserUpvotes
    {
        public UserUpvotes()
        {
            ID = Guid.NewGuid().ToString();
        }

        public string ID { get; set; }
        public string UserUpvotedID { get; set; }
        public string UserUpvoterID { get; set; }

        public User UserUpvoted { get; set; }
        public User UserUpvoter { get; set; }
    }
}
