using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TikTokDL.Handlers
{
    public static class TikTokValidators
    {
        public static bool ValidateUrl(string url)
        {

            // Regex pattern to match the TikTok URL format with @username and video ID
            string pattern = @"^(https:\/\/)?(www\.)?tiktok\.com\/@[\w]+\/(video|image)\/\d+(\?.*)?$";


            // Match the URL against the regex pattern
            return Regex.IsMatch(url, pattern);
        }
    }
}
