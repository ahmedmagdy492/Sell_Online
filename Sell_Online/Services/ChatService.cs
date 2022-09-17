using Sell_Online.Data;
using Sell_Online.IServices;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Services
{
    public class ChatService : IChatService
    {
        private readonly AppDBContext _context;

        public ChatService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateChat(Chat chat)
        {
            _context.Chats.Add(chat);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public List<Chat> GetChatListByUserID(string userId)
        {
            return _context.Chats.Where(c => c.SenderID == userId || c.ReceiverID == userId).ToList();
        }

        public List<Chat> GetChatByID(string chatId)
        {
            return _context.Chats.Where(c => c.ChatID == chatId).ToList();
        }
    }
}
