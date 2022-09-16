using Sell_Online.DTO.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
