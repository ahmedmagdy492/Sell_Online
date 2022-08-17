using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImageURL { get; set; }
        public string ImageExtension { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsVerified { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<PostViews> MyViews { get; set; }

        public object GetUserBasicInfo()
        {
            return new
            {
                DisplayName,
                Email,
                Country,
                City,
                District,
                UserID,
                PhoneNumbers
            };
        }
    }
}
