using System;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class AuthenticateRequest : RequestMessageBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}