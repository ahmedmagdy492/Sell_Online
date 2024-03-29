﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Models
{
    public class Notification
    {
        public Notification()
        {
            NotificationDate = DateTime.Now;
        }

        [Key]
        public string NotificationID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserID { get; set; }
        public DateTime? NotificationDate { get; set; }

        public User User { get; set; }
    }
}
