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
    public class AuthService
    {
        private readonly AppDBContext _context;

        public AuthService(AppDBContext context)
        {
            _context = context;
        }

        public List<User> GetUserBy(Func<User, bool> condition, string include = "")
        {
            if (include == "")
                return _context.Users.Where(condition).ToList();
            return _context.Users.Include(include).Where(condition).ToList();
        }

        public async Task<bool> CreateUser(User user)
        {
            _context.Users.Add(user);
            return (await _context.SaveChangesAsync()) > 0;
        }


        /// <summary>
        /// change the password of a given user in the database
        /// </summary>
        /// <param name="changePasswordDTO">
        /// New Password: should be Hashed
        /// </param>
        /// <returns>whether the operation were successfull or not</returns>
        public async Task<bool> ChangePassword(User user, string newPassword)
        {
            user.Password = newPassword;
            _context.Entry(user).State = EntityState.Modified;
            return (await _context.SaveChangesAsync()) > 0;
        }


    }
}
