﻿using System;
using TexasHoldemCommonAssembly.Enums;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class PlayerActionServer : ServerMessageBase
    {
        public PlayerAction Action { get; set; }
        public double RaiseAmount { get; set; }

        public int PlayerPos { get; set; }
    }
}