using Sell_Online.Data;
using Sell_Online.DTO;
using Sell_Online.IServices;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDBContext _context;

        public NotificationService(AppDBContext context)
        {
            _context = context;
        }

        public List<Notification> GetMyNotification(string userId)
        {
            return _context.Notifications.Where(i => i.UserID == userId).ToList();
        }

        public async Task<bool> CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
