using Sell_Online.Data;
using Sell_Online.Models;
using Sell_Online.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public class MessageService : IMessageService
    {
        private readonly AppDBContext _context;

        public MessageService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMessage(Message message)
        {
            _context.Messages.Add(message);
            return await _context.SaveChangesAsync() > 0;
        }

        public List<Message> GetMessagesByChatID(string chatId)
        {
            return _context.Messages.Where(m => m.ChatID == chatId).ToList();
        }
    }
}
