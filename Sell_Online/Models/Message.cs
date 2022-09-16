using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class Message
    {
        public Message()
        {
            SentDate = DateTime.Now;
        }

        [Key]
        public string ID { get; set; }
        public string Content { get; set; }
        public DateTime? SentDate { get; set; }
        public bool? Seen { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
