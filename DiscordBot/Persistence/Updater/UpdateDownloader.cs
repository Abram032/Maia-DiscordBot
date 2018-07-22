using DiscordBot.Core.Updater;
using DiscordBot.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Persistence.Updater
{
    class UpdateDownloader : IUpdateDownloader
    {
        private const string updateFile = "update.zip";
        private readonly string updatePath = Environment.CurrentDirectory;

        public async Task<bool> DownloadUpdate()
        {
            Console.WriteLine("Downloading update...");
            if(CheckConnection() == false)
                return false;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
                client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                await client.DownloadFileTaskAsync(Info.DownloadURL, updateFile);
            }
            ExtractZip();
            return true;
        }

        private void ExtractZip()
        {
            Console.WriteLine("Extracting files...");
            ZipFile.ExtractToDirectory(updateFile, Environment.CurrentDirectory);
            File.Delete(updateFile);
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
    }
}
