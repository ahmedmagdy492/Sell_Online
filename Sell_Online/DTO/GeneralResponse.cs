using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.DTO
{
    public class GeneralResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public string ErrorCode { get; set; }
        public List<ErrorModel> ValidationErrors { get; set; }
    }
}
