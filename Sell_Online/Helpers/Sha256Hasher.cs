using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Helpers
{
    public class Sha256Hasher
    {
        private SHA256 _sHA256;
        
        public Sha256Hasher()
        {
            _sHA256 = SHA256.Create();
        }

        public string Hash(string plainText)
        {
            var hashedBytes = _sHA256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            return ConvertBytesToHex(hashedBytes);
        }

        private string ConvertBytesToHex(byte[] array)
        {
            StringBuilder result = new StringBuilder();
            
            foreach(byte b in array)
            {
                result.AppendFormat("{0:x2}", b);
            }

            return result.ToString();
        }
    }
}
