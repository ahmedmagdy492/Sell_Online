using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sell_Online.DTO;
using Sell_Online.Filters;
using Sell_Online.Helpers;
using Sell_Online.IServices;
using Sell_Online.Models;
using Sell_Online.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Message/")]
    [ExecptionCatcherFilter]
    public class MessageController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;

        public MessageController(IChatService chatService, IMessageService messageService)
        {
            _chatService = chatService;
            _messageService = messageService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetMessagesOfChat(string chatId)
        {
            if (string.IsNullOrWhiteSpace(chatId))
                return BadRequest(new { Message = "Invalid Chat ID" });

            var messages = _messageService.GetMessagesByChatID(chatId);
            return Ok(new { Message = "Success", Data = messages });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDTO message)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Validation Errors", Errors = ValidationHelper.ExtractErrMsgs(ModelState.Values) });

            var chat = _chatService.GetChatByID(message.ChatID).FirstOrDefault();

            if(chat == null)
            {
                // create chat
                chat = new Chat
                {
                    ChatID = Guid.NewGuid().ToString("N"),
                    Date = DateTime.Now,
                    Title = message.Message,
                    ReceiverID = message.ReceiverID,
                    SenderID = message.SenderID
                };
                await _chatService.CreateChat(chat);
            }
            else
            {
                chat.Title = message.Message;
                chat.Date = DateTime.Now;
                await _chatService.Update(chat);
            }

            await _messageService.CreateMessage(new Message
            {
                ID = Guid.NewGuid().ToString("N"),
                Content = message.Message,
                ChatID = chat.ChatID,
                ReceiverID = message.ReceiverID,
                SenderID = message.SenderID,
                Seen = false,
                SentDate = DateTime.Now,
            });

            return Created("", new { Message = "Message has been Sent" });
        }
    }
}
