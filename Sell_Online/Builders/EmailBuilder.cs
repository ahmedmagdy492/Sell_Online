using Sell_Online.DTO.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Builders
{
    public class EmailBuilder
    {
        private readonly MailRequest _mailRequest;

        public EmailBuilder()
        {
            _mailRequest = new MailRequest();
        }

        public EmailBuilder ToEmail(string email)
        {
            _mailRequest.ToEmail = email;
            return this;
        }

        /// <summary>
        /// sets the body of the mail as html by using a provided template
        /// NOTE: when ToEmail property is null an exception is thrown
        /// </summary>
        /// <param name="emailTemplate">string that represents the template to send</param>
        /// <returns>Email Builder</returns>
        /// <exception cref="ArgumentNullException">when ToEmail property is null</exception>
        public EmailBuilder Body(IEmailTamplate emailTemplate)
        {
            if (string.IsNullOrWhiteSpace(_mailRequest.ToEmail))
                throw new ArgumentNullException("To Email must be set first");

            _mailRequest.Body = emailTemplate.GetContent(_mailRequest.ToEmail);
            return this;
        }

        public EmailBuilder Subject(string subject)
        {
            _mailRequest.Subject = subject;
            return this;
        }

        public MailRequest Build()
        {
            return _mailRequest;
        }
    }
}
