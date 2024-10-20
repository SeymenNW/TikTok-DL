using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using TikTok_DL.ViewModels;
using TikTok_DL.Views;
using TikTokDL.Services;

namespace TikTok_DL
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
           
            ServiceCollection collection = new ServiceCollection();

            collection.AddTransient<ITikTokService, TikTokService>();

            collection.AddTransient<MainWindowViewModel>();

            ServiceProvider services = collection.BuildServiceProvider();

            MainWindowViewModel mainWindowViewModel = services.GetRequiredService<MainWindowViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    
                    DataContext = mainWindowViewModel
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainWindow
                {
                    DataContext = mainWindowViewModel
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
