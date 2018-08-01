using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Common
{
    interface IAudioService
    {
        Task JoinAudioChannel(IGuild guild, IVoiceChannel voiceChannel);
        Task LeaveAudioChannel(IGuild guild);
        Task PlayAudioAsync(IGuild guild, IMessageChannel channel, string path);
        Task PauseAudioAsync(IGuild guild);
        Task ResumeAudioAsync(IGuild guild);
    }
}
