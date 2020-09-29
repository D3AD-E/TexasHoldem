using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class AddMoneyPhraseResponse : ResponseMessageBase
    {
        public string Phrase { get; set; }
        public AddMoneyPhraseResponse(AddMoneyPhraseRequest req)
            : base(req)
        {

        }
    }
}
