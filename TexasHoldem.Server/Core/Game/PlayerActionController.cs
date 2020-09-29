using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Transactions;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldemServer.Core.Game.Entities;

namespace TexasHoldemServer.Core.Game
{
    public class PlayerActionController
    {
        //mb move to private

        public GameState GameState { get; private set; }

        public Place Button { get; private set; }

        public Place SBlind { get; private set; }

        public Place BBlind { get; private set; }

        public Place PlayerToAct { get; private set; }

        public List<PlayerData> Players { get; set; }

        private int orbitEnd;

        public PlayerActionController()
        {
            Players = new List<PlayerData>();
        }

        public void SetupGame(double BBBet)
        {
            GameState = GameState.PreFlop;
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
            orbitEnd = PlayerToAct.GetPrevious().Value;

            foreach (var player in Players)
            {
                player.IsPlaying = true;
            }
            Players[BBlind.Value].Money -= BBBet;
            Players[BBlind.Value].CurrentBet = BBBet;

            Players[SBlind.Value].Money -= BBBet / 2;
            Players[SBlind.Value].CurrentBet = BBBet / 2;
        }

        public bool HandleAction(Guid id, PlayerAction action, double bet) //bug with all in
        {
            PlayerData PlayerToHandle = Players[PlayerToAct.Value];
            if (PlayerToHandle.ID != id)
                return false;

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
                return true;
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


            return true;
        }

        private Place GetNextPlayingPlace(Place from)
        {
            Place toret = from;
            while(true)
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

                foreach (var player in Players)
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

        public bool HasGameEnded()
        {
            if (GameState == GameState.Showdown)
            {
                return true;
            }
            else
            {
                int nonFolders = 0;
                foreach (var player in Players)
                {
                    if (player.IsPlaying)
                    {
                        nonFolders++;
                    }
                    if (nonFolders >= 2)
                        return false;
                }
            }   
            return true;
        }

        public void HandleGameEnd()
        {
            Button = Button.GetNext();
            SBlind = Button.GetNext();
            BBlind = SBlind.GetNext();
            PlayerToAct = BBlind.GetNext();
        }
    }
}
