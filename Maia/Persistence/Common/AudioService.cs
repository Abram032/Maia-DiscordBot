using Discord;
using Discord.Audio;
using Discord.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Common
{
    class AudioService : IAudioService
    {
        private ILogHandler _logHandler;
        private readonly ConcurrentDictionary<ulong, IAudioClient> ConnectedChannels = new ConcurrentDictionary<ulong, IAudioClient>();

        public AudioService(ILogHandler logHandler, IConfiguration config)
        {
            _logHandler = logHandler;
        }
        
        [Command("summon", RunMode = RunMode.Async)]
        public async Task JoinAudioChannel(IGuild guild, IVoiceChannel target)
        {
            //TODO: Throw exceptions beside returns.
            IAudioClient client;
            if (ConnectedChannels.TryGetValue(guild.Id, out client))
            {
                return;
            }
            if (target.Guild.Id != guild.Id)
            {
                return;
            }

            var audioClient = await target.ConnectAsync();

            if (ConnectedChannels.TryAdd(guild.Id, audioClient))
            {
                LogMessage logMessage = new LogMessage(LogSeverity.Info, "Audio", $"Connected to voice on {guild.Name}.");
                await _logHandler.Handle(logMessage);
                return;
            }
            else
            {
                //Something went wrong 
                return;
            }
        }

        public async Task LeaveAudioChannel(IGuild guild)
        {
            IAudioClient client;
            if (ConnectedChannels.TryRemove(guild.Id, out client))
            {
                await client.StopAsync();
                LogMessage logMessage = new LogMessage(LogSeverity.Info, "Audio", $"Disconnected from voice on {guild.Name}.");
                await _logHandler.Handle(logMessage);
                return;
            }
            else
            {
                //Something went very wrong;
                return;
            }
        }

        public async Task PlayAudioAsync(IGuild guild, IMessageChannel channel, string path)
        {
            if (!File.Exists(path))
            {
                await channel.SendMessageAsync("File does not exist!");
                IAudioClient client;
                if (ConnectedChannels.TryGetValue(guild.Id, out client))
                {
                    //await Log(LogSeverity.Debug, $"Starting playback of {path} in {guild.Name}");
                    using (var ffmpeg = CreateProcess(path))
                    using (var stream = client.CreatePCMStream(AudioApplication.Music))
                    {
                        try { await ffmpeg.StandardOutput.BaseStream.CopyToAsync(stream); }
                        finally { await stream.FlushAsync(); }
                    }
                }
            }
        }

        private Process CreateProcess(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true
            });
        }

        public Task PauseAudioAsync(IGuild guild)
        {
            throw new NotImplementedException();
        }

        public Task ResumeAudioAsync(IGuild guild)
        {
            throw new NotImplementedException();
        }
    }
}
