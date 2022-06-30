using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class Chat
    {
        public string ChatID { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }

        public User Sender { get; set; }
        public User Reciever { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
