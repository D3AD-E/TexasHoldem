using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using TexasHoldem.CommonAssembly.Game.Entities;
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

        private PlayerActionController _playerController { get; set; }

        //public double PotSize { get; private set; }

        public Stack<Pot> Pots { get; private set; }

        public double BBlindBet { get; private set; }
        public double RaiseJump { get; private set; }
        public double CurrentBet { get; private set; }

        public int MaxPlayerAmount { get; private set; }

        public int CurrentPlayerAmount { get; private set; }

        public string Name { get; private set; }

        public bool IsGameInProcess { get; private set; }

        private System.Timers.Timer _afkTimer;

        private const int DECK_SIZE = 52;

        private const int PLAYER_AFK_DELAY = 60000;//in ms

        public GameLogic(string name, int maxPlayerAmount)
        {
            Name = name;
            MaxPlayerAmount = maxPlayerAmount;
            CurrentPlayerAmount = 0;
            IsGameInProcess = false;

            SetupDeck();
            _playerController = new PlayerActionController();
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
            return _playerController.Players.Values.ToList();
        }

        public List<PlayerData> GetActivePlayers()
        {
            List<PlayerData> suitablePlayers = new List<PlayerData>();
            foreach (var player in _playerController.Players.Values)
            {
                if (!player.IsPlaying)
                    continue;
                suitablePlayers.Add(player);
            }
            return suitablePlayers;
        }

        public void HandleGameEnd(GameEndType gameEnd)
        {
            _playerController.HandleGameEnd();
            IsGameInProcess = false;

            if (gameEnd == GameEndType.WinByFold)
            {
                var player = GetActivePlayers().FirstOrDefault();

                double sum = 0;
                while (Pots.Count > 0)
                {
                    var pot = Pots.Pop();
                    sum += pot.Size;
                }
                player.Money += sum;
                return;
            }

            while (Pots.Count > 0)
            {
                var pot = Pots.Pop();
                var suitablePlayers = new List<Player>();
                foreach (var player in pot.Players)
                {
                    if (!player.IsPlaying)
                        continue;
                    if (player.Holding == null)
                        DetermineHolding(player);
                    suitablePlayers.Add(player);
                }

                var sorted = suitablePlayers.OrderByDescending(i => i.Holding);

                if (sorted.Count() == 1)
                {
                    var Winner = sorted.First();
                    Winner.Money += pot.Size;
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

                double prize = pot.Size / winnersAmount;
                foreach (var player in sorted)
                {
                    player.Money += prize;
                    i++;
                    if (i == winnersAmount)
                        break;
                }
            }
            //List<PlayerData> suitablePlayers = new List<PlayerData>();
            //foreach (var player in PlayerController.Players.Values)
            //{
            //    if (!player.IsPlaying)
            //        continue;
            //    DetermineHolding(player);
            //    suitablePlayers.Add(player);
            //}
            //var sorted = suitablePlayers.OrderByDescending(i => i.Holding);

            ////if (sorted.Count() == 1)
            ////{
            ////    var Winner = sorted.First();
            ////    Winner.Money += PotSize;
            ////    PotSize = 0;
            ////    return;
            ////}

            //var bestPlayer = sorted.First();

            //int winnersAmount = 0;

            //foreach (var player in sorted)
            //{
            //    if (player.Holding == bestPlayer.Holding)
            //        winnersAmount++;
            //    else
            //        break;
            //}

            //int i = 0;
            //foreach (var player in sorted)
            //{
            //    player.Money += PotSize / winnersAmount;
            //    i++;
            //    if (i == winnersAmount)
            //        break;
            //}
            //PotSize = 0;
        }

        public GameEndType HasGameEnded()
        {
            var endType = _playerController.HasGameEnded();
            if (endType == Enums.GameEndType.None)
            {
                return endType;
            }
            else
            {
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
            if (_playerController.HandleAction(id, action, CurrentBet))
            {
                Pots.Peek().Size += _playerController.GetMoneyToPot(_playerController.PlayerToAct.Value);
                return true;
            }
            else
                return false;
        }

        public List<Card> GetPendingCards()
        {
            return DealCardsToBoard(_playerController.GameState);
        }

        public void Start()
        {
            IsGameInProcess = true;
            RefreshCards();

            if (Pots == null)
                Pots = new Stack<Pot>();
            else
                Pots.Clear();

            _playerController.SetupGame(BBlindBet);

            CurrentBet = BBlindBet;
            var pot = new Pot
            {
                Size = BBlindBet + BBlindBet / 2,
                Players = _playerController.Players.Values.OfType<Player>().ToList()
            };
            Pots.Push(pot);
        }

        private void RefreshCards()
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
                if (!_playerController.Players.ContainsKey(i))
                {
                    _playerController.Players.Add(i, new PlayerData(id, i, username, money, receiver));
                    CurrentPlayerAmount++;
                    return i;
                }
            }

            return -1;
        }

        public void HandleOrbitEnd()
        {
            var currPot = Pots.Peek();

            var players = _playerController.GetPlayingPlayers().OrderBy(i => i.CurrentBet).ToList();
            if (players.Count() == 0)
                return;
            var player = players[0];

            int playersAmount = players.Count();
            int sameBetIndexStart = 0;
            for (int i = 1; i < playersAmount; i++)
            {
                if (player.CurrentBet != players[i].CurrentBet && player.CurrentBet != 0)
                {
                    var pot = new Pot();
                    double prevBet = player.CurrentBet;
                    foreach (var temp in players)
                    {
                        if (temp.CurrentBet != 0)
                        {
                            pot.Players.Add(temp);
                            temp.CurrentBet -= prevBet;
                        }
                    }

                    pot.Size = prevBet * pot.Players.Count();

                    if (pot.Equals(currPot))
                    {
                        currPot.Size -= currPot.Size - pot.Size;
                    }
                    else
                    {
                        currPot.Size -= pot.Size;

                        Pots.Push(pot);
                    }

                    sameBetIndexStart = i;
                    player = players[i];
                }
            }

            if (sameBetIndexStart == playersAmount - 1)
            {
                players[sameBetIndexStart].Money += players[sameBetIndexStart].CurrentBet;
            }
            else
            {
                var pot = new Pot();
                int playersInPot = playersAmount - sameBetIndexStart;
                pot.Players.AddRange(players.GetRange(sameBetIndexStart, playersInPot));
                pot.Size = players[sameBetIndexStart].CurrentBet * playersInPot;

                currPot.Size -= pot.Size;

                Pots.Push(pot);
            }

            _playerController.HandleOrbitEnd();
            CurrentBet = 0;
        }

        public List<PlayerBase> GetPlayers() //??????????????????? does not work otherwise, cast from linq
        {
            List<PlayerBase> toret = new List<PlayerBase>();
            foreach (var player in _playerController.Players.Values)
            {
                toret.Add(new PlayerBase() { Money = player.Money, Place = player.Place, Username = player.Username });
            }
            return toret;
        }

        public void CurrentPlayerAfkTimerStart()
        {
            if (_afkTimer == null)
            {
                _afkTimer = new System.Timers.Timer();
                _afkTimer.Enabled = true;
                _afkTimer.Start();
                _afkTimer.Interval = PLAYER_AFK_DELAY;
                _afkTimer.Elapsed += new ElapsedEventHandler(AfkTimer_Tick);
            }
            else
            {
                _afkTimer.Start();
            }
        }

        public void CurrentPlayerAfkTimerStop()
        {
            if (_afkTimer != null)
                _afkTimer.Stop();
        }

        public void PlayerDisconnected(int place)
        {
            _playerController.Players.Remove(place);
        }

        private void AfkTimer_Tick(object sender, ElapsedEventArgs e)
        {
            _afkTimer.Stop();
            Console.WriteLine("fold by time");
            _playerController.Players[_playerController.PlayerToAct.Value].ClientReceiver.CurrentPlayerFoldByTime();
        }

        public List<Card> ForceGameEnd()
        {
            var cards = new List<Card>();
            while (_playerController.GameState != GameState.Showdown)
            {
                cards.AddRange(GetPendingCards());
                _playerController.SkipGameState();
            }
            return cards;
        }

        public int GetCurrentPlayerPlace()
        {
            return _playerController.PlayerToAct.Value;
        }

        public int GetBBPlace()
        {
            return _playerController.BBlind.Value;
        }

        public int GetButtonPlace()
        {
            return _playerController.Button.Value;
        }

        public bool HasOrbitEnded()
        {
            return _playerController.HasOrbitEnded();
        }
    }
}