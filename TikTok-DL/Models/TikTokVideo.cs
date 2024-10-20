using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//The model class for a TikTokVideo.
namespace TikTokDL.Models
{
    public class TikTokVideo
    {
        public string? TitleAndTags { get; set; }

        public string OnlyTitle
        {
            get
            {
                string onlyTitle = TitleAndTags?.Substring(0, 60);
                return $"{onlyTitle}...";
            }
        }

        public string? Url { get; set; }
        public string? Author { get; set; }
        public string? AuthorImage { get; set; }
        public string? FileSize
        {
            get
            {
                //Probably not a good solution, but this is used to remove the ,000 at the end.
                string dataSizeFormat = Math.Round(DataSizeKB).ToString("N3", System.Globalization.CultureInfo.GetCultureInfo("fr-FR"));

                return $"{dataSizeFormat.Substring(0, dataSizeFormat.Length - 4)} KB";

            }
        }
        public double DataSizeKB { get; set; }
        public string? Duration { get; set; }
        public bool? HasDataLoaded { get; set; }
        public string? CoverImage { get; set; }
        public string? DownloadUrl { get; set; }
        public DateTime UploadDate { get; set; }

        public string UploadedDays
        {
            get
            {
                long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                long uploadDate = ((DateTimeOffset)UploadDate).ToUnixTimeSeconds();

                long timeBetween = (long)Math.Round((currentTime - uploadDate) / 86400.0);

                return $"Uploaded {timeBetween} days ago.";
            }
        }
    }
}
