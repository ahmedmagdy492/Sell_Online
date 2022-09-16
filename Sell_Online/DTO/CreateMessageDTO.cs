using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.DTO
{
    public class CreateMessageDTO
    {
        [Required]
        public string Content { get; set; }
        public String SentDate { get; set; }
        public string SenderID { get; set; }
        public string RecieverID { get; set; }
    }
}
