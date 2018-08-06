using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Audio;
using Discord.Commands;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Context;

namespace Maia.Persistence.Commands.Audio
{
    class SummonCommand : BaseCommand, ICommand
    {
        IAudioService _audioService;
        IGuild _guild;

        public SummonCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IAudioService audioService, IGuild guild, IValidationHandler validationHandler, params string[] parameters)
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
                var voiceChannel = await GetUserVoiceChannel();
                if(voiceChannel == null)
                {
                    //Throw exception usernotinchannel.
                }
                else
                {
                    await _audioService.JoinAudioChannel(_guild, voiceChannel);
                    await SendMessageAsync("I'm comming!");
                }
            }
            else
                await InvalidUseOfCommand();
        }

        private async Task<IVoiceChannel> GetUserVoiceChannel()
        {
            var voiceChannels = await _guild.GetVoiceChannelsAsync();
            foreach (var voiceChannel in voiceChannels)
            {
                var users = await voiceChannel.GetUsersAsync().FlattenAsync();
                foreach (var user in users)
                {
                    if(user.Id == Author.Id)
                        return voiceChannel;
                }
            }
            return null;
        }
    }
}
