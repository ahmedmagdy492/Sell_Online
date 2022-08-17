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

        public virtual User Sender { get; set; }
        public virtual User Reciever { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public object GetChatOutModel()
        {
            return new
            {
                ChatID,
                SenderID,
                ReceiverID,
                SenderName = Sender?.DisplayName,
                ReceiverName = Reciever?.DisplayName
            };
        }
    }
}
