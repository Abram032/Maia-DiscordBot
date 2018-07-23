using DiscordBot.Persistence.Updater;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Core.Updater
{
    interface IUpdateInfo
    {
        string Version { get; set; }
        string DownloadURL { get; set; }
    }
}
