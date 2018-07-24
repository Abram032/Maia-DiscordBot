using Maia.Core.Updater;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Updater
{
    class UpdateInfo : IUpdateInfo
    {
        public string Version { get; set; } = string.Empty;
        public string DownloadURL { get; set; } = string.Empty;
    }
}
