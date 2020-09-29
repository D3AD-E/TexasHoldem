using System;
using System.Collections.Generic;
using System.Text;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldemCommonAssembly.Network.Message.Base;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class JoinResponse : ResponseMessageBase
    {
        public List<PlayerBase> PlayersInRoom { get; set; }

        public int YourPlace { get; set; }

        public double PotSize { get; set; }

        public JoinResponse(JoinRequest req)
            : base(req)
        {

        }

        public JoinResponse(RoomRequestsMessageBase req)
            : base(req)
        {

        }
    }
}
