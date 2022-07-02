using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z0-9]{8,16}$")]
        public string Password { get; set; }

        /// <summary>
        /// the Base64 of the image
        /// </summary>
        [Required]
        public string ProfileiImage { get; set; }
        [Required]
        public string ImageType { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Phone Number 1 Should Be exactly 11 digits")]
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
    }
}
