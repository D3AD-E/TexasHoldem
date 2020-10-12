using System;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class AddMoneyRequest : RequestMessageBase
    {
        public string Phrase { get; set; }
    }
}