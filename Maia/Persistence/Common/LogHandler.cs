using Discord;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Common
{
    class LogHandler : ILogHandler
    {
        private IConfiguration _config;
        private string directoryPath = Environment.CurrentDirectory + @"/Logs";
        private string logFilePath = Environment.CurrentDirectory + @"/Logs/" 
            + DateTime.Today.ToShortDateString() + @".log";
        private bool saveLogs;

        public LogHandler(IConfiguration config)
        {
            _config = config;            
        }

        public async Task Handle(LogMessage log)
        {
            saveLogs = GetLoggingConfig();
            if(saveLogs)
            {
                EnsureDirectoryCreated();
                await FileOutput(log);
            }
            await ConsoleOutput(log);
            await Task.CompletedTask;
        }

        private async Task FileOutput(LogMessage log)
        {
            using(StreamWriter sw = new StreamWriter(logFilePath, true))
                await sw.WriteLineAsync(log.ToString());
            await Task.CompletedTask;
        }

        private async Task ConsoleOutput(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            await Task.CompletedTask;
        }

        private bool GetLoggingConfig()
        {
            bool value;
            if(bool.TryParse(_config.GetValue(ConfigKeys.SaveLogs), out value) == false)
                value = bool.Parse(_config.GetDefaultValue(ConfigKeys.SaveLogs));
            return value;
        }

        private void EnsureDirectoryCreated()
        {
            if(Directory.Exists(directoryPath) == false)
                Directory.CreateDirectory(directoryPath);
        }
    }
}
