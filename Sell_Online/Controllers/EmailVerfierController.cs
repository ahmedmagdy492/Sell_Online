using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sell_Online.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Controllers
{
    [ApiController]
    [Route("[controller]/VerifyEmail")]
    public class EmailVerfierController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<EmailVerfierController> _logger;
        private readonly IEmailVerificationService _emailVerificationService;

        public EmailVerfierController(IUserService userService, ILogger<EmailVerfierController> logger, IEmailVerificationService emailVerificationService)
        {
            _userService = userService;
            _logger = logger;
            _emailVerificationService = emailVerificationService;
        }

        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            if(string.IsNullOrWhiteSpace(token))
                return View("ErrorVerification");

            string currentToken = _emailVerificationService.GetToken(email)?.Token;

            _logger.LogInformation($"Current Token: {currentToken}, Given Token: {token}");

            if(string.IsNullOrWhiteSpace(currentToken) || currentToken != token)
                return View("ErrorVerification");

            var user = _userService.GetUserBy(u => u.Email == email).FirstOrDefault();

            if(user == null)
                return View("ErrorVerification");

            user.IsVerified = true;
            await _userService.UpdateUser(user);

            await _emailVerificationService.UpdateStatus(_emailVerificationService.GetToken(email));

            ViewData["Email"] = email;
            return View();
        }
    }
}
