﻿using System.Collections.Generic;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldem.Client.Core.Game
{
    public class PlayerController
    {
        //mb move to private

        public GameState GameState { get; private set; }

        public Place Button { get; private set; }

        public Place SBlind { get; private set; }

        public Place BBlind { get; private set; }

        public Place PlayerToAct { get; private set; }

        public Dictionary<int, Player> Players { get; set; }

        private int _orbitEnd;

        public PlayerController()
        {
            Players = new Dictionary<int, Player>();
        }

        public bool IsCurrentPlayerTurn(int currPlayerPlace)
        {
            return PlayerToAct.Value == currPlayerPlace;
        }

        public void HandleAction(PlayerAction action, double bet)
        {
            Player PlayerToHandle = GetCurrentPlayer();
            if (PlayerToHandle.Money == 0)
            {
                //PlayerToHandle.PreviousBet = 0;
                //PlayerToHandle.CurrentBet = 0;
                return;
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
            return;
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

        public void HandleOrbitChange()
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

        public Player GetCurrentPlayer()
        {
            return Players[PlayerToAct.Value];
        }

        public void HandleGameEnding()
        {
            Button = Button.GetNext();
            SBlind = Button.GetNext();
            BBlind = SBlind.GetNext();
            PlayerToAct = BBlind.GetNext();
        }

        public void Setup(int bBPlace, int buttonPlace, int playerToAct, double BBBet)
        {
            int maxVal = 8;
            BBlind = new Place(bBPlace, maxVal);
            Button = new Place(buttonPlace, maxVal);
            SBlind = GetPreviuosPlayingPlace(BBlind);
            PlayerToAct = new Place(playerToAct, maxVal);
            GameState = GameState.PreFlop;
            _orbitEnd = GetPreviuosPlayingPlace(PlayerToAct).Value;

            Players[BBlind.Value].Money -= BBBet;
            Players[BBlind.Value].CurrentBet = BBBet;

            Players[SBlind.Value].Money -= BBBet / 2;
            Players[SBlind.Value].CurrentBet = BBBet / 2;
        }

        public int GetPlayingPlayersAmount() //fix below
        {
            int amount = 0;
            foreach (var player in Players.Values)
            {
                if (player.IsPlaying)
                    amount++;
            }
            return amount;
        }

        public List<Player> GetPlayingPlayers()
        {
            var players = new List<Player>();
            foreach (var player in Players.Values)
            {
                if (player.IsPlaying)
                    players.Add(player);
            }
            return players;
        }

        public List<Player> GetPlayingPlayersForPots()
        {
            var players = new List<Player>();
            foreach (var player in Players.Values)
            {
                if (player.IsPlaying && player.CurrentBet != 0)
                    players.Add(player);
            }
            return players;
        }

        public bool HasGameEnded()
        {
            return GetPlayingPlayersAmount() <= 1;
        }

        public int GetWinnerPlace()
        {
            foreach (var player in Players.Values)
            {
                if (player.IsPlaying)
                    return player.Place;
            }
            return -1;
        }

        public bool IsAllIn()
        {
            int haveMoney = 0;
            foreach (var player in Players.Values)
            {
                if (player.Money > 0)
                {
                    haveMoney++;
                }
            }
            return haveMoney == 1;
        }
    }
}