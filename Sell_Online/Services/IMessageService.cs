using Sell_Online.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell_Online.Services
{
    public interface IMessageService
    {
        Task<bool> CreateMessage(Message message);
        List<Message> GetMessagesByChatID(string chatId);
    }
}