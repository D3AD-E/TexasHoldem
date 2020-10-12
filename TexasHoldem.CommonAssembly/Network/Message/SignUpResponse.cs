using System;

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