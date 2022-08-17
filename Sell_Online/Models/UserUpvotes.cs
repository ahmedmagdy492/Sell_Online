using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class UserUpvotes
    {
        public string ID { get; set; }
        public string UserUpvotedID { get; set; }
        public string UserUpvoterID { get; set; }

        public virtual User UserUpvoted { get; set; }
        public virtual User UserUpvoter { get; set; }
    }
}
