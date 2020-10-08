using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TexasHoldem.Server.Core.Game;
using TexasHoldem.Server.Core.Game.Entities;
using TexasHoldem.Server.Core.Network;
using TexasHoldem.Server.Enums;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldem.Server
{
    public class GameLogic
    {
        public Card[] Deck { get; private set; }

        public List<Card> Board { get; private set; }

        private PlayerActionController PlayerController { get; set; }

        public double PotSize { get; private set; }

        public double BBlindBet { get; private set; }
        public double RaiseJump { get; private set; }
        public double CurrentBet { get; private set; }

        public int MaxPlayerAmount { get; private set; }

        public int CurrentPlayerAmount { get; private set; }

        public string Name { get; private set; }

        public bool IsGameInProcess { get; private set; }

        private System.Timers.Timer AfkTimer;

        private const int DECK_SIZE = 52;

        private const int PLAYER_AFK_DELAY = 60000;//in ms

        public GameLogic(string name, int maxPlayerAmount)
        {
            Name = name;
            MaxPlayerAmount = maxPlayerAmount;
            CurrentPlayerAmount = 0;
            IsGameInProcess = false;

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

        public List<Card> DealCardsToHand(PlayerData player)
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
                player.Hand[i] = Deck[cardNum];
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
            else if (state == GameState.Showdown)
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
            return PlayerController.Players.Values.ToList();
        }

        public List<PlayerData> GetActivePlayers()
        {
            List<PlayerData> suitablePlayers = new List<PlayerData>();
            foreach (var player in PlayerController.Players.Values)
            {
                if (!player.IsPlaying)
                    continue;
                suitablePlayers.Add(player);
            }
            return suitablePlayers;
        }

        private void HandleGameEnd()
        {
            IsGameInProcess = false;

            List<PlayerData> suitablePlayers = new List<PlayerData>();
            foreach (var player in PlayerController.Players.Values)
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

        public GameEndType HasGameEnded()
        {
            var endType = PlayerController.HasGameEnded();
            if (endType == Enums.GameEndType.None)
            {
                return endType;
            }
            else
            {
                PlayerController.HandleGameEnd();
                HandleGameEnd();
                return endType;
            }
        }

        public bool HandlePlayerAction(Guid id, PlayerAction action, double raiseAmount = 0)
        {
            if (raiseAmount != 0 && action == PlayerAction.Raise)
            {
                RaiseJump = raiseAmount - CurrentBet;
                CurrentBet = raiseAmount;
            }
            if (PlayerController.HandleAction(id, action, raiseAmount))
            {
                PotSize += PlayerController.GetMoneyToPot(PlayerController.PlayerToAct.Value);
                return true;
            }
            else
                return false;
        }

        public List<Card> GetPendingCards()
        {
            CurrentBet = 0;
            return DealCardsToBoard(PlayerController.GameState);
        }

        public void Start()
        {
            IsGameInProcess = true;
            RefreshGame();

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

        public int AddPlayer(Guid id, string username, double money, Receiver receiver)
        {
            for (int i = 0; i < 8; i++)
            {
                if (!PlayerController.Players.ContainsKey(i))
                {
                    PlayerController.Players.Add(i, new PlayerData(id, i, username, money, receiver));
                    CurrentPlayerAmount++;
                    return i;
                }
            }

            return -1;
        }

        public List<PlayerBase> GetPlayers() //??????????????????? does not work otherwise, cast from linq
        {
            List<PlayerBase> toret = new List<PlayerBase>();
            foreach (var player in PlayerController.Players.Values)
            {
                toret.Add(new PlayerBase() { Money = player.Money, Place = player.Place, Username = player.Username });
            }
            return toret;
        }

        public void CurrentPlayerAfkTimerStart()
        {
            if (AfkTimer == null)
            {
                AfkTimer = new System.Timers.Timer();
                AfkTimer.Enabled = true;
                AfkTimer.Start();
                AfkTimer.Interval = PLAYER_AFK_DELAY;
                AfkTimer.Elapsed += new ElapsedEventHandler(AfkTimer_Tick);
            }
            else
            {
                AfkTimer.Start();
            }
        }

        public void CurrentPlayerAfkTimerStop()
        {
            if (AfkTimer != null)
                AfkTimer.Stop();
        }

        public void PlayerDisconnected(int place)
        {
            PlayerController.Players.Remove(place);
        }

        private void AfkTimer_Tick(object sender, ElapsedEventArgs e)
        {
            AfkTimer.Stop();
            Console.WriteLine("fold by time");
            PlayerController.Players[PlayerController.PlayerToAct.Value].ClientReceiver.CurrentPlayerFoldByTime();
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

        public bool HasOrbitEnded()
        {
            return PlayerController.HasOrbitEnded();
        }
    }
}