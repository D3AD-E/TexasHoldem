using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemCommonAssembly.Enums
{
    [Serializable]
    public enum PlayerAction
    {
        Idle,
        Fold,
        Check,
        Call,
        Raise,
        FoldByDisconnect
    }
}
