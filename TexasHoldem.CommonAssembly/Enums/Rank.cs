using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemCommonAssembly.Enums
{
    [Serializable]
    public enum Rank
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfKind,
        Straight,
        Flush,
        FullHouse,
        FourOfKind,
        StraightFlush,
        RoyalFlush
    }
}
