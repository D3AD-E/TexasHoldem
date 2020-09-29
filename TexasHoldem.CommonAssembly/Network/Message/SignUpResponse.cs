using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class SignUpResponse : ResponseMessageBase
    {
        public SignUpResponse(SignUpRequest req)
            : base(req)
        {

        }
    }
}
