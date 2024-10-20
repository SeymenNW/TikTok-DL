using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using TikTokDL.ViewModels;
using TikTokDL.Handlers;

namespace TikTokDL.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindowViewModel(); //// This gets set in the DI Container.
        }

        //The reason this is in the code-behind is due to the StorageProvider needing the window object.
        //I am looking for alternative ways for it though (maybe through some DI stuff)
        private async void DownloadVideo_Click(object sender, RoutedEventArgs e)
        {
            var window = this.GetVisualRoot() as Window;

            if (DataContext is MainWindowViewModel videoViewModel)
            {
                await VideoOptions.DownloadAndSaveVideoAsync(window, videoViewModel.TikTokVideo.DownloadUrl);
            }
        }
    }
}