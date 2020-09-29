using System;
using System.Collections.Generic;
using System.Text;

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
