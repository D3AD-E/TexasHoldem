using System;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class AuthenticateResponse : ResponseMessageBase
    {
        public double Money { get; set; }

        public AuthenticateResponse(AuthenticateRequest req)
            : base(req)
        {
        }
    }
}