using Sell_Online.Models;
using System.Threading.Tasks;

namespace Sell_Online.IServices
{
    public interface IViewsService
    {
        int HasUserViewedPost(string userId, string postId);
        Task<bool> ViewPost(Post post, string viewerId);
    }
}