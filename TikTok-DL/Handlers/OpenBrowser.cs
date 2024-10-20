using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTokDL.Handlers
{
    //The purpose of this class is to handle all kinds of browser related tasks.
    public static class OpenBrowser
    {
        public static async Task OpenInBrowserAsync(string url)
        {
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(psi);
                }
                else
                {
                    throw new InvalidOperationException("The VideoUrl is null or empty.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
