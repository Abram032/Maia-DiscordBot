using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Context;

namespace Maia.Persistence.Commands.Misc
{
    class ColorCommand : BaseCommand, ICommand
    {
        Random random = new Random();

        public ColorCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IValidationHandler validationHandler, params string[] parameters)
            : base(author, config, channel, messageWriter, validationHandler, parameters)
        {
        }

        public override bool CanExecute()
        {
            ICommandValidationContext context = new NoParametersValidationContext(_config);
            var result = _validationHandler.Validate(context, this);
            return result.IsSuccessful;
        }

        public override async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                byte r = Generate();
                byte g = Generate();
                byte b = Generate();
                byte[] data = { r,g,b };
                string message = "#" + BitConverter.ToString(data).Replace("-", string.Empty);
                Rgba32 color = new Rgba32(r,g,b);
                Image<Rgba32> image  = new Image<Rgba32>(null, 150, 25, color);
                MemoryStream ms = new MemoryStream();
                image.SaveAsPng(ms);
                image.Dispose();
                ms.Position = 0;
                await _messageWriter.SendFile(ms, "image.png", message, Author, Channel);              
                ms.Close();
                ms.Dispose();
            }
            else
                await InvalidUseOfCommand();
        }

        public byte Generate() => (byte)random.Next(0, 256);
    }
}