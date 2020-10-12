using System;
using TexasHoldemCommonAssembly.Network.Message;

namespace TexasHoldem.CommonAssembly.Network.Message
{
    [Serializable]
    public class HeartbeatResponse : ResponseMessageBase
    {
        public HeartbeatResponse(HeartbeatRequest req) : base(req)
        {
        }
    }
}