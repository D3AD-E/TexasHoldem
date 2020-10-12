using System;

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