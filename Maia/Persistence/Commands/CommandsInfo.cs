using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Persistence.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maia.Persistence.Commands
{
    public class CommandsInfo : ICommandsInfo
    {
        private readonly Dictionary<string, string> Commands;
        private static readonly object syncRoot = new object();
        private static CommandsInfo instance = null;

        private CommandsInfo()
        {
            Commands = HydrateDictionary();
        }

        public static ICommandsInfo Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new CommandsInfo();
                }
                return instance;
            }
        }

        public string GetCommandHelp(string key) => Commands[key];

        public List<string> GetCommands() => Commands.Keys.ToList();

        private Dictionary<string, string> HydrateDictionary()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add(CommandNames.exit, "Stops bot process. \nUsage: exit");
            keyValuePairs.Add(CommandNames.help, "Shows help. \nUsage: help, help <command>");
            keyValuePairs.Add(CommandNames.github, "Shows URL to bot's code on GitHub. \nUsage: github");
            keyValuePairs.Add(CommandNames.random, "Generates random integer value, between 0 and 100. \nUsage: random, random [max], random [min] [max]");
            keyValuePairs.Add(CommandNames.restart, "Restarts bot process. \nUsage: restart");
            keyValuePairs.Add(CommandNames.update, "Updates bot and restarts it. \nUsage: update");
            keyValuePairs.Add(CommandNames.version, "Shows current version of the bot. \nUsage: version");
            keyValuePairs.Add(CommandNames.uptime, "Shows for how long bot has been running without stopping. \nUsage: uptime");
            return keyValuePairs;
        }
    }
}
