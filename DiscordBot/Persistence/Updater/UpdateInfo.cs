using DiscordBot.Core.Updater;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Persistence.Updater
{
    class UpdateInfo : IUpdateInfo
    {
        public string Version { get; set; } = string.Empty;
        public string DownloadURL { get; set; } = string.Empty;
    }
}
