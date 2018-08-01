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

namespace Maia.Persistence.Commands
{
    class SummonCommand : BaseCommand, ICommand
    {
        IAudioService _audioService;
        IGuild _guild;

        public SummonCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IAudioService audioService, IGuild guild, params string[] parameters)
            : base(author, config, channel, messageWriter, parameters)
        {
            _audioService = audioService;
            _guild = guild;
        }

        public override async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                var voiceChannel = await GetUserVoiceChannel();
                if(voiceChannel == null)
                {
                    //TODO: Throw exception usernotinchannel.
                }
                else
                {
                    await _audioService.JoinAudioChannel(_guild, voiceChannel);
                    await Reply("I'm comming!");
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
                    if(user.Id == _author.Id)
                        return voiceChannel;
                }
            }
            return null;
        }
    }
}
