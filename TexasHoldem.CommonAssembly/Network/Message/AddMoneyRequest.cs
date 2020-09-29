using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class AddMoneyRequest : RequestMessageBase
    {
        public string Phrase { get; set; }
    }
}
