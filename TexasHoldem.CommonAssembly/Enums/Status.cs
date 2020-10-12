using System;

namespace TexasHoldemCommonAssembly.Enums
{
    [Serializable]
    public enum Status
    {
        Connected,
        Disconnected,
        Validated,
        InSession
    }
}