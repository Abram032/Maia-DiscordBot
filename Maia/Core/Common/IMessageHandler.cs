﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Common
{
    interface IMessageHandler
    {
        Task Handle();
    }
}
