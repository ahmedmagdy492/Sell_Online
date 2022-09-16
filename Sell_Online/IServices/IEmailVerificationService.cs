using Sell_Online.Models;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IEmailVerificationService
    {
        EmailVerificationToken GetToken(string email);
        Task<bool> SetToken(string token, string email);
        Task<bool> UpdateStatus(EmailVerificationToken token);
    }
}