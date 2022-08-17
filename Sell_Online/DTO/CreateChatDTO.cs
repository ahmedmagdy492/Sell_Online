using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.DTO
{
    public class CreateChatDTO
    {
        public CreateChatDTO()
        {
            ChatID = Guid.NewGuid().ToString();
        }

        public string ChatID { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
    }
}
