using Discord;
using Maia.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Common
{
    class MessageWriter : IMessageWriter
    {
        public async Task Send(string message, IUser author, IMessageChannel channel)
        {
            string _message = author.Mention + " " + message;
            await channel.SendMessageAsync(_message);
        }

        public async Task SendFile(string file, string message, IUser author, IMessageChannel channel)
        {
            string _message = author.Mention + " " + message;
            await channel.SendFileAsync(file, _message);
        }

        public async Task SendFile(Stream file, string filename, string message, IUser author, IMessageChannel channel)
        {
            string _message = author.Mention + " " + message;
            await channel.SendFileAsync(file, filename, _message);
        }
    }
}
