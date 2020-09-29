using System;
using System.Collections.Generic;
using System.Linq;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldemServer.Core.Game;
using TexasHoldemServer.Core.Game.Entities;

namespace TexasHoldemServer
{
    public class GameLogic
    {
        public Card[] Deck { get; private set; }

        public List<Card> Board { get; private set; }

        private  PlayerActionController PlayerController { get; set; }

        public double PotSize { get;  private set; }

        public double BBlindBet { get; private set; }

        public double CurrentBet { get; private set; }

        public int MaxPlayerAmount { get; private set; }

        public int CurrentPlayerAmount { get; private set; }

        public string Name { get; private set; }

        private const int DECK_SIZE = 52;

        public GameLogic(string name, int maxPlayerAmount)
        {
            Name = name;
            MaxPlayerAmount = maxPlayerAmount;
            CurrentPlayerAmount = 0;


            SetupDeck();
            PlayerController = new PlayerActionController();
            Board = new List<Card>();
            BBlindBet = 2;
        }

        private void SetupDeck()
        {
            Deck = new Card[DECK_SIZE];
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

        public List<Card> DealCardsToHand(int place)
        {
            var rand = new Random();
            int cardNum;
            var toret = new List<Card>();
            for (int i = 0; i < 2; i++)
            {
                do
                {
                    cardNum = rand.Next(52);
                } while (Deck[cardNum].IsDrawn);
                PlayerController.Players[place].Hand[i] = Deck[cardNum];
                toret.Add(Deck[cardNum]);
                Deck[cardNum].IsDrawn = true;
            }
            return toret;
        }

        private List<Card> DealCardsToBoard(GameState state)
        {
            var rand = new Random();
            int cardNum;
            var toret = new List<Card>();
            int cardsAmount = 0;
            if (state == GameState.Flop)
            {
                cardsAmount = 3;
            }
            else if(state == GameState.Showdown)
            {
                return null;
            }
            else
                cardsAmount = 1;
            for (int i = 0; i < cardsAmount; i++)
            {
                do
                {
                    cardNum = rand.Next(52);
                } while (Deck[cardNum].IsDrawn);
                Board.Add(Deck[cardNum]);
                toret.Add(Deck[cardNum]);
                Deck[cardNum].IsDrawn = true;
            }
            return toret;
        }

        private void DetermineHolding(Player player)
        {
            var toDetermine = new Card[player.Hand.Length + Board.Count()];
            player.Hand.CopyTo(toDetermine, 0);
            Board.CopyTo(toDetermine, player.Hand.Length);

            player.Holding = new Holding(toDetermine);
        }
        public List<PlayerData> GetAllPlayers()
        {
            return PlayerController.Players;
        }
        public List<PlayerData> GetActivePlayers()
        {
            List<PlayerData> suitablePlayers = new List<PlayerData>();
            foreach (var player in PlayerController.Players)
            {
                if (!player.IsPlaying)
                    continue;
                suitablePlayers.Add(player);
            }
            return suitablePlayers;
        }

        private void HandleGameEnd()
        {
            List<PlayerData> suitablePlayers = new List<PlayerData>();
            foreach (var player in PlayerController.Players)
            {
                if (!player.IsPlaying)
                    continue;
                DetermineHolding(player);
                suitablePlayers.Add(player);
            }
            var sorted = suitablePlayers.OrderByDescending(i => i.Holding);

            if (sorted.Count() == 1)
            {
                var Winner = sorted.First();
                Winner.Money += PotSize;
                PotSize = 0;
                return;
            }

            var bestPlayer = sorted.First();

            int winnersAmount = 0;

            foreach (var player in sorted)
            {
                if (player.Holding == bestPlayer.Holding)
                    winnersAmount++;
                else
                    break;
            }

            int i = 0;
            foreach (var player in sorted)
            {
                player.Money += PotSize / winnersAmount;
                i++;
                if (i == winnersAmount)
                    break;
            }
            PotSize = 0;
        }

        public bool HasGameEnded()
        {
            if (PlayerController.HasGameEnded())
            {
                PlayerController.HandleGameEnd();
                HandleGameEnd();
                return true;
            }
            else
                return false;
        }

        public bool HandlePlayerAction(Guid id, PlayerAction action, double bet = 0)
        {
            bet += CurrentBet;
            if (PlayerController.HandleAction(id, action, bet))
            {
                PotSize += PlayerController.GetMoneyToPot(PlayerController.PlayerToAct.Value);
                return true;
            }
            else
                return false;
        }

        public List<Card> GetPendingCardsIfAny()
        {
            if (PlayerController.HasOrbitEnded() || PlayerController.GameState ==GameState.Showdown)
            {
                CurrentBet = 0;
                return DealCardsToBoard(PlayerController.GameState);
            }
            else
                return null;
        }

        public void Start()
        {
            PlayerController.SetupGame(BBlindBet);
            CurrentBet = BBlindBet;
            PotSize = BBlindBet + BBlindBet / 2;
        }

        private void RefreshGame()
        {
            foreach (var card in Deck)
            {
                card.IsDrawn = false;
            }
        }

        public int AddPlayer(Guid id, string username)
        {
            int place = PlayerController.Players.Count();
            PlayerController.Players.Add(new PlayerData(id, place, username));
            CurrentPlayerAmount++;
            return place;
        }

        public List<PlayerBase> GetPlayers() //??????????????????? does not work otherwise, cast from linq
        {
            List<PlayerBase> toret = new List<PlayerBase>();
            foreach(var player in PlayerController.Players)
            {
                toret.Add(new PlayerBase() { Money = player.Money, Place = player.Place, Username = player.Username });
            }
            return toret;
        }

        public int GetCurrentPlayerPlace()
        {
            return PlayerController.PlayerToAct.Value;
        }

        public int GetBBPlace()
        {
            return PlayerController.BBlind.Value;
        }

        public int GetButtonPlace()
        {
            return PlayerController.Button.Value;
        }

        //private void Test()
        //{
        //    foreach (var player in Players)
        //    {
        //        foreach (var card in player.Hand)
        //        {
        //            //Console.WriteLine("Player number " + player.ID + " has " + card.ToString());
        //        }
        //    }

        //    var rand = new Random();
        //    int cardNum;

        //    for (int j = 0; j < 5; j++)
        //    {
        //        do
        //        {
        //            cardNum = rand.Next(52);
        //        } while (Deck[cardNum].isDrawn);
        //        Board[j] = Deck[cardNum];
        //        Deck[cardNum].isDrawn = true;
        //    }

        //    //Board[0] = new Card(CardProperty.Suit.Diamonds, CardProperty.Value.Nine);
        //    //Board[1] = new Card(CardProperty.Suit.Clubs, CardProperty.Value.Nine);
        //    //Board[2] = new Card(CardProperty.Suit.Spades, CardProperty.Value.Seven);
        //    //Board[3] = new Card(CardProperty.Suit.Diamonds, CardProperty.Value.Seven);
        //    //Board[4] = new Card(CardProperty.Suit.Spades, CardProperty.Value.Nine);

        //    Console.WriteLine("Board is:");
        //    foreach (var card in Board)
        //    {
        //        Console.Write(card.ToString() + " ||| ");
        //    }
        //    Console.Write(Environment.NewLine);
        //    DetermineWinner();
        //}

        //private void DetermineWinner()
        //{
        //    foreach(var player in Players.Values)
        //    {
        //        DetermineHolding(player);
        //    }

        //    Player winner = Players[0];
        //    //Bug when more than 2 players and tie
        //    for (int i = 1; i < Players.Count(); i++)
        //    {
        //        int res = winner.Holding.CompareTo(Players[i].Holding);
        //        if (res == 0)
        //        {
        //            Console.WriteLine("Tie with " + winner.Holding.Rank);
        //            return;
        //        }
        //        else if (res <= -1)
        //            winner = Players[i];
        //    }
        //    //Console.WriteLine("Player " + winner.ID + " won with " + winner.Holding.Rank);
        //    //foreach (var player in Players)
        //    //{
        //    //    Console.WriteLine("Player " + player.ID + " had " + player.Holding.Rank);
        //    //}
        //}
    }
}