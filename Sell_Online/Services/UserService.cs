using Microsoft.EntityFrameworkCore;
using Sell_Online.Data;
using Sell_Online.DTO;
using Sell_Online.IServices;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _context;

        public UserService(AppDBContext context)
        {
            _context = context;
        }

        public List<User> GetUserBy(Func<User, bool> condition, string include = "")
        {
            if (include == "")
                return _context.Users.Where(condition).ToList();
            var result = _context.Users.Include(include).Where(condition).ToList();
            result.ForEach(i =>
            {
                i.PhoneNumbers.ToList().ForEach(i =>
                {
                    i.User = null;
                });
            });
            return result;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> CreateUser(User user)
        {
            _context.Users.Add(user);
            return (await _context.SaveChangesAsync()) > 0;
        }

        /// <summary>
        /// changes the password of a given user
        /// </summary>
        /// <param name="user">user object to change the password for</param>
        /// <param name="newPassword">should be Hashed</param>
        /// <returns>true if the password has been changed successfully</returns>
        public async Task<bool> ChangePassword(User user, string newPassword)
        {
            user.Password = newPassword;
            _context.Entry(user).State = EntityState.Modified;
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
