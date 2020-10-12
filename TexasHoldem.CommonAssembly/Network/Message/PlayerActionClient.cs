using System;
using TexasHoldemCommonAssembly.Enums;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class PlayerActionClient : ClientMessageBase
    {
        public PlayerAction Action { get; set; }
        public double RaiseAmount { get; set; }
    }
}