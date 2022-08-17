using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sell_Online.IServices;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Chats")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetChatListOfUser()
        {
            var userId = User.Claims.ToList()[0].Value;
            
            var chats = _chatService.GetChatsOfUser(userId);

            List<object> customChats = new List<object>();

            foreach(Chat chat in chats)
            {
                customChats.Add(chat.GetChatOutModel());
            }

            return Ok(new { Message = "Success", Data = customChats });
        }
    }
}
