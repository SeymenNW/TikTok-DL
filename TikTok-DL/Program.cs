using Avalonia;
using System;
using System.Threading.Tasks;

namespace TikTokDL
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] args)
        {


            //IMPORTANT:
            /*
             The Playwright library will be added in a future release for webscraping and other features
            
             
             */

            // The following line (if uncommented) installs the default browsers. If you only need a subset of browsers,
            // you can specify the list of browsers you want to install among: chromium, chrome,
            // chrome-beta, msedge, msedge-beta, msedge-dev, firefox, and webkit.
            // var exitCode = Microsoft.Playwright.Program.Main(new[] { "install", "webkit", "chrome" });
            // If you need to install dependencies, you can add "--with-deps"
            //var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
            //if (exitCode != 0)
            //{
            //    Console.WriteLine("Failed to install browsers");
            //    Environment.Exit(exitCode);
            //}




            // Start your Avalonia app
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}
