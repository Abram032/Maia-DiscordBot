using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Persistence.Settings;
using Maia.Resources;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maia.Persistence.Commands
{
    public class CommandsInfo : ICommandsInfo
    {
        private readonly ConcurrentDictionary<string, string> Commands;

        public CommandsInfo()
        {
            Commands = InitiateDictionary();
        }
        
        public string GetCommandHelp(string key) => Commands[key];
        //TODO: Check if this works correctly.
        public bool TryGetCommandHelp(string key, out string value)
        {
            if(Commands.TryGetValue(key, out value))
                return true;
            return false;
        }

        public List<string> GetCommands() => Commands.Keys.ToList();

        private ConcurrentDictionary<string, string> InitiateDictionary()
        {
            ConcurrentDictionary<string, string> keyValuePairs = new ConcurrentDictionary<string, string>();
            keyValuePairs.TryAdd(CommandNames.exit, "Stops bot process. \nUsage: exit");
            keyValuePairs.TryAdd(CommandNames.help, "Shows help. \nUsage: help, help <command>");
            keyValuePairs.TryAdd(CommandNames.github, "Shows URL to bot's code on GitHub. \nUsage: github");
            keyValuePairs.TryAdd(CommandNames.random, "Generates random integer value, between 0 and 100. \nUsage: random, random [max], random [min] [max]");
            keyValuePairs.TryAdd(CommandNames.restart, "Restarts bot process. \nUsage: restart");
            keyValuePairs.TryAdd(CommandNames.update, "Updates bot and restarts it. \nUsage: update");
            keyValuePairs.TryAdd(CommandNames.version, "Shows current version of the bot. \nUsage: version");
            keyValuePairs.TryAdd(CommandNames.uptime, "Shows for how long bot has been running without stopping. \nUsage: uptime");
            keyValuePairs.TryAdd(CommandNames.flipcoin, "Flips a coin. \nUsage: flipcoin");
            keyValuePairs.TryAdd(CommandNames.color, "Generates random color in hex with preview. \nUsage: color");
            keyValuePairs.TryAdd(CommandNames.dismiss, "Bot leaves voice channel if it's in any. \nUsage: dismiss");
            keyValuePairs.TryAdd(CommandNames.purge, "Removes messages sent by bot and command calls sent by users. \nUsage: purge, purge [amount]");
            keyValuePairs.TryAdd(CommandNames.roll, "Rolls a dice. Max up to 10 dices. \nUsage: roll [size], roll [size] [number of dices]");
            keyValuePairs.TryAdd(CommandNames.summon, "Bot joins voice channel of user who summoned it. \nUsage: summon");
            return keyValuePairs;
        }
    }
}
