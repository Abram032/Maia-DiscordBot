using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Updater
{
    public interface IUpdateDownloader
    {
        Task DownloadUpdate(IUpdateInfo updateInfo);
        void ExtractZip();
    }
}
