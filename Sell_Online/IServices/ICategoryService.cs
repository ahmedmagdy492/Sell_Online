using Sell_Online.Models;
using System.Collections.Generic;

namespace Sell_Online.IServices
{
    public interface ICategoryService
    {
        List<PostCategory> GetAll();
    }
}