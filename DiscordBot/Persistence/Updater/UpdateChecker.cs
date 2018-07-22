using DiscordBot.Core.Updater;
using DiscordBot.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Persistence.Updater
{
    class UpdateChecker : IUpdateChecker
    {
        public async Task<bool> CheckForUpdates()
        {
            Console.WriteLine("Checking for updates...");
            if (CheckConnection() == false)
                return false;
            string response = await GetInfo();
            dynamic info = DeserializeResponse(response);
            Info.LatestVersion = GetLatestVersion(info);
            Info.DownloadURL = GetDownloadURL(info);
            return true;
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
        //TODO: Change link to latest releases
        private async Task<string> GetInfo()
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://api.github.com");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
            var response = await client.GetAsync("https://api.github.com/repos/Maissae/NetCoreBot/releases");
            //response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        private dynamic DeserializeResponse(string response)
        {
            return JsonConvert.DeserializeObject(response);
        }

        private string GetLatestVersion(dynamic info)
        {
            return info[0].tag_name;
        }

        private string GetDownloadURL(dynamic info)
        {
            return info[0].assets[0].browser_download_url;
        }
    }
}
