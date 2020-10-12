using System;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class GameEndServer : CardInfoServer
    {
        public bool Showdown { get; set; }
    }
}