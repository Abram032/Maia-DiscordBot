using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Updater
{
    public interface IUpdateChecker
    {
        Task<IUpdateInfo> CheckForUpdates();
    }
}
