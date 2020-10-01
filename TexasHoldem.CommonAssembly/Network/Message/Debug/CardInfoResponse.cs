using System;
using System.Collections.Generic;
using System.Text;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class CardInfoResponse : ResponseMessageBase
    {
        public List<Card> Cards { get; set; }

        public bool ToHand { get; set; }

        public CardInfoResponse(CardInfoRequest request)
            : base(request)
        {

        }
    }
}
