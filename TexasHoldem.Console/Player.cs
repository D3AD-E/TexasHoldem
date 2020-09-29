using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemConsole
{
    class Player 
    {
        public Card[] Hand { get; private set; }

        public Holding Holding { get; private set; }

        public ulong Id { get; private set; }

        public Player(ulong id)
        {
            Hand = new Card[2];
            Id = id;
        }

        public Player(Card fCard, Card bCard, ulong id)
        {
            Hand = new Card[2];
            Hand[0] = fCard;
            Hand[1] = bCard;
            Id = id;
        }

        public void GetCards(Card[] deck)
        {
            var rand = new Random();
            int cardNum;
            for(int i = 0; i<2;i++)
            {
                do
                {
                    cardNum = rand.Next(52);
                } while (deck[cardNum].isDrawn);
                Hand[i] = deck[cardNum];
                deck[cardNum].isDrawn = true;
            }
        }

        public void ShowdownHolding(Card[] board)
        {
            var toDetermine = new Card[Hand.Length + board.Length];
            Hand.CopyTo(toDetermine, 0);
            board.CopyTo(toDetermine, Hand.Length);

            Holding = new Holding(toDetermine);
        }
    }
}
