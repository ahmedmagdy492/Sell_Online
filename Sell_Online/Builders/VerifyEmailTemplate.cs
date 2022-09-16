using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sell_Online.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Builders
{
    public class VerifyEmailTemplate : IEmailTamplate
    {
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private readonly IEmailVerificationService _emailVerificationService;

        public VerifyEmailTemplate(IConfiguration configuration, HttpContext httpContext, IEmailVerificationService emailVerificationService)
        {
            _configuration = configuration;
            _httpContext = httpContext;
            _emailVerificationService = emailVerificationService;
        }

        public string GetContent(string email)
        {
            string token = Guid.NewGuid().ToString("N");
            _emailVerificationService.SetToken(token, email).Wait();
            return $"<div>\r\n  <h2 style=\"text-align: center\">Sell Online</h2>\r\n  <h4 style=\"text-align: center\">Please Verify your Email</h4>\r\n  <h4 style=\"text-align: center;\">\r\n    <a href=\"{_configuration["MailSettings:RedirectURL"]}?email={email}&token={token}\" style=\"text-decoration: none;background: #000;padding: 10px;color: #fff;border-radius: 10px\">Verify Your Email</a>\r\n  </h4>\r\n</div>\r\n<hr style=\"margin-top: 20px\">\r\n<h4 style=\"text-align: center;\">Sell Online &copy 2022</h4>";
        }
    }
}
