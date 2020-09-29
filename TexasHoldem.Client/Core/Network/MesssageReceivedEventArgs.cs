using System;
using System.Collections.Generic;
using System.Text;
using TexasHoldemCommonAssembly.Network.Message;

namespace TexasHoldem.Client.Core.Network
{
    public class MesssageReceivedEventArgs
    {
        public ServerMessageBase Message { get; private set; }

        public MesssageReceivedEventArgs(ServerMessageBase msg)
        {
            Message = msg;
        }
    }
}
