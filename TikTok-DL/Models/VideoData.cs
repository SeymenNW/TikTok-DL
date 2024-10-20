using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTokDL.Models
{

    //These classes are primarily only used for getting data from the TikTok Api.
    //Then it gets wrapped around in another class to make it simpler to understand.
    
    class VideoData
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("images")]
        public List<string> Images { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("avatarUrls")]
        public List<string> AvatarUrls { get; set; }

        [JsonProperty("gifAvatarUrls")]
        public List<string> GifAvatarUrls { get; set; }
    }

    class ApiData
    {
        [JsonProperty("aweme_list")]
        public List<Aweme> AwemeList { get; set; }
    }

    class Aweme
    {
        [JsonProperty("aweme_id")]
        public string AwemeId { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("image_post_info")]
        public ImagePostInfo ImagePostInfo { get; set; }

        [JsonProperty("create_time")]
        public long CreateTime { get; set; }

        [JsonProperty("video")]
        public Video Video { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }
    }

    class ImagePostInfo
    {
        [JsonProperty("images")]
        public List<Image> Images { get; set; }
    }

    class Image
    {
        [JsonProperty("display_image")]
        public DisplayImage DisplayImage { get; set; }
    }

    class DisplayImage
    {
        [JsonProperty("url_list")]
        public List<string> UrlList { get; set; }
    }

    public class Video
    {
        [JsonProperty("download_addr")]
        public DownloadAddr DownloadAddr { get; set; }

        [JsonProperty("cover")]
        public Cover Cover = new();

        [JsonProperty("dynamic_cover")]
        public DynamicCover DynamicCover { get; set; }


        [JsonProperty("play_addr")]
        public PlayAddr PlayAddr { get; set; }

    }

    public class Cover
    {
        [JsonProperty("url_list")]
        public List<string> UrlList { get; set; }
    }

    public class DynamicCover
    {
        [JsonProperty("url_list")]
        public List<string> UrlList { get; set; }
    }


    class Author
    {
        [JsonProperty("avatar_medium")]
        public Avatar AvatarMedium { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }


    }
    class Avatar
    {
        [JsonProperty("url_list")]
        public List<string> UrlList { get; set; }
    }

    public class DownloadAddr
    {
        [JsonProperty("url_list")]
        public List<string> UrlList { get; set; }
    }

    public class PlayAddr
    {
        [JsonProperty("url_list")]
        public List<string> UrlList { get; set; }

        [JsonProperty("data_size")]
        public long DataSize { get; set; }
    }


}
