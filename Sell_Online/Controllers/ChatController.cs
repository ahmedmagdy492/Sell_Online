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
    [Route("v1/Chat")]
    [ExecptionCatcherFilter]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetChatsOfUser()
        {
            var userId = User.Claims.ToList()[0].Value;
            return Ok(new { Message = "Success", Data = _chatService.GetChatListByUserID(userId) });
        }
    }
}
