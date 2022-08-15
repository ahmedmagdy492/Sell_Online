using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IAuthService
    {
        /// <summary>
        /// authenticates the user
        /// </summary>
        /// <param name="username">the username of the user</param>
        /// <param name="password">the hashed version of the password</param>
        /// <returns>true if the user credientials are valid</returns>
        bool Authenticate(string username, string password);
    }
}
