using Maia.Core.Updater;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Updater
{
    class UpdateDownloader : IUpdateDownloader
    {
        private const string zipPath = "update.zip";
        private readonly string extractPath = Environment.CurrentDirectory;

        public async Task DownloadUpdate(IUpdateInfo updateInfo)
        {
            Console.WriteLine("Downloading update...");
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
                client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                await client.DownloadFileTaskAsync(updateInfo.DownloadURL, zipPath);
            }
        }

        public void ExtractZip()
        {
            if(File.Exists(zipPath))
            {
                ZipFile.ExtractToDirectory(zipPath, Environment.CurrentDirectory);
                File.Delete(zipPath);
            }           
        }
    }
}
