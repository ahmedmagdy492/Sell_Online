using Sell_Online.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IMessageService
    {
        Task<bool> CreateMessage(Message message);
        List<Message> GetListOfMessagesOfChat(string chatId);
        Task<bool> MarkAsSeen(Message message);
        Message GetMessageByID(string messageId);
    }
}