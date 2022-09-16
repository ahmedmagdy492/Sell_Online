﻿using Microsoft.EntityFrameworkCore;
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
    public class MessageService : IMessageService
    {
        private readonly AppDBContext _context;

        public MessageService(AppDBContext context)
        {
            _context = context;
        }

        public object GetChatsOfUser(string userId)
        {
            return _context.Messages.Include(u => u.Sender).Include(u => u.Receiver).Where(m => m.SenderID == userId || m.ReceiverID == userId).Select(m => new { m.ID, SenderName = m.Sender.DisplayName, RecieverName = m.Receiver.DisplayName, m.SentDate, m.Content, m.Seen }).ToList();
        }

        public object GetListOfMessagesOfUser(string senderId, string receiverId)
        {
            return _context.Messages.Include(u => u.Sender).Include(u => u.Receiver).Where(m => (m.SenderID == senderId && m.ReceiverID == receiverId) || (m.SenderID == receiverId && m.ReceiverID == senderId)).Select(m => new { m.ID, SenderName = m.Sender.DisplayName, RecieverName = m.Receiver.DisplayName, m.SentDate, m.Content, m.Seen }).ToList();
        }

        public async Task<bool> CreateMessage(Message message)
        {
            _context.Messages.Add(message);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> MarkAsSeen(Message message)
        {
            message.Seen = true;
            _context.Entry(message).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public Message GetMessageByID(string messageId)
        {
            return _context.Messages.FirstOrDefault(m => m.ID == messageId);
        }
    }
}
