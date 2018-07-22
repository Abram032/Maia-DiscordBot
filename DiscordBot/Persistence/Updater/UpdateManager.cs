using DiscordBot.Core.Updater;
using DiscordBot.Resources;
using System;
using System.Collections.Generic;
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
            if(await _updateChecker.CheckForUpdates() == false)
            {
                Console.WriteLine("Could not check for updates. Please check your internet connection.");
                return;
            }
            if(CompareVersions() == false)
            {
                Console.WriteLine("Update is available. Do you wish to update now? (y/n)");
                if (UserResponse())
                {
                    if(await _downloader.DownloadUpdate() == false)
                    {
                        Console.WriteLine("Could not download update. Please check your internet connection.");
                        return;
                    }

                    await Update();
                }
            }
            Console.WriteLine("Current version: " + Info.version);
            await Task.CompletedTask;
        }

        public async Task Update()
        {
            Console.WriteLine("Updating...");

        }

        private bool CompareVersions()
        {
            if(Info.LatestVersion != null && Info.LatestVersion.Equals(Info.version) == false)
                return false;
            return true;
        }

        private bool UserResponse()
        {
            while(true)
            {
                string reply = Console.ReadLine();
                reply = reply.ToLower();
                reply = reply.Trim();
                if(reply.StartsWith('y'))
                    return true;
                else if(reply.StartsWith('n'))
                    return false;
            }
        }
    }
}
