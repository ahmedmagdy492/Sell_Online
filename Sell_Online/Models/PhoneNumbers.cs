using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class PhoneNumbers
    {
        public string ID { get; set; }
        public string PhoneNumber { get; set; }
        public string UserID { get; set; }

        public virtual User User { get; set; }
    }
}
