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
        public DateTime? SentDate { get; set; }
        public string ChatID { get; set; }
    }
}
