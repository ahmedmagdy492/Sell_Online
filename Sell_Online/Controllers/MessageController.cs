using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sell_Online.DTO;
using Sell_Online.Helpers;
using Sell_Online.IServices;
using Sell_Online.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("v1/Messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;

        public MessageController(IMessageService messageService, IChatService chatService)
        {
            _messageService = messageService;
            _chatService = chatService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetListOfMessagesByChatID(string chatId)
        {
            if (string.IsNullOrWhiteSpace(chatId))
                return BadRequest(new { Message = "Invalid Chat ID" });

            var messages = _messageService.GetListOfMessagesOfChat(chatId);
            return Ok(new { Message = "Success", Data = messages });
        }

        [Authorize]
        [HttpPost("SendMessage")]
        public async Task<IActionResult> CreateMessage(CreateMessageDTO messageDto, string receiverId)
        {
            if (string.IsNullOrWhiteSpace(receiverId))
                return BadRequest(new { Message = "Invalid Receiver ID", Errors = new List<string> { "Invalid Receiver ID" } });

            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Validation Errors", Errors = ValidationHelper.ValidateInput(ModelState.Values) });

            var chatModel = new CreateChatDTO
            {
                ReceiverID = receiverId,
                SenderID = User.Claims.ToList()[0].Value
            };
            var createChatResult = await _chatService.CreateChat(ChatMapper.Map(chatModel));

            if (!createChatResult)
                return BadRequest(new { Message = "Error Occured while creating chat", Errors = new List<string> { "Error Occured while creating chat" } });

            messageDto.ChatID = chatModel.ChatID;
            var createMsgResult = await _messageService.CreateMessage(MessageMapper.MapMessage(messageDto));
            return Ok(new { Message = "Message has been sent Successfully", Errors = new List<string> { } });
        }

        [Authorize]
        [HttpPatch("See")]
        public IActionResult MarkAsSeen(string messageId)
        {
            if (string.IsNullOrWhiteSpace(messageId))
                return BadRequest(new { Message = "Invalid Message ID", Errors = new List<string> { "Invalid Message ID" } });

            var message = _messageService.GetMessageByID(messageId);

            if (message == null)
                return NotFound(new { Message = "Message is not found", Errors = new List<string> { "Message id is not found" } });

            var updateMsgResult = _messageService.MarkAsSeen(message);
            return Ok(new { Message = "Message has been updated successfully", Errors = new List<string> { } });
        }
    }
}
