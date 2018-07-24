using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Core.Updater
{
    interface IUpdateInfo
    {
        string Version { get; set; }
        string DownloadURL { get; set; }
    }
}
