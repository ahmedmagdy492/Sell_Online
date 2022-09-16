using Sell_Online.Data;
using Sell_Online.IServices;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Services
{
    public class EmailVerificationService : IEmailVerificationService
    {
        private readonly AppDBContext _context;

        public EmailVerificationService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> SetToken(string token, string email)
        {
            _context.EmailVerificationTokens.Add(new Models.EmailVerificationToken
            {
                Email = email,
                Token = token,
            });
            return (await _context.SaveChangesAsync()) > 0;
        }

        public EmailVerificationToken GetToken(string email)
        {
            return _context.EmailVerificationTokens.FirstOrDefault(x => x.Email == email  && !x.IsUsed);
        }

        public async Task<bool> UpdateStatus(EmailVerificationToken token)
        {
            token.IsUsed = true;
            _context.Entry(token).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
