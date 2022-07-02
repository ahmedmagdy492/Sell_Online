﻿using Microsoft.EntityFrameworkCore;
using Sell_Online.Data;
using Sell_Online.DTO;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Services
{
    public class UserService
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
    }
}
