using Sell_Online.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IMessageService
    {
        object GetChatsOfUser(string userId);
        Task<bool> CreateMessage(Message message);
        Task<bool> MarkAsSeen(Message message);
        Message GetMessageByID(string messageId);
        object GetListOfMessagesOfUser(string senderId, string receiverId);
    }
}