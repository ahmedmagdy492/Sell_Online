using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class EmailVerificationToken
    {
        [Key]
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsUsed { get; set; } = false;
    }
}
