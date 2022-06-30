using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z0-9]{8,16}$")]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}
