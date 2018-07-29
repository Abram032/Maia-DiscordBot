using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Settings
{
    public interface IConfiguration
    {
        void LoadConfig();
        string GetValue(string key);
        string GetDefaultValue(string key);
        string GetValueOrDefault(string key);
    }
}
