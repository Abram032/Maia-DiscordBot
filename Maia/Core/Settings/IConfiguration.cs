using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Settings
{
    interface IConfiguration
    {
        Task LoadConfig();
        string GetValue(string key);
        string GetDefaultValue(string key);
    }
}
