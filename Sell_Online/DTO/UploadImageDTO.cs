using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.DTO
{
    public class UploadImageDTO
    {
        [Required]
        public string Base64 { get; set; }
        [Required]
        public string ImageType { get; set; }
        [Required]
        public string PostID { get; set; }
    }
}
