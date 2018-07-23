using DiscordBot.Core.Updater;
using DiscordBot.Resources;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Persistence.Updater
{
    class UpdateManager : IUpdateManager
    {
        IUpdateCleaner _cleaner;
        IUpdateDownloader _downloader;
        IUpdateChecker _updateChecker;

        public UpdateManager(IUpdateCleaner cleaner, IUpdateDownloader downloader, IUpdateChecker updateChecker)
        {
            _cleaner = cleaner;
            _downloader = downloader;
            _updateChecker = updateChecker;
        }

        public async Task MainAsync()
        {
            if(CheckConnection() == false)
                Console.WriteLine("Could not check for updates. Please check your internet connection.");               
            else
            {
                Console.WriteLine("Checking for updates...");
                IUpdateInfo updateInfo = await _updateChecker.CheckForUpdates();
                if(CompareVersions(updateInfo) == false)
                    Console.WriteLine("No new updates found.");
                else
                {
                    Console.WriteLine("New update is available.");
                    Console.WriteLine("Update version: " + updateInfo.Version);
                    Console.WriteLine("Current version: " + Info.version);
                    Console.WriteLine("Do you wish to download update now? (y/n)");
                    if(UserResponse())
                    {
                        Console.WriteLine();
                        await _downloader.DownloadUpdate(updateInfo);
                        Console.WriteLine("Extracting files...");
                        _downloader.ExtractZip();
                        await Update();
                    }
                }
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Current version: " + Info.version);
            await Task.CompletedTask;
        }

        public async Task Update()
        {
            Console.WriteLine("Updating...");

        }

        private bool CheckConnection()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.github.com/");
            request.ContentType = "application/json";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }

        private bool CompareVersions(IUpdateInfo updateInfo)
        {
            return (updateInfo.Version != string.Empty && updateInfo.Version.Equals(Info.version) == false);
        }

        private bool UserResponse()
        {
            return Console.ReadKey().Key.Equals(ConsoleKey.Y);
        }
    }
}
