using System;

namespace TexasHoldemCommonAssembly.Enums
{
    [Serializable]
    public abstract class CardProperty
    {
        [Serializable]
        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }

        [Serializable]
        public enum Value
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }
    }
}