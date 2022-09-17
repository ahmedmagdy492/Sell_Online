using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sell_Online.Filters;
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
    [Route("v1/Chat")]
    [ExecptionCatcherFilter]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IChatService chatService, ILogger<ChatController> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetChatsOfUser()
        {
            var userId = User.Claims.ToList()[0].Value;
            return Ok(new { Message = "Success", Data = _chatService.GetChatListByUserID(userId) });
        }

        [Authorize]
        [HttpGet("{senderId}/{receiverId}")]
        public IActionResult GetChatBySenderAndReceiver([FromRoute]string senderId, [FromRoute]string receiverId)
        {
            if (string.IsNullOrWhiteSpace(senderId) || string.IsNullOrWhiteSpace(receiverId))
                return BadRequest(new { Message = "Invalid Ids Sent" });

            _logger.LogInformation($"senderId = {senderId}, receiverId = {receiverId}");

            var chat = _chatService.GetChatBySenderAndReceiverIds(senderId, receiverId);
            
            return Ok(new { Message = "Success", Data = new List<Chat> { chat } });
        }
    }
}
