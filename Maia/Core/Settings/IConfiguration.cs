using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.Settings
{
    interface IConfiguration
    {
        Task LoadConfig();
        string GetValue(string key);
        string GetDefaultValue(string key);
    }
}
