using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Helpers
{
    public class FileReader
    {
        private readonly IConfiguration _configuration;

        public FileReader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public byte[] ReadFile(string fileName)
        {
            return File.ReadAllBytes(Path.Combine(_configuration["AppSettings:ImagePath"], fileName));
        }
    }
}
