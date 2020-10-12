using System;
using System.Collections.Generic;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class ShowRoomsResponse : ResponseMessageBase
    {
        public List<Room> Rooms { get; set; }

        public ShowRoomsResponse(ShowRoomsRequest req)
            : base(req)
        {
        }
    }
}