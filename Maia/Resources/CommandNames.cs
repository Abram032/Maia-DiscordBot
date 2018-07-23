using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Resources
{
    class CommandNames
    {
        public static readonly List<string> commands = InitList();
        public const string random = "random";
        public const string exit = "exit";
        public const string help = "help";
        public const string info = "info";
        public const string restart = "restart";
        //public const string purge = "purge";

        private static List<string> InitList()
        {
            List<string> commands = new List<string>();
            commands.Add(random);
            commands.Add(exit);
            commands.Add(help);
            commands.Add(restart);
            commands.Add(info);
            //commands.Add(purge);
            commands.Sort();
            return commands;
        }
    }
}
