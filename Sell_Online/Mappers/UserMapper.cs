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
        public static User MapCreateUser(RegisterUserDTO model)
        {
            var user = new User
            {
                UserID = Guid.NewGuid().ToString(),
                Email = model.Email,
                DisplayName = model.DisplayName,
                Country = model.Country,
                City = model.City,
                District = model.District,
                Password = model.Password,
                ProfileImageURL = model.ProfileiImage
            };

            user.PhoneNumbers = new List<PhoneNumbers>
            {
                new PhoneNumbers
                {
                    ID = Guid.NewGuid().ToString(),
                    UserID = user.UserID,
                    PhoneNumber = model.PhoneNumber1
                }
            };

            user.PhoneNumbers.Add(new PhoneNumbers
            {
                ID = Guid.NewGuid().ToString(),
                UserID = user.UserID,
                PhoneNumber = model.PhoneNumber2
            });

            return user;
        }

        public static User MapUpdateUser(User user, UpdateUserDTO model)
        {
            user.Country = model.Country;
            user.City = model.City;
            user.District = model.District;

            if(user.PhoneNumbers != null)
            {
                var phoneNumber1 = user.PhoneNumbers.FirstOrDefault();

                if(phoneNumber1 != null)
                {
                    phoneNumber1.PhoneNumber = model.PhoneNumber1;
                }
                else
                {
                    user.PhoneNumbers.Add(new PhoneNumbers
                    {
                        UserID = user.UserID,
                        ID = Guid.NewGuid().ToString(),
                        PhoneNumber = model.PhoneNumber1
                    });
                }

                var phoneNumber2 = user.PhoneNumbers.LastOrDefault();

                if(phoneNumber2 != null)
                {
                    phoneNumber2.PhoneNumber = model.PhoneNumber2;
                }
                else
                {
                    user.PhoneNumbers.Add(new PhoneNumbers
                    {
                        UserID = user.UserID,
                        PhoneNumber = model.PhoneNumber2,
                        ID = Guid.NewGuid().ToString()
                    });
                }
            }
            else
            {
                user.PhoneNumbers = new List<PhoneNumbers>
                {
                    new PhoneNumbers
                    {
                        ID = Guid.NewGuid().ToString(),
                        UserID = user.UserID,
                        PhoneNumber = model.PhoneNumber1
                    }
                };

                user.PhoneNumbers = new List<PhoneNumbers>
                {
                    new PhoneNumbers
                    {
                        ID = Guid.NewGuid().ToString(),
                        UserID = user.UserID,
                        PhoneNumber = model.PhoneNumber2
                    }
                };
            }

            return user;
        }
    }
}
