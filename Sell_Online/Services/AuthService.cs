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
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public bool Authenticate(string username, string password)
        {
            var user = _userService.GetUserBy(u => u.Email == username && u.Password == password);
            return user != null;
        }
    }
}
