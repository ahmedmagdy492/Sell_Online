using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.DTO
{
    public class GeneralResponseGet<T>
    {
        public string Message { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
