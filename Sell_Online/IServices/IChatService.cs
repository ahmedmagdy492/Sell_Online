using Sell_Online.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IChatService
    {
        Task<bool> CreateChat(Chat chat);
        List<Chat> GetChatsOfUser(string userId);
    }
}