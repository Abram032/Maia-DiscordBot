using Maia.Core.Updater;
using Maia.Persistence.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Maia.Persistence.Updater
{
    class UpdateChecker : IUpdateChecker
    {
        public async Task<IUpdateInfo> CheckForUpdates()
        {
            string json = await GetInfo();
            return DeserializeResponse(json);
        }

        //TODO: Change link to latest releases
        private async Task<string> GetInfo()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
            var response = await client.GetAsync("https://api.github.com/repos/Maissae/Maia-DiscordBot/releases");
            //response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        private IUpdateInfo DeserializeResponse(string json)
        {
            var infoList = JsonConvert.DeserializeObject<List<IUpdateInfo>>(json, new UpdateInfoConverter());
            if(infoList.Count == 0)
                return new UpdateInfo();
            return infoList[0];
        }
    }
}
