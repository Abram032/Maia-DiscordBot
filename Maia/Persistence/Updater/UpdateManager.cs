using Maia.Core.Updater;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Updater
{
    class UpdateManager : IUpdateManager
    {
        IUpdateDownloader _downloader;
        IUpdateChecker _updateChecker;
        private string Arg { get; set; }

        public UpdateManager(IUpdateDownloader downloader, IUpdateChecker updateChecker, string arg)
        {
            _downloader = downloader;
            _updateChecker = updateChecker;
            Arg = arg;
        }

        public async Task MainAsync()
        {
            if(CheckConnection() == false)
                Console.WriteLine("Could not check for updates. Please check your internet connection.");               
            else
            {
                Console.WriteLine("Checking for updates...");
                IUpdateInfo updateInfo = await _updateChecker.CheckForUpdates();                
                if(CompareVersions(updateInfo, Info.version) == false)
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
                        string updateDirPath = "Maia-DiscordBot-" + updateInfo.Version;
                        if(Directory.Exists(updateDirPath))
                            await Update(updateDirPath);
                        else
                            Console.WriteLine("Could not find update directory!");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Current version: " + Info.version);
        }

        public async Task Update(string updateDirPath)
        {
            Console.WriteLine("Application will now close in order to update.");
            await Task.Delay(3000);
            var processPID = Process.GetCurrentProcess().Id;
            var dir = Environment.CurrentDirectory + @"/";
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Process.Start(dir + "UpdateSetup.bat", updateDirPath + " " + processPID);
            else
                Process.Start(dir + "UpdateSetup.sh", updateDirPath + " " + processPID);
            Environment.Exit(0);
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

        private bool CompareVersions(IUpdateInfo updateInfo, string version)
        {
            return (updateInfo.Version != string.Empty && updateInfo.Version.Equals(Info.version) == false);
        }

        private bool UserResponse()
        {
            if(Arg.Equals("-y"))
                return true;
            else
                return Console.ReadLine().StartsWith('y');
        }
    }
}
