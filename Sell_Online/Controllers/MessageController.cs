using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sell_Online.DTO;
using Sell_Online.Filters;
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
    [ExecptionCatcherFilter]
    [Route("v1/Messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetListOfMessagesBySenderAndReceiverId(string receiverId)
        {
            if (string.IsNullOrWhiteSpace(receiverId))
                return BadRequest(new { Message = "Invalid Receiver Id", Errors = new List<string> { "Invalid Receiver Id" } });

            var messages = _messageService.GetListOfMessagesOfUser(User.Claims.ToList()[0].Value, receiverId);
            return Ok(new { Message = "Success", Data = messages });
        }

        [Authorize]
        [HttpPost("SendMessage")]
        public async Task<IActionResult> CreateMessage(CreateMessageDTO messageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Validation Errors", Errors = ValidationHelper.ExtractErrMsgs(ModelState.Values) });

            messageDto.SenderID = User.Claims.ToList()[0].Value;
            await _messageService.CreateMessage(MessageMapper.MapMessage(messageDto));
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
