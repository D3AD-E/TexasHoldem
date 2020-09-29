using System;
using System.Collections.Generic;
using System.Text;
using TexasHoldemCommonAssembly.Network.Message.Base;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class JoinRoomRequest :RoomRequestsMessageBase
    {
        public Guid RoomId { get; set; }
    }
}
