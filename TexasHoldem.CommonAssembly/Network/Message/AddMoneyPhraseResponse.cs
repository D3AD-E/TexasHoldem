using System;

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