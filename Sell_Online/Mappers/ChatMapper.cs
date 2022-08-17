using Sell_Online.DTO;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Mappers
{
    public static class ChatMapper
    {
        public static Chat Map(CreateChatDTO model)
        {
            return new Chat
            {
                ChatID = model.ChatID,
                ReceiverID = model.ReceiverID,
                SenderID = model.SenderID
            };
        }
    }
}
