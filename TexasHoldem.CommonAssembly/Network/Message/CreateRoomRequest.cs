﻿using System;
using TexasHoldemCommonAssembly.Network.Message.Base;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class CreateRoomRequest : RoomRequestsMessageBase
    {
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
    }
}