﻿using System;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class SignUpRequest : RequestMessageBase
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}