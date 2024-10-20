using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TikTokDL.Models;

namespace TikTokDL.Services
{
    public class TikTokService : ITikTokService
    {

        public async Task<string> GetRedirectUrl(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    return response.RequestMessage?.RequestUri?.AbsoluteUri ?? url;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<TikTokVideo?> GetMedia(string url)
        {
            bool withWatermark = false;
            var mediaId = await GetMediaId(url);

            //This is the TikTok API, It's disguised as a phone request (I found this API in another open source project)
            var apiUrl = $"https://api22-normal-c-alisg.tiktokv.com/aweme/v1/feed/?aweme_id={mediaId}&iid=7318518857994389254&device_id=7318517321748022790&channel=googleplay&app_name=musical_ly&version_code=300904&device_platform=android&device_type=ASUS_Z01QD&version=9";

            using (var client = new HttpClient())
            {
                try
                {
                    var finalUrl = await GetRedirectUrl(url);
                    var request = new HttpRequestMessage(HttpMethod.Options, apiUrl);
                    var response = await client.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();


                    if (string.IsNullOrWhiteSpace(json))
                    {
                        return null;
                    }

                    var data = JsonConvert.DeserializeObject<ApiData>(json);

                    if (data?.AwemeList == null || data.AwemeList.Count == 0)
                    {
                        return null;
                    }

                    var video = data.AwemeList.FirstOrDefault();
                    var videoTitle = video?.Desc;
                    var authorName = video?.Author.Nickname;
                    var urlMedia = withWatermark ? video?.Video?.DownloadAddr?.UrlList.FirstOrDefault() : video?.Video?.PlayAddr?.UrlList.FirstOrDefault();
                    var imageUrls = video?.ImagePostInfo?.Images?.Select(img => img.DisplayImage.UrlList.FirstOrDefault()).ToList();
                    var avatarUrls = video?.Author?.AvatarMedium?.UrlList ?? new List<string>();
                    var coverUrl = video?.Video.DynamicCover.UrlList[0];
                    var gifAvatarUrls = video?.Author?.AvatarMedium?.UrlList ?? new List<string>();
                    var dataSize = video?.Video.PlayAddr.DataSize;
                    var uploadDate = video?.CreateTime;

                    if (urlMedia == null)
                    {

                        return null;
                    }

                    return new TikTokVideo
                    {
                        DownloadUrl = urlMedia,
                        AuthorImage = gifAvatarUrls[0],
                        Author = authorName,
                        TitleAndTags = videoTitle,
                        CoverImage = coverUrl,
                        DataSizeKB = (double)(dataSize / 1024.0),
                        UploadDate = DateTimeOffset.FromUnixTimeSeconds((long)uploadDate).LocalDateTime

                    };
                }
                catch (HttpRequestException ex)
                {
                    return null;
                }
                catch (JsonException ex)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<string> GetMediaId(string url)
        {
            try
            {
                if (url.Contains("/t/"))
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(url);
                        url = response.RequestMessage?.RequestUri.Segments.LastOrDefault()?.TrimEnd('/') ?? string.Empty;
                    }
                }
                else
                {
                    url = await GetRedirectUrl(url);
                }

                var matching = url.Contains("/video/");
                var matchingPhoto = url.Contains("/photo/");
                var startIndex = url.IndexOf("/video/") + 7;
                var endIndex = url.IndexOf("/video/") + 26;

                if (matchingPhoto)
                {
                    startIndex = url.IndexOf("/photo/") + 7;
                    endIndex = url.IndexOf("/photo/") + 26;
                }
                else if (!matching)
                {
                    //URL Not found.
                    return string.Empty;
                }

                if (endIndex > url.Length || startIndex < 0 || endIndex < startIndex)
                {
                    //Invalid URL Format.
                    return string.Empty;
                }

                var MediaID = url.Substring(startIndex, endIndex - startIndex);

                return MediaID;
            }
            catch (Exception ex)
            {
                //Error occurred while extracting MediaID
                return string.Empty;
            }
        }



    }
}
