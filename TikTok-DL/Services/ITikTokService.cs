

using System.Threading.Tasks;

namespace TikTokDL.Services
{
    public interface ITikTokService
    {

        //This method is for getting the title
        Task<string> GetTitle();
    }
}

