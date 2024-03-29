﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.DTO
{
    public class CreateMessageDTO
    {
        public string ChatID { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string SenderID { get; set; }
        [Required]
        public string ReceiverID { get; set; }
    }
}
