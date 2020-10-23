using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Utils;

namespace TexasHoldemCommonAssembly.Game.Entities
{
    public class Holding : IComparable<Holding>
    {
        private List<Card> Cards;

        private Card[] CardsInPlay;

        public Rank Rank { get; private set; }

        public Holding(List<Card> cards)
        {
            CardsInPlay = new Card[5];

            Cards = cards;
            Cards.Sort((a, b) => b.CompareTo(a));
            Rank = DetermineRank();
        }

        public override string ToString()
        {
            string toret = Rank.ToString();
            if (Rank == Rank.Pair)
            {
                toret += " of " + CardsInPlay[0].Value.ToString() + "s, " + CardsInPlay[2].Value.ToString() + " kicker";
                return toret;
            }
            else if (Rank == Rank.TwoPair)
            {
                toret += " " + CardsInPlay[0].Value.ToString() + "s and " + CardsInPlay[2].Value.ToString() + "s";
                return toret;
            }
            else if (Rank == Rank.HighCard)
            {
                toret += " " + CardsInPlay[0].Value.ToString();
                return toret;
            }
            else if (Rank == Rank.FourOfKind || Rank == Rank.ThreeOfKind)
            {
                toret += " " + CardsInPlay[0].Value.ToString() + "s";
                return toret;
            }
            else if (Rank == Rank.FullHouse)
            {
                toret += " " + CardsInPlay[0].Value.ToString() + "s full of " + CardsInPlay[3].Value.ToString() + "s";
                return toret;
            }
            else if (Rank == Rank.Straight || Rank == Rank.StraightFlush)
            {
                toret += " from " + CardsInPlay[0].Value.ToString() + " to " + CardsInPlay[4].Value.ToString();
                return toret;
            }
            else if (Rank == Rank.Flush)
            {
                toret += " " + CardsInPlay[0].Value.ToString() + " high";
                return toret;
            }
            else if (Rank == Rank.RoyalFlush)
            {
                return toret.ToUpper();
            }
            else
                return "Unknown";
        }

