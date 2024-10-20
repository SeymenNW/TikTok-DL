using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TikTokDL.Handlers
{
    //This class is imported from my SwiftyDL project (Multi SoMe Downloader), Which means it may have some stuff that isn't being used by TikTokDL.
    public static class VideoOptions
    {

        public static async Task<long?> GetVideoFileSizeAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Head, url);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode && response.Content.Headers.ContentLength.HasValue)
                {
                    return response.Content.Headers.ContentLength; // Bytes
                }
            }

            return null;
        }

        public static async Task DownloadVideoAsync(string videoUrl, string filePath)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var videoData = await httpClient.GetByteArrayAsync(videoUrl);

                    await File.WriteAllBytesAsync($"{filePath}.mp4", videoData);

                    Console.WriteLine("Download complete.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        public static async Task DownloadAndSaveVideoAsync(Avalonia.Controls.Window window, string videoUrl)
        {
            var storage = window.StorageProvider;

            if (storage.CanSave)
            {
                var file = await storage.SaveFilePickerAsync(new FilePickerSaveOptions());

                if (file != null)
                {
                    var filePath = file.TryGetLocalPath().ToString();
                    if (filePath.StartsWith("file://"))
                    {
                        filePath = filePath.Substring(7);
                    }

                    if (string.IsNullOrWhiteSpace(filePath) || filePath.Contains("::"))
                    {
                        throw new ArgumentException("The file path is invalid.");
                    }

                    await DownloadVideoAsync(videoUrl, filePath);
                }
            }

        }
    }
}
