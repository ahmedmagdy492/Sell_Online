using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Builders
{
    public interface IEmailTamplate
    {
        string GetContent(string data);
    }
}
