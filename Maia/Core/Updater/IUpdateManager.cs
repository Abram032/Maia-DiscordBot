using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Updater
{
    interface IUpdateManager
    {
        Task Update(string updateDirPath);
    }
}
