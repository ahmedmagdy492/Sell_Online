using Sell_Online.Data;
using Sell_Online.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sell_Online.Services
{
    public class ImagesServices
    {
        private readonly AppDBContext _context;

        public ImagesServices(AppDBContext context)
        {
            _context = context;
        }

        public List<PostImages> GetImagesByPostID(string postID)
        {
            return _context.PostImages.Where(i => i.PostID == postID).ToList();
        }
    }
}
