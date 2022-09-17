using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class Chat
    {
        public Chat()
        {
            Date = DateTime.Now;
        }

        [Key]
        public string ChatID { get; set; }
        public string Title { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public DateTime? Date { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
