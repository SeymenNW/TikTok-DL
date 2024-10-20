using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TikTokDL.Models;

namespace TikTokDL.Services
{

    
    public class TikTokService : ITikTokService
    {


        public async Task<TikTokVideo?> GetTikTokMedia(string url)
        {
            bool isWatermarked = false;
            string mediaId;

           
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    url = response.RequestMessage.RequestUri.AbsoluteUri;
                }

                Regex rgxMediaId = new Regex(@"(?:/video/|/photo/)(\d+)", RegexOptions.Compiled);
                Match rgxMediaIdMatch = rgxMediaId.Match(url);
                if (rgxMediaIdMatch.Success)
                {
                    mediaId = rgxMediaIdMatch.Groups[1].Value; 
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            //This is the TikTok API, It's disguised as a phone request (I found this API in another open source project)
            string apiUrl = $"https://api22-normal-c-alisg.tiktokv.com/aweme/v1/feed/?aweme_id={mediaId}&iid=7318518857994389254&device_id=7318517321748022790&channel=googleplay&app_name=musical_ly&version_code=300904&device_platform=android&device_type=ASUS_Z01QD&version=9";

            using (var client = new HttpClient())
            {
                try
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Options, apiUrl);
                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(json))
                        return null;

                    var data = JsonConvert.DeserializeObject<ApiData>(json);
                    if (data?.AwemeList == null || data.AwemeList.Count == 0)
                        return null;

                    var video = data.AwemeList.FirstOrDefault();
                    if (video == null) return null;

                    var videoTitle = video.Desc;
                    var authorName = video.Author?.Nickname;
                    var urlMedia = isWatermarked ? video.Video?.DownloadAddr?.UrlList.FirstOrDefault() : video.Video?.PlayAddr?.UrlList.FirstOrDefault();
                    var coverUrl = video.Video?.DynamicCover?.UrlList.FirstOrDefault();
                    var dataSize = video.Video?.PlayAddr?.DataSize ?? 0;
                    var uploadDate = video.CreateTime;

                    if (urlMedia == null)
                        return null;

                    return new TikTokVideo
                    {
                        DownloadUrl = urlMedia,
                        AuthorImage = video.Author?.AvatarMedium?.UrlList.FirstOrDefault(),
                        Author = authorName,
                        TitleAndTags = videoTitle,
                        CoverImage = coverUrl,
                        DataSizeKB = dataSize / 1024.0,
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
    }
}
