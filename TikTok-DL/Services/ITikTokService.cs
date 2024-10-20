

using System.Threading.Tasks;
using TikTokDL.Models;

namespace TikTokDL.Services
{
    public interface ITikTokService
    {
        public Task<TikTokVideo> GetTikTokMedia(string url);


    }
}