        public int CompareTo([AllowNull] Holding other)
        {
            if (other == null)
                return 1;
            if (Rank == other.Rank)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (CardsInPlay[i] < other.CardsInPlay[i])
                        return -1;
                    else if (CardsInPlay[i] > other.CardsInPlay[i])
                        return 1;
                }
                return 0;
            }
            else if (Rank < other.Rank)
                return -1;
            else
                return 1;
        }

        private bool isStraight()
        {
            CardProperty.Value currValue = Cards[0].Value;
            int errors = 0;
            int correctCount = 0;
            int cardsAmount = Cards.Count;
            for (int i = 1; i < cardsAmount; i++)
            {
                if (errors >= 2)
                    return false;
                if (currValue == Cards[i].Value)
                    continue;
                if (--currValue != Cards[i].Value)
                {
                    currValue = Cards[i].Value;
                    errors++;
                    correctCount = 0;
                }
                else
                {
                    correctCount++;
                    if (correctCount >= 4)
                    {
                        Cards.CopyTo(i - 4, CardsInPlay, 0, 5);
                        //Array.Copy(Cards, i - 4, CardsInPlay, 0, 5);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool isFlush()
        {
            Card[] temp = Cards.Select(x => (Card)x.Clone()).ToArray();
            temp = temp.OrderBy(item => item.Value).ToArray();

            CardProperty.Suit currSuit = temp[0].Suit;
            int errors = 0;
            int correctCount = 0;
            int cardsAmount = Cards.Count;
            for (int i = 1; i < cardsAmount; i++)
            {
                if (errors >= 2)
                    return false;
                if (currSuit != Cards[i].Suit)
                {
                    currSuit = Cards[i].Suit;
                    errors++;
                    correctCount = 0;
                }
                else
                {
                    correctCount++;
                    if (correctCount >= 4)
                    {
                        Cards.CopyTo(i - 4, CardsInPlay, 0, 5);
                        //Array.Copy(Cards, i - 4, CardsInPlay, 0, 5);
                        Array.Sort(CardsInPlay);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool isStraightFlush(bool straight)
        {
            if (!straight)
                return false;
            Card temp = CardsInPlay[0];
            for (int i = 1; i < CardsInPlay.Length; i++)
            {
                if (temp.Suit != CardsInPlay[i].Suit)
                {
                    return false;
                }
            }
            return true;
        }

        private Rank DetermineRank()
        {
            bool straight = isStraight();
            bool sflush = isStraightFlush(straight);
            bool flush = isFlush();

            if (sflush && Cards[0].Value == CardProperty.Value.Ace)
            {
                return Rank.RoyalFlush;
            }
            else if (sflush)
            {
                return Rank.StraightFlush;
            }
            else if (flush)
            {
                return Rank.Flush;
            }
            else if (straight)
            {
                return Rank.Straight;
            }

            Dictionary<Card, RankHelper> cardCount = new Dictionary<Card, RankHelper>(new CardComparer());
            int cardsAmount = Cards.Count;
            for (int i = 0; i < cardsAmount; i++)
            {
                if (cardCount.ContainsKey(Cards[i]))            //does not compile otherwise why?
                {
                    var temp = cardCount[Cards[i]];
                    temp.Amount++;
                    cardCount[Cards[i]] = temp;
                }
                else
                {
                    var temp = new RankHelper() { Amount = 1, Position = i };
                    cardCount.Add(Cards[i], temp);
                }
            }

            Rank probableRank = Rank.HighCard;
            foreach (var key in cardCount.Keys)
            {
                int amount = cardCount[key].Amount;
                if (amount == 4)
                {
                    if (cardCount[key].Position != 0)
                        CardsInPlay[0] = Cards[0];
                    else
                        CardsInPlay[0] = Cards[4];
                    Cards.CopyTo(cardCount[key].Position, CardsInPlay, 1, 4);
                    //Array.Copy(Cards, cardCount[key].Position, CardsInPlay, 1, 4);
                    return Rank.FourOfKind;
                }
                if (amount == 3)
                {
                    if (probableRank == Rank.ThreeOfKind)
                    {
                        if (key > CardsInPlay[0])
                        {
                            Card[] temp = new Card[2];
                            Array.Copy(CardsInPlay, 0, temp, 0, 2);
                            Cards.CopyTo(cardCount[key].Position, CardsInPlay, 0, 3);
                            //Array.Copy(Cards, cardCount[key].Position, CardsInPlay, 0, 3);
                            Array.Copy(temp, 0, CardsInPlay, 3, 2);
                        }
                        else
                        {
                            Cards.CopyTo(cardCount[key].Position, CardsInPlay, 3, 2);
                            //Array.Copy(Cards, cardCount[key].Position, CardsInPlay, 3, 2);
                        }
                        return Rank.FullHouse;
                    }
                    if (probableRank == Rank.Pair || probableRank == Rank.TwoPair)       //bug 22 aaa 33 will leave 22 aaa or not xd
                    {
                        Card[] temp = new Card[2];
                        Array.Copy(CardsInPlay, 0, temp, 0, 2);
                        Cards.CopyTo(cardCount[key].Position, CardsInPlay, 0, 3);
                        //Array.Copy(Cards, cardCount[key].Position, CardsInPlay, 0, 3);
                        Array.Copy(temp, 0, CardsInPlay, 3, 2);
                        return Rank.FullHouse;
                    }
                    else
                    {
                        Cards.CopyTo(cardCount[key].Position, CardsInPlay, 0, 3);
                        //Array.Copy(Cards, cardCount[key].Position, CardsInPlay, 0, 3);
                        probableRank = Rank.ThreeOfKind;
                    }
                }
                if (amount == 2)
                {
                    if (probableRank == Rank.ThreeOfKind)
                    {
                        Cards.CopyTo(cardCount[key].Position, CardsInPlay, 3, 2);
                        //Array.Copy(Cards, cardCount[key].Position, CardsInPlay, 3, 2);
                        return Rank.FullHouse;
                    }
                    if (probableRank == Rank.Pair)
                    {
                        Cards.CopyTo(cardCount[key].Position, CardsInPlay, 2, 2);
                        //Array.Copy(Cards, cardCount[key].Position, CardsInPlay, 2, 2);
                        probableRank = Rank.TwoPair;
                    }
                    else
                    {
                        Cards.CopyTo(cardCount[key].Position, CardsInPlay, 0, 2);
                        //Array.Copy(Cards, cardCount[key].Position, CardsInPlay, 0, 2);
                        probableRank = Rank.Pair;
                    }
                }
            }
            int counter = 0;
            if (probableRank == Rank.ThreeOfKind)
            {
                int placeToAdd = 3;
                int hit = cardCount[CardsInPlay[0]].Position;

                for (int i = 0; i < cardsAmount; i++)
                {
                    if (counter >= 2)
                        break;
                    if (i == hit)
                    {
                        i += 2;
                        continue;
                    }
                    CardsInPlay[placeToAdd] = Cards[i];
                    counter++;
                    placeToAdd++;
                }
            }
            else if (probableRank == Rank.TwoPair)
            {
                int hit1 = cardCount[CardsInPlay[0]].Position;
                int hit2 = cardCount[CardsInPlay[2]].Position;
                for (int i = 0; i < cardsAmount; i++)
                {
                    if (i == hit1 || i == hit2)
                    {
                        i++;
                        continue;
                    }
                    CardsInPlay[4] = Cards[i];
                    break;
                }
            }
            else if (probableRank == Rank.Pair)
            {
                int placeToAdd = 2;
                int hit = cardCount[CardsInPlay[0]].Position;
                for (int i = 0; i < cardsAmount; i++)
                {
                    if (counter >= 3)
                        break;
                    if (i == hit)
                    {
                        i++;
                        continue;
                    }
                    CardsInPlay[placeToAdd] = Cards[i];
                    counter++;
                    placeToAdd++;
                }
            }
            else
                Cards.CopyTo(0, CardsInPlay, 0, 5);
            //Array.Copy(Cards, 0, CardsInPlay, 0, 5);
            return probableRank;
        }

        private struct RankHelper
        {
            public int Amount;
            public int Position;
        }

        public static bool operator <(Holding holding1, Holding holding2)
        {
            return holding1.CompareTo(holding2) < 0;
        }

        public static bool operator >(Holding holding1, Holding holding2)
        {
            return holding1.CompareTo(holding2) > 0;
        }
    }
}