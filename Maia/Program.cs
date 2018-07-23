using DiscordBot.Core.Commands;
using DiscordBot.Core.Common;
using DiscordBot.Core.Settings;
using DiscordBot.Core.Updater;
using DiscordBot.Persistence.Commands;
using DiscordBot.Persistence.Common;
using DiscordBot.Persistence.Settings;
using DiscordBot.Persistence.Updater;
using System;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Program
    {
        static IConfiguration configuration = Configuration.Instance;
        static ILogHandler logHandler = new LogHandler(configuration);
        static IConnectionHandler connectionHandler = new ConnectionHandler(configuration, logHandler);
        static IMessageWriter messageWriter = new MessageWriter();
        static ICommandBuilder commandBuilder = new CommandBuilder(configuration, messageWriter, connectionHandler);
        static ICommandHandler commandHandler = new CommandHandler(configuration, commandBuilder, messageWriter);
        static IMessageHandler messageHandler = new MessageHandler(configuration, connectionHandler, commandHandler);

        static IUpdateDownloader updateDownloader = new UpdateDownloader();
        static IUpdateChecker updateChecker = new UpdateChecker();
        static IUpdateCleaner updateCleaner = new UpdateCleaner();

        public static void Bot() => new Bot(connectionHandler, configuration, messageHandler).MainAsync().GetAwaiter().GetResult();
        public static void UpdateManager() => new UpdateManager(updateCleaner, updateDownloader, updateChecker).MainAsync().GetAwaiter().GetResult();
        static Task bot = new Task(Bot);
        static Task updateManager = new Task(UpdateManager);

        public static string arg { get; private set; } = string.Empty;

        static void Main(string[] args)
        {
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
