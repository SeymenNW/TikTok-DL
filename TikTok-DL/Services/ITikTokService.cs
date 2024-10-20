

using System.Threading.Tasks;
using TikTokDL.Models;

namespace TikTokDL.Services
{
    public interface ITikTokService
    {
        public Task<string> GetRedirectUrl(string url);
        public Task<string> GetMediaId(string url);
        public Task<TikTokVideo> GetMedia(string url);


    }
}

