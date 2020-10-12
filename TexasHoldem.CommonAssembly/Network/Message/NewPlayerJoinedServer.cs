using System;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class NewPlayerJoinedServer : ServerMessageBase
    {
        public PlayerBase Player { get; set; }
    }
}