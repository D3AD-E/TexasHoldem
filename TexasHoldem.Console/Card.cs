using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TexasHoldemConsole
{
    public class Card : IComparable<Card>, IEquatable<Card>, ICloneable
    {
        public CardProperty.Suit Suit { get; private set; }

        public CardProperty.Value Value { get; private set; }

        public bool isDrawn { get; set; }
        public Card(CardProperty.Suit suit, CardProperty.Value value)
        {
            Suit = suit;
            Value = value;
            isDrawn = false;
        }

        public override string ToString()
        {
            return Value.ToString()+ " of "+Suit.ToString();
        }

        public int CompareTo([AllowNull] Card other)
        {
            if (other == null)
                return 1;
            if (Value > other.Value)
                return 1;
            if (Value < other.Value)
                return -1;
            else
                return 0;
        }

        public bool Equals([AllowNull] Card other)
        {
            if (other == null)
                return false;
            return Value == other.Value;
        }

        public object Clone()
        {
            Card toret = new Card(this.Suit, this.Value);
            return toret;
        }

        public static bool operator <(Card c1, Card c2)
        {
            return c1.CompareTo(c2) < 0;
        }

        public static bool operator >(Card c1, Card c2)
        {
            return c1.CompareTo(c2) > 0;
        }
    }
}
