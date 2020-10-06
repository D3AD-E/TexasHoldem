using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Transactions;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldem.Server.Core.Game.Entities;
using TexasHoldem.Server.Enums;

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

        private int orbitEnd;

        public PlayerActionController()
        {
            Players = new Dictionary<int, PlayerData>();
        }

        public void SetupGame(double BBBet)
        {
            foreach(var player in Players.Values)
            {
                player.IsPlaying = true;
            }

            GameState = GameState.PreFlop;

            if(Button == null)
            {
                SetupNewGame();
            }
            else
            {
                RefreshGame();
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
            orbitEnd = PlayerToAct.GetPrevious().Value;
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

            if (action == PlayerAction.Call || action == PlayerAction.Check)
            {
                PlayerToHandle.Money -= moneyToSubstract;
            }
            else if (action == PlayerAction.Raise)
            {
                PlayerToHandle.Money -= moneyToSubstract;
                orbitEnd = PlayerToAct.GetPrevious().Value;
            }
            else if (action == PlayerAction.FoldByDisconnect)
            {
                PlayerToHandle.IsPlaying = false;
                PlayerToHandle.IsDisconnected = true;
                return true;
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

                foreach (var player in Players.Values)
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

        public GameEndType HasGameEnded()
        {
            if (GameState == GameState.Showdown)
            {
                return GameEndType.Showdown;
            }
            else
            {
                int nonFolders = 0;
                foreach (var player in Players.Values)
                {
                    if (player.IsPlaying)
                    {
                        nonFolders++;
                    }
                    if (nonFolders >= 2)
                        return GameEndType.None;
                }
            }
            return GameEndType.WinByFold;
        }

        public void HandleGameEnd()
        {
            Button = Button.GetNext();
            SBlind = Button.GetNext();
            BBlind = SBlind.GetNext();
            PlayerToAct = BBlind.GetNext();

            orbitEnd = PlayerToAct.GetPrevious().Value;
        }

        public void RefreshGame()
        {
            //throw new NotImplementedException();
        }
    }
}
