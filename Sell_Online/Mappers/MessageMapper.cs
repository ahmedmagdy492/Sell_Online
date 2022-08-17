using Sell_Online.DTO;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Mappers
{
    public static class MessageMapper
    {
        public static Message MapMessage(CreateMessageDTO model)
        {
            return new Message
            {
                Content = model.Content,
                ChatID = model.ChatID,
                Seen = false,
                ID = Guid.NewGuid().ToString(),
                SentDate = DateTime.Now
            };
        }
    }
}
