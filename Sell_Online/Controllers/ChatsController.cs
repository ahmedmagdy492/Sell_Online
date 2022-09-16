using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sell_Online.Filters;
using Sell_Online.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [ExecptionCatcherFilter]
    [Route("v1/Chats")]
    public class ChatsController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public ChatsController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetChatsOfUser()
        {
            var userId = User.Claims.ToList()[0].Value;

            var chats = _messageService.GetChatsOfUser(userId);
            return Ok(new { Message = "Success", Data = chats });
        }
    }
}
