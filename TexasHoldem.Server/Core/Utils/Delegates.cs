using System;
using System.Collections.Generic;
using System.Text;
using TexasHoldem.Server.Core.Network;

namespace TexasHoldem.Server
{
    public class Delegates
    {
        //public delegate void ClientValidatingDelegate(EventArguments.ClientValidatingEventArgs args);
        public delegate void ClientBasicDelegate(Receiver receiver);
    }
}
