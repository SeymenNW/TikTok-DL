using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System;
using TikTokDL.Models;
using TikTokDL.Handlers;
using TikTokDL.Services;

namespace TikTok_DL.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        // Fields
        #region fields
        private readonly ITikTokService _tikTokService;
        #endregion

        public MainWindowViewModel(ITikTokService tikTokService)
        {
            //The TikTokService gets injected from DI Container.
            _tikTokService = tikTokService;
        }



        #region Properties

        [ObservableProperty]
        public TikTokVideo tikTokVideo;

        [ObservableProperty]
        public bool isLoading;

        [ObservableProperty]
        public bool hasLoaded;

        [ObservableProperty]
        public string loadTimer1;

        private string x => tikTokVideo.DownloadUrl;

        [ObservableProperty]
        public string videoUrl = "https://www.tiktok.com/@tridzoid/video/7414578349164580138";

        #endregion

        #region Commands



        [RelayCommand]
        public async Task LoadMetadata()
        {

            //This only represents the expected load time, and if u have slow internet it will eventually pass 100% lol.
            //However the API response gets slower the more you request it within a timeframe of 30-60 seconds.
            ProgressCount = 0;
            var timer = new System.Timers.Timer
            {
                Interval = 60,
                Enabled = true,
                AutoReset = true,

            }; timer.Elapsed += LoadTimer;


            TikTokVideo = null;
            while (TikTokVideo == null)
            {
                IsLoading = true;
                HasLoaded = false;
                TikTokVideo = await _tikTokService.GetMedia(VideoUrl);
                IsLoading = false;
                HasLoaded = true;
            }


        }

        private static int ProgressCount = 0;

        private void LoadTimer(object? sender, System.Timers.ElapsedEventArgs e)
        {
            ProgressCount++;
            if (ProgressCount < 101)
            {
                LoadTimer1 = $"{ProgressCount.ToString()}%";

            }
            else
            {
                LoadTimer1 = "Still loading.. (Slower than expected.. Get better internet😉)";
            }
        }

        [RelayCommand]
        private async Task OpenInTikTok()
        {
            await OpenBrowser.OpenInBrowserAsync(VideoUrl);
        }






        #endregion
    }
}
