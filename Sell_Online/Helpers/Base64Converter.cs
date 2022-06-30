using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Helpers
{
    public class Base64Converter
    {
        public byte[] ConvertFromBase64(string base64)
        {
            return Convert.FromBase64String(base64);
        }
    }
}
