using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldem.Client.Core.Network
{
    public class ResponseCallbackObject
    {
        public Delegate CallBack { get; set; }
        public Guid ID { get; set; }
    }
}
