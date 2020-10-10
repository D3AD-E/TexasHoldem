using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using TexasHoldem.CommonAssembly.Game.Entities;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldemCommonAssembly.Network.Message.Base;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class JoinResponse : ResponseMessageBase
    {
        public List<PlayerBase> PlayersInRoom { get; set; }

        public int YourPlace { get; set; }

        public Stack<Pot> Pots { get; set; }

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
