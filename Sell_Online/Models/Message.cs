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
            ID = Guid.NewGuid().ToString();
            SentDate = DateTime.Now;
        }

        [Key]
        public string ID { get; set; }
        public string Content { get; set; }
        public DateTime? SentDate { get; set; }
        public string ChatID { get; set; }
        public bool? Seen { get; set; }
        public Chat Chat { get; set; }
    }
}
