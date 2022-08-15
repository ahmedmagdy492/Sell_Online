using Sell_Online.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface INotificationService
    {
        Task<bool> CreateNotification(Notification notification);
        List<Notification> GetMyNotification(string userId);
    }
}