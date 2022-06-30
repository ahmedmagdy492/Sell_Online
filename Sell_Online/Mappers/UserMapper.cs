using Sell_Online.DTO;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Mappers
{
    public static class UserMapper
    {
        public static User MapUser(RegisterUserDTO model)
        {
            return new User
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                Country = model.Country,
                City = model.City,
                District = model.District,
                Password = model.Password,
                ProfileImageURL = model.ProfileiImage
            };
        }
    }
}
