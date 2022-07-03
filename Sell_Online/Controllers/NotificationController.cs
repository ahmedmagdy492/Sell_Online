using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sell_Online.Filters;
using Sell_Online.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Notification/")]
    [ExecptionCatcherFilter]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMyNotifications()
        {
            var userId = User.Claims.ToList()[0].Value;
            var notifications = _notificationService.GetMyNotification(userId);

            return Ok(new { Message = "Success", Data = notifications });
        }
    }
}
