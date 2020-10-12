using System;

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