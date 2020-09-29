using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemConsole
{
    public abstract class CardProperty
    {
        public enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

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
