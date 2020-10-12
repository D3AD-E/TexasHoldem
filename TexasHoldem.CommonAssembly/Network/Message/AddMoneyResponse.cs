using System;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class AddMoneyResponse : ResponseMessageBase
    {
        public AddMoneyResponse(AddMoneyRequest req)
            : base(req)
        {
        }
    }
}