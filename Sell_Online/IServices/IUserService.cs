using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IUserService
    {
        Task<bool> ChangePassword(User user, string newPassword);
        Task<bool> CreateUser(User user);
        List<User> GetUserBy(Func<User, bool> condition, string include = "");
        Task<bool> UpdateUser(User user);
    }
}