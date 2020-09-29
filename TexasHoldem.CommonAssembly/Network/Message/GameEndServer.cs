using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class GameEndServer : CardInfoServer
    {
        public bool Showdown { get; set; }
    }
}
