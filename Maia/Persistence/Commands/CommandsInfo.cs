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
        //TODO: Remove singleton and make dictionary concurrent to make it threadsafe.
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
            keyValuePairs.Add(CommandNames.flipcoin, "Flips a coin. \nUsage: flipcoin");
            keyValuePairs.Add(CommandNames.color, "Generates random color in hex with preview. \nUsage: color");
            keyValuePairs.Add(CommandNames.dismiss, "Bot leaves voice channel if it's in any. \nUsage: dismiss");
            keyValuePairs.Add(CommandNames.purge, "Removes messages sent by bot and command calls sent by users. \nUsage: purge, purge [amount]");
            keyValuePairs.Add(CommandNames.roll, "Rolls a dice. Max up to 10 dices. \nUsage: roll [size], roll [size] [number of dices]");
            keyValuePairs.Add(CommandNames.summon, "Bot joins voice channel of user who summoned it. \nUsage: summon");
            return keyValuePairs;
        }
    }
}
