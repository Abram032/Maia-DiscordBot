using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands.Audio
{
    class DismissCommand : BaseCommand, ICommand
    {
        private readonly IAudioService _audioService;
        private readonly IGuild _guild;

        public DismissCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IAudioService audioService, IGuild guild, IValidationHandler validationHandler, params string[] parameters) 
            : base(author, config, channel, messageWriter, validationHandler, parameters)
        {
            _audioService = audioService;
            _guild = guild;
        }

        public override bool CanExecute()
        {
            ICommandValidationContext context = new OwnerOnlyNoParametersValidationContext(_config);
            var result = _validationHandler.Validate(context, this);
            return result.IsSuccessful;
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
