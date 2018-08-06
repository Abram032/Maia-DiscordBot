using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Updater;
using Maia.Core.Validation;
using Maia.Persistence.Commands;
using Maia.Persistence.Common;
using Maia.Persistence.Settings;
using Maia.Persistence.Updater;
using Maia.Persistence.Validation;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Maia
{
    class Program
    {
        //TODO: Change error messages in validators to something more appropiate.
        //TODO: Global exception handler with full info saved to special log, and basic info shown to user.
        //TODO: Do some changes to the command execution process. Try if making few private methods with different overloads will work out.
        //UNDONE: Check where else I should use protected instead of public.
        public static Stopwatch timer = new Stopwatch();
        private static readonly IConfiguration configuration = new Configuration();
        private static readonly ICommandsInfo commandsInfo = new CommandsInfo();
        private static readonly IValidationHandler validationHandler = new ValidationHandler();
        private static readonly ILogHandler logHandler = new LogHandler(configuration);
        private static readonly IAudioService audioService = new AudioService(logHandler, configuration);
        private static readonly IConnectionHandler connectionHandler = new ConnectionHandler(configuration, logHandler);
        private static readonly IMessageWriter messageWriter = new MessageWriter();
        private static readonly ICommandBuilder commandBuilder = new CommandBuilder(configuration, messageWriter, commandsInfo, connectionHandler, audioService, validationHandler);
        private static readonly ICommandHandler commandHandler = new CommandHandler(configuration, commandBuilder, messageWriter);
        private static readonly IMessageHandler messageHandler = new MessageHandler(configuration, connectionHandler, commandHandler);

        private static readonly IUpdateDownloader updateDownloader = new UpdateDownloader();
        private static readonly IUpdateChecker updateChecker = new UpdateChecker();

        public static void UpdateManager() => new UpdateManager(updateDownloader, updateChecker, Arg).MainAsync().GetAwaiter().GetResult();
        public static void Bot() => new Bot(connectionHandler, configuration, messageHandler).MainAsync().GetAwaiter().GetResult();

        static Task updateManager = new Task(UpdateManager);
        static Task bot = new Task(Bot);        

        public static string Arg { get; private set; } = string.Empty;

        static void Main(string[] args)
        {
            timer.Start();
            Arg = GetArgument(args);
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
