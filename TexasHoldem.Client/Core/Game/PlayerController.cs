using System;
using System.Collections.Generic;
using System.Text;
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

        private int orbitEnd;

        public PlayerController()
        {
            Players = new Dictionary<int, Player>();
        }

        public bool IsCurrentPlayerTurn(int currPlayerPlace)
        {
            return PlayerToAct.Value == currPlayerPlace;
        }

        public bool HandleAction(PlayerAction action, double bet) //bug with all in
        {
            Player PlayerToHandle = Players[PlayerToAct.Value];

            if (PlayerToHandle.Money == 0)
            {
                PlayerToHandle.PreviousBet = 0;
                PlayerToHandle.CurrentBet = 0;
                return true;
            }

            if (bet > PlayerToHandle.Money)
                bet = PlayerToHandle.Money;

            PlayerToHandle.PreviousBet = PlayerToHandle.CurrentBet;
            PlayerToHandle.CurrentBet = bet;

            double moneyToSubstract = PlayerToHandle.CurrentBet - PlayerToHandle.PreviousBet;

            if (action == PlayerAction.Fold)
            {
                PlayerToHandle.IsPlaying = false;
                return HasOrbitEnded();
            }
            if (action == PlayerAction.FoldByDisconnect)
            {
                PlayerToHandle.IsPlaying = false;
                PlayerToHandle.IsDisconnected = true;
                return HasOrbitEnded();
            }

            if (action == PlayerAction.Call)
            {
                PlayerToHandle.Money -= moneyToSubstract;
            }
            else if (action == PlayerAction.Raise)
            {
                PlayerToHandle.Money -= moneyToSubstract;
                orbitEnd = PlayerToAct.GetPrevious().Value;
            }
            else if (action == PlayerAction.Check)
            {
                //Players[PlayerToAct.Value].PreviousBet = 0;
            }

            return HasOrbitEnded();
        }

        private Place GetNextPlayingPlace(Place from)
        {
            Place toret = from;
            while (true)
            {
                toret++;
                if (Players[toret.Value].IsPlaying == true)
                    return toret;
                if (toret == from)
                    break;
            }
            return toret;
        }

        public bool HasOrbitEnded()
        {
            if (PlayerToAct.Value == orbitEnd)
            {
                PlayerToAct = GetNextPlayingPlace(Button);
                orbitEnd = Button.Value;
                GameState++;

                foreach(var player in Players.Values)
                {
                    if (!player.IsPlaying)
                        continue;
                    player.PreviousBet = 0;
                    player.CurrentBet = 0;
                }

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

        public void HandleGameEnding()
        {
            Button = Button.GetNext();
            SBlind = Button.GetNext();
            BBlind = SBlind.GetNext();
            PlayerToAct = BBlind.GetNext();
        }

        public void Setup(int bBPlace, int buttonPlace, int playerToAct, double BBBet)
        {
            int maxVal = Players.Count;
            BBlind = new Place(bBPlace, maxVal);
            Button = new Place(buttonPlace, maxVal);
            SBlind = BBlind.GetPrevious();
            PlayerToAct = new Place(playerToAct, maxVal);
            GameState = GameState.PreFlop;
            orbitEnd = PlayerToAct.GetPrevious().Value;

            Players[BBlind.Value].Money -= BBBet;
            Players[BBlind.Value].CurrentBet = BBBet;

            Players[SBlind.Value].Money -= BBBet/2;
            Players[SBlind.Value].CurrentBet = BBBet/2;
        }

        public int GetPlayingPlayersAmount()
        {
            int amount = 0;
            foreach (var player in Players.Values)
            {
                if (player.IsPlaying)
                    amount++;
            }
            return amount;
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
    }
}
