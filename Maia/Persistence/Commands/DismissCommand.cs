using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands
{
    class DismissCommand : BaseCommand, ICommand
    {
        private IAudioService _audioService;
        private IGuild _guild;

        public DismissCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IAudioService audioService, IGuild guild, params string[] parameters) 
            : base(author, config, channel, messageWriter, parameters)
        {
            _audioService = audioService;
            _guild = guild;
        }

        public override async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                await _audioService.LeaveAudioChannel(_guild);
            }
            else
                await InvalidUseOfCommand();
        }
    }
}
