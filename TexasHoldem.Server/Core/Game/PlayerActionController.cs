﻿using System;
using System.Collections.Generic;
using TexasHoldem.Server.Core.Game.Entities;
using TexasHoldem.Server.Enums;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldem.Server.Core.Game
{
    public class PlayerActionController
    {
        //mb move to private

        public GameState GameState { get; private set; }

        public Place Button { get; private set; }

        public Place SBlind { get; private set; }

        public Place BBlind { get; private set; }

        public Place PlayerToAct { get; private set; }

        public Dictionary<int, PlayerData> Players { get; set; }

        private int _orbitEnd;

        public PlayerActionController()
        {
            Players = new Dictionary<int, PlayerData>();
        }

        public void SkipGameState()
        {
            if (GameState != GameState.Showdown)
                GameState++;
        }

        public void SetupGame(double BBBet)
        {
            foreach (var player in Players.Values)
            {
                player.IsPlaying = true;
            }

            GameState = GameState.PreFlop;

            if (Button == null)
            {
                SetupNewGame();
            }

            Players[BBlind.Value].Money -= BBBet;
            Players[BBlind.Value].CurrentBet = BBBet;

            Players[SBlind.Value].Money -= BBBet / 2;
            Players[SBlind.Value].CurrentBet = BBBet / 2;
        }

        private void SetupNewGame()
        {
            if (Players.Count == 2)
            {
                Button = new Place(0, 2);
                SBlind = new Place(0, 2);
                BBlind = new Place(1, 2);
                PlayerToAct = new Place(0, 2);
            }
            else
            {
                Button = new Place(0, Players.Count);
                SBlind = Button.GetNext();
                BBlind = SBlind.GetNext();
                PlayerToAct = BBlind.GetNext();
            }
            _orbitEnd = PlayerToAct.GetPrevious().Value;
        }

        public bool HandleAction(Guid id, PlayerAction action, double bet)
        {
            PlayerData PlayerToHandle = Players[PlayerToAct.Value];
            if (PlayerToHandle.ID != id)
                return false;

            if (PlayerToHandle.Money == 0)
            {
                //PlayerToHandle.PreviousBet = 0;
                //PlayerToHandle.CurrentBet = 0;
                return true;
            }
            if (bet > PlayerToHandle.Money)
            {
                bet = PlayerToHandle.CurrentBet + PlayerToHandle.Money;
            }

            PlayerToHandle.PreviousBet = PlayerToHandle.CurrentBet;
            PlayerToHandle.CurrentBet = bet;

            double moneyToSubstract = PlayerToHandle.CurrentBet - PlayerToHandle.PreviousBet;

            if (action == PlayerAction.Fold)
            {
                PlayerToHandle.IsPlaying = false;
            }
            else if (action == PlayerAction.FoldByDisconnect)
            {
                PlayerToHandle.IsPlaying = false;
                PlayerToHandle.IsDisconnected = true;
            }
            else if (action == PlayerAction.Call)
            {
                PlayerToHandle.Money -= moneyToSubstract;
            }
            else if (action == PlayerAction.Raise)
            {
                PlayerToHandle.Money -= moneyToSubstract;
                _orbitEnd = GetPreviuosPlayingPlace(PlayerToAct).Value;
            }
            else if (action == PlayerAction.Check)
            {
                //Players[PlayerToAct.Value].PreviousBet = 0;
            }

            return true;
        }

        private Place GetNextPlayingPlace(Place from)
        {
            Place toret = from;
            do
            {
                toret++;
                if (!Players.ContainsKey(toret.Value))
                    continue;
                if (Players[toret.Value].IsPlaying == true)
                    return toret;
            }
            while (toret != from);
            return toret;
        }

        private Place GetPreviuosPlayingPlace(Place from)
        {
            Place toret = from;
            do
            {
                toret--;
                if (!Players.ContainsKey(toret.Value))
                    continue;
                if (Players[toret.Value].IsPlaying == true)
                    return toret;
            }
            while (toret != from);
            return toret;
        }

        public void HandleOrbitEnd()
        {
            PlayerToAct = GetNextPlayingPlace(Button);
            _orbitEnd = Button.Value;
            GameState++;

            foreach (var player in Players.Values)
            {
                if (!player.IsPlaying)
                    continue;
                player.PreviousBet = 0;
                player.CurrentBet = 0;
            }
        }

        public bool HasOrbitEnded()
        {
            if (PlayerToAct.Value == _orbitEnd)
            {
                return true;
            }
            else
            {
                PlayerToAct = GetNextPlayingPlace(PlayerToAct);
                return false;
            }
        }

        public double GetMoneyToPot(int playerPos)
        {
            return Players[playerPos].CurrentBet - Players[playerPos].PreviousBet;
        }

        public GameEndType HasGameEnded()
        {
            if (GameState == GameState.Showdown)
            {
                return GameEndType.Showdown;
            }
            else
            {
                int nonFolders = 0;
                int haveMoney = 0;
                foreach (var player in Players.Values)
                {
                    if (player.IsPlaying)
                    {
                        nonFolders++;
                    }
                    if (player.Money > 0)
                    {
                        haveMoney++;
                    }
                }
                if (haveMoney == 1)
                    return GameEndType.AllIn;
                if (nonFolders >= 2)
                    return GameEndType.None;
            }
            return GameEndType.WinByFold;
        }

        public void HandleGameEnd()
        {
            Button = Button.GetNext();
            SBlind = Button.GetNext();
            BBlind = SBlind.GetNext();
            PlayerToAct = BBlind.GetNext();

            _orbitEnd = PlayerToAct.GetPrevious().Value;
        }

        public List<Player> GetPlayingPlayers()
        {
            var players = new List<Player>();
            foreach (var player in Players.Values)
            {
                if (player.IsPlaying && player.CurrentBet != 0)
                    players.Add(player);
            }
            return players;
        }
    }
}