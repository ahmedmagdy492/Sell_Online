﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class PostStates
    {
        [Key]
        public short? StateID { get; set; }
        public string StateTitle { get; set; }
    }
}
