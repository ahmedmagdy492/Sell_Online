using Sell_Online.DTO;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Mappers
{
    public static class NotificationMapper
    {
        public static Notification MapNotification(CreateNotificationDTO model)
        {
            return new Notification
            {
                NotificationID = Guid.NewGuid().ToString(),
                Content = model.Content,
                Title = model.Title,
                UserID = model.UserID,
                NotificationDate = DateTime.Now
            };
        }
    }
}
