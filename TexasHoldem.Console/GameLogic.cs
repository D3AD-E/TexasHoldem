using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TexasHoldemConsole
{
    class GameLogic
    {
        public Card[] Deck { get; private set; }

        public Card[] Board { get; private set; }

        public List<Player> Players { get; private set; }

        private const int DeckSize = 52;

        public GameLogic()
        {
            SetupDeck();
            Players = new List<Player>();
            Board = new Card[5];

            for (int i = 0; i < 2; i++)
                Players.Add(new Player((ulong)i));
            DealCards();

            //Card togive1 = new Card(CardProperty.Suit.Diamonds, CardProperty.Value.Five);
            //Card togive2 = new Card(CardProperty.Suit.Clubs, CardProperty.Value.Two);
            //Players.Add(new Player(togive1, togive2, 1));
            //Card togive3 = new Card(CardProperty.Suit.Spades, CardProperty.Value.Four);
            //Card togive4 = new Card(CardProperty.Suit.Diamonds, CardProperty.Value.Seven);
            //Players.Add(new Player(togive3, togive4, 2));

            Test();
        }

        private void SetupDeck()
        {
            Deck = new Card[DeckSize];
            var suits = Enum.GetValues(typeof(CardProperty.Suit)).Cast<CardProperty.Suit>();
            var cardValues = Enum.GetValues(typeof(CardProperty.Value)).Cast<CardProperty.Value>();
            int currCard = 0;
            foreach (var suit in suits)
                foreach (var cValue in cardValues)
                {
                    Deck[currCard] = new Card(suit, cValue);
                    currCard++;
                }
        }

        private void DealCards()
        {
            foreach(var player in Players)
            {
                player.GetCards(Deck);
            }
        }

        private void Test()
        {
            foreach (var player in Players)
            {
                foreach (var card in player.Hand)
                {
                    Console.WriteLine("Player number " + player.Id + " has " + card.ToString());
                }
            }

            var rand = new Random();
            int cardNum;

            for (int j = 0; j < 5; j++)
            {
                do
                {
                    cardNum = rand.Next(52);
                } while (Deck[cardNum].isDrawn);
                Board[j] = Deck[cardNum];
                Deck[cardNum].isDrawn = true;
            }


            //Board[0] = new Card(CardProperty.Suit.Diamonds, CardProperty.Value.Nine);
            //Board[1] = new Card(CardProperty.Suit.Clubs, CardProperty.Value.Nine);
            //Board[2] = new Card(CardProperty.Suit.Spades, CardProperty.Value.Seven);
            //Board[3] = new Card(CardProperty.Suit.Diamonds, CardProperty.Value.Seven);
            //Board[4] = new Card(CardProperty.Suit.Spades, CardProperty.Value.Nine);

            Console.WriteLine("Board is:");
            foreach(var card in Board)
            {
                Console.Write(card.ToString() + " ||| ");
            }
            Console.Write(Environment.NewLine);
            DetermineWinner();
        }

        private void DetermineWinner()
        {
            foreach (var player in Players)
            {
                player.ShowdownHolding(Board);
            }
            Player winner = Players[0];
            //Bug when more than 2 players and tie
            for(int i = 1; i< Players.Count(); i++)
            {
                int res = winner.Holding.CompareTo(Players[i].Holding);
                if(res == 0)
                {
                    Console.WriteLine("Tie with " + winner.Holding.Rank);
                    return;
                }
                else if (res <=-1)
                    winner = Players[i];
            }
            Console.WriteLine("Player "+winner.Id+" won with "+winner.Holding.Rank);
            foreach(var player in Players)
            {
                Console.WriteLine("Player " + player.Id + " had " + player.Holding.Rank);
            }
        }
    }
}
