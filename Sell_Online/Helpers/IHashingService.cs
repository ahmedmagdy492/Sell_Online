using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Helpers
{
    public interface IHashingService
    {
        string Hash(string plainText);
    }
}
