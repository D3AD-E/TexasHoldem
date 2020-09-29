using System;
using System.Collections.Generic;
using System.Text;
using TexasHoldemServer.Core.Network;

namespace TexasHoldemServer
{
    public class Delegates
    {
        //public delegate void ClientValidatingDelegate(EventArguments.ClientValidatingEventArgs args);
        public delegate void ClientBasicDelegate(Receiver receiver);
    }
}
