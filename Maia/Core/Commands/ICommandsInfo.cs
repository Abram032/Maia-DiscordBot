using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Core.Commands
{
    public interface ICommandsInfo
    {
        //ICommandsInfo Instance { get; }
        List<string> GetCommands();
        string GetCommandHelp(string key);
    }
}
