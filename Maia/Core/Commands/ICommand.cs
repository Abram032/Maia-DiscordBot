using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Commands
{
    public interface ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
}
