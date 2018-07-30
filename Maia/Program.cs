using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Updater;
using Maia.Persistence.Commands;
using Maia.Persistence.Common;
using Maia.Persistence.Settings;
using Maia.Persistence.Updater;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Maia
{
    class Program
    {
        public static Stopwatch uptime = new Stopwatch();
        static IConfiguration configuration = Configuration.Instance;
        static ICommandsInfo commandsInfo = CommandsInfo.Instance;
        static ILogHandler logHandler = new LogHandler(configuration);
        static IConnectionHandler connectionHandler = new ConnectionHandler(configuration, logHandler);
        static IMessageWriter messageWriter = new MessageWriter();
        static ICommandBuilder commandBuilder = new CommandBuilder(configuration, messageWriter, commandsInfo, connectionHandler);
        static ICommandHandler commandHandler = new CommandHandler(configuration, commandBuilder, messageWriter);
        static IMessageHandler messageHandler = new MessageHandler(configuration, connectionHandler, commandHandler);

        static IUpdateDownloader updateDownloader = new UpdateDownloader();
        static IUpdateChecker updateChecker = new UpdateChecker();

        public static void UpdateManager() => new UpdateManager(updateDownloader, updateChecker, arg).MainAsync().GetAwaiter().GetResult();
        public static void Bot() => new Bot(connectionHandler, configuration, messageHandler).MainAsync().GetAwaiter().GetResult();

        static Task updateManager = new Task(UpdateManager);
        static Task bot = new Task(Bot);        

        public static string arg { get; private set; } = string.Empty;

        static void Main(string[] args)
        {
            uptime.Start();
            arg = GetArgument(args);
            updateManager.Start();
            updateManager.Wait();
            bot.Start();
            bot.Wait();
        }

        static string GetArgument(string[] args)
        {
            if(args.Length > 0)
                return args[0];
            else
                return string.Empty;
        }
    }
}
