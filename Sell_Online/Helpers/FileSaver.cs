using Microsoft.Extensions.Configuration;
using Sell_Online.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Helpers
{
    public class FileSaver
    {
        private readonly IConfiguration _configuration;

        public FileSaver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SaveFile(byte[] fileBytes, string fileType)
        {
            string filePath = System.IO.Path.Combine(_configuration["AppSettings:ImagePath"], $"{Guid.NewGuid()}.{fileType}");

            System.IO.File.WriteAllBytes(filePath, fileBytes);
        }
    }
}
