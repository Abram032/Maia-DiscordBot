using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.Updater
{
    interface IUpdateChecker
    {
        Task<IUpdateInfo> CheckForUpdates();
    }
}
