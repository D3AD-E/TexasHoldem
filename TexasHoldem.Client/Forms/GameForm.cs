using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexasHoldem.Client.Core.Game;
using TexasHoldem.Client.Core.Network;
using TexasHoldem.Client.Forms;
using TexasHoldem.Client.Utils;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldemCommonAssembly.Network.Message;

namespace TexasHoldem.Client
{
    public partial class GameForm : Form
    {
        private readonly Core.Network.Client _client;

        private System.Timers.Timer AfkTimer;

        private List<Card> Board;

        private PictureBox[] BoardImg;

        private PlayerDisplay[] PlayerDisplays;

        private readonly Player CurrentPlayer;

        private double PotSize;

        private double BBBet;

        private double CurrentBet;

        private readonly PlayerController PlayerController;

        private int ActivePlayers;

        private const string PATH = @"../../Pics/cards.png";

        private const int TIMER_INTERVAL = 200;//in ms

        private const int PLAYER_AFK_DELAY = 15;//in sec

        public GameForm(string username, double money, JoinResponse res)
        {
            _client = Core.Network.Client.Instance;
            _client.MessageReceived += Client_MessageReceived;
            PlayerController = new PlayerController();
            Board = new List<Card>();

            CurrentPlayer = new Player
            {
                Username = username,
                Money = money,
                Place = res.YourPlace
            };
            PotSize = res.PotSize;

            foreach (var player in res.PlayersInRoom)
            {
                PlayerController.Players.Add(player.Place, new Player(player));
            }
            
            PlayerController.Players.Add(CurrentPlayer.Place, CurrentPlayer);
            InitializeComponent();
        }

        private void Client_MessageReceived(MessageReceivedEventArgs args)
        {
            var msg = args.Message;
            if (msg is GameEndServer)
            {
                GameEndHandler(msg as GameEndServer);
            }
            else if (msg is CardInfoServer)
            {
                CardInfoHandler(msg as CardInfoServer);
            }
            else if (msg is PlayerActionServer)
            {
                PlayerActionHandler(msg as PlayerActionServer);
            }
            else if (msg is NewPlayerJoinedServer)
            {
                NewPlayerJoinedHandler(msg as NewPlayerJoinedServer);
            }
            else if (msg is GameInfoServer)
            {
                GameInfoHandler(msg as GameInfoServer);
            }
        }

        private void GameEndHandler(GameEndServer msg)
        {
            AfkTimer.Stop();
            if (ActivePlayers < 1)
            {
                ExeptionHandler.HandleExeption(new Exception("Desync with server happenned"), true);
            }
            if (msg.Showdown)
            {
                PlayerController.Players[msg.Place].Hand[0] = msg.Cards[0];
                PlayerController.Players[msg.Place].Hand[1] = msg.Cards[1];

                PlayerDisplays[msg.Place].CardImg0.Image = TextureHelper.CardToImage(msg.Cards[0], PATH);
                PlayerDisplays[msg.Place].CardImg1.Image = TextureHelper.CardToImage(msg.Cards[1], PATH);
            }
            ActivePlayers--;
            if (ActivePlayers == 1)
            {
                GameEndingHandler();
            }
        }

        private void GameEndingHandler()
        {
            foreach (var player in PlayerController.Players.Values)
            {
                if (!player.IsPlaying || player.Holding != null)
                    continue;
                DetermineHolding(player);
            }
            DetermineWinner();
            PlayerController.HandleGameEnding();

            //for (int i = 0; i<PlayerDisplays.Length; i++)
            //    RefreshPlayerDisplay(i);

            RefreshGame().ConfigureAwait(false);
            RefreshPotDisplay();
            ActionButtonsBehaviour(false);
        }

        private async Task RefreshGame()
        {
            foreach (var player in PlayerController.Players.Values)
            {
                if (player.IsDisconnected)
                {
                    PlayerDisplays[player.Place].EmptySeatSetup();
                    PlayerController.Players.Remove(player.Place);
                    continue;
                }  
            }
            await Task.Delay(2800);
            Board.Clear();
            
            foreach(var display in PlayerDisplays)
            {
                if (display.CardImg0.Image != null)
                {
                    InvokeUI(() =>
                    {
                        display.CardImg0.Image.Dispose();
                        display.CardImg0.Image = null;
                    });
                    
                }
                if (display.CardImg1.Image != null)
                {
                    InvokeUI(() =>
                    {
                        display.CardImg1.Image.Dispose();
                        display.CardImg1.Image = null;
                    });
                    
                }
            }

            foreach (var pictureBox in BoardImg)
            {
                if (pictureBox.Image != null)
                {
                    InvokeUI(() =>
                    {
                        pictureBox.Image.Dispose();
                        pictureBox.Image = null;
                    });
                }
            }
        }

        private void DetermineWinner()
        {
            List<Player> suitablePlayers = new List<Player>();
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
                RefreshPlayerDisplay(Winner.Place, "Won " + PotSize);
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

            double prize = PotSize / winnersAmount;
            foreach (var player in sorted)
            {
                player.Money += prize;
                RefreshPlayerDisplay(player.Place, "Won " + prize);
                i++;
                if (i == winnersAmount)
                    break;
            }
            PotSize = 0;
        }

        private void GameInfoHandler(GameInfoServer msg)            //BAD
        {
            PlayerController.Setup(msg.BBPlace, msg.ButtonPlace, msg.PlayerToAct, msg.BBBet);
            BBBet = msg.BBBet;
            CurrentBet = msg.BBBet;

            PotSize = BBBet + BBBet / 2;

            int BBPlace = PlayerController.BBlind.Value;
            int SBPlace = PlayerController.SBlind.Value;

            InvokeUI(() =>
            {
                PlayerDisplays[BBPlace].ActionLabel.Text = "BB bet " + BBBet;
                PlayerDisplays[BBPlace].MoneyLabel.Text = PlayerController.Players[BBPlace].Money.ToString();

                PlayerDisplays[SBPlace].ActionLabel.Text = "SB bet " + BBBet / 2;
                PlayerDisplays[SBPlace].MoneyLabel.Text = PlayerController.Players[SBPlace].Money.ToString();

                RaiseAmountUD.Minimum = (decimal)CurrentBet * 2;
                RaiseAmountUD.Maximum = (decimal)CurrentPlayer.Money;

                PotSizeLabel.Text = PotSize.ToString();
            });

            InvokeUI(() => PlayerDisplays[PlayerController.PlayerToAct.Value].SetupPlayerAfkAwaiting());
            if (AfkTimer == null)
            {
                AfkTimer = new System.Timers.Timer();
                AfkTimer.Enabled = true;
                AfkTimer.Start();
                AfkTimer.Interval = TIMER_INTERVAL;
                AfkTimer.Elapsed += new System.Timers.ElapsedEventHandler(AfkTimer_Tick);
            }
            else
            {
                AfkTimer.Start();
            }
            ActionButtonsBehaviour(PlayerController.IsCurrentPlayerTurn(CurrentPlayer.Place));
        }

        private void NewPlayerJoinedHandler(NewPlayerJoinedServer msg)
        {
            PlayerController.Players.Add(msg.Player.Place, new Player(msg.Player));
            InvokeUI(() =>
            {
                PlayerDisplays[msg.Player.Place].UsernameLabel.Text = msg.Player.Username;
                PlayerDisplays[msg.Player.Place].MoneyLabel.Text = msg.Player.Money.ToString();
            });
        }

        private void PlayerActionHandler(PlayerActionServer msg)
        {
            HandlePlayerAction(msg.PlayerPos, msg.Action, msg.RaiseAmount);
            //string action = msg.Action.ToString();
            //if (msg.RaiseAmount != 0)
            //{
            //    action += " for " + msg.RaiseAmount;
            //}

            //CurrentBet += msg.RaiseAmount;

            //if (PlayerController.HandleAction(msg.Action, CurrentBet))
            //    GameStateChanged();

            //PotSize += PlayerController.GetMoneyToPot(msg.PlayerPos);

            //if(PlayerController.HasGameEnded())
            //{
            //    int winnerPlace = PlayerController.GetWinnerPlace();
            //    if (winnerPlace == -1)
            //        throw new Exception("No active players");
            //    RefreshPlayerDisplay(winnerPlace, "Won "+PotSize);
            //    PotSize = 0;
            //    PlayerController.HandleGameEnding();
            //    RefreshGame().ConfigureAwait(false);
            //    RefreshPotDisplay();
            //    ActionButtonsBehaviour(false);
            //    return;
            //}


            //if (PlayerController.GameState == GameState.Showdown)
            //    ActivePlayers = PlayerController.GetPlayingPlayersAmount();

            //RefreshPlayerDisplay(msg.PlayerPos, action);
            //RefreshPotDisplay();
            //ActionButtonsBehaviour(PlayerController.IsCurrentPlayerTurn(CurrentPlayer.Place));
        }

        private void CardInfoHandler(CardInfoServer msg)
        {
            if (msg.ToHand)
            {
                PlayerController.Players[msg.Place].Hand[0] = msg.Cards[0];
                PlayerController.Players[msg.Place].Hand[1] = msg.Cards[1];

                PlayerDisplays[msg.Place].CardImg0.Image = TextureHelper.CardToImage(msg.Cards[0], PATH);
                PlayerDisplays[msg.Place].CardImg1.Image = TextureHelper.CardToImage(msg.Cards[1], PATH);
            }
            else
            {
                Board.AddRange(msg.Cards);

                for (int i = 0; i < Board.Count; i++)
                {
                    if (BoardImg[i].Image != null)
                        continue;

                    BoardImg[i].Image = TextureHelper.CardToImage(Board[i], PATH);
                }
                DetermineHolding(PlayerController.Players[CurrentPlayer.Place]);
                InvokeUI(() =>
                {
                    HoldingLabel.Text = PlayerController.Players[CurrentPlayer.Place].Holding.ToString();
                });
            }
        }

        private void DetermineHolding(Player player)
        {
            var toDetermine = new Card[player.Hand.Length + Board.Count];
            player.Hand.CopyTo(toDetermine, 0);
            Board.CopyTo(toDetermine, player.Hand.Length);

            player.Holding = new Holding(toDetermine);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _client.Connect("localhost", 8888);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while connecting to server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
        }

        private void HandleCurrentPlayerAction(PlayerAction action, double raiseAmount = 0)
        {
            if(PlayerController.Players[CurrentPlayer.Place].Money == 0)
            {
                ActionButtonsBehaviour(false);
                return;
            }

            _client.SendPlayerAction(action, raiseAmount);

            HandlePlayerAction(CurrentPlayer.Place, action, raiseAmount);

            //string actionStr = action.ToString();
            //if (raiseAmount != 0)
            //{
            //    actionStr += " for " + raiseAmount;
            //}

            //CurrentBet += raiseAmount;
            //if (PlayerController.HandleAction(action, CurrentBet))
            //    GameStateChanged();

            //PotSize += PlayerController.GetMoneyToPot(CurrentPlayer.Place);

            //if (PlayerController.HasGameEnded())
            //{
            //    int winnerPlace = PlayerController.GetWinnerPlace();
            //    RefreshPlayerDisplay(winnerPlace, "Won " + PotSize);
            //    PotSize = 0;
            //    PlayerController.HandleGameEnding();
            //    RefreshPotDisplay();
            //    RefreshGame().ConfigureAwait(false);
            //    ActionButtonsBehaviour(false);
            //    return;
            //}

            //if (PlayerController.GameState == GameState.Showdown)
            //    ActivePlayers = PlayerController.GetPlayingPlayersAmount();

            //RefreshPlayerDisplay(CurrentPlayer.Place, actionStr);
            //RefreshPotDisplay();
            //ActionButtonsBehaviour(PlayerController.IsCurrentPlayerTurn(CurrentPlayer.Place));
        }

        private void RefreshPotDisplay()
        {
            InvokeUI(() =>
            {
                PotSizeLabel.Text = PotSize.ToString();
            });
        }

        private void RefreshPlayerDisplay(int displayNumber)
        {
            InvokeUI(() =>
            {
                PlayerDisplays[displayNumber].ActionLabel.Text = string.Empty;
                PlayerDisplays[displayNumber].MoneyLabel.Text = PlayerController.Players[displayNumber].Money.ToString();
            });
        }

        private void RefreshPlayerDisplay(int displayNumber, string action)
        {
            InvokeUI(() =>
            {
                PlayerDisplays[displayNumber].ActionLabel.Text = action;
                PlayerDisplays[displayNumber].MoneyLabel.Text = PlayerController.Players[displayNumber].Money.ToString();
            });
        }

        private void GameStateChanged()
        {
            CurrentBet = 0;
            InvokeUI(() =>
            {
                RaiseAmountUD.Minimum = (decimal)BBBet;
                RaiseAmountUD.Maximum = (decimal)CurrentPlayer.Money;

                PotSizeLabel.Text = PotSize.ToString();

                for (int i = 0; i < PlayerDisplays.Length; i++)
                {
                    PlayerDisplays[i].ActionLabel.Text = string.Empty;
                }
            });
        }

        private void ActionButtonsBehaviour(bool enabled)
        {
            InvokeUI(() =>
            {
                FoldFButton.Enabled = enabled;
                CallFButton.Enabled = enabled;
                RaiseFButton.Enabled = enabled;
            });
        }

        private void InvokeUI(Action action)
        {
            this.Invoke(action);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

            BoardImg = new PictureBox[5];
            BoardImg[0] = BoardCard1Img;
            BoardImg[1] = BoardCard2Img;
            BoardImg[2] = BoardCard3Img;
            BoardImg[3] = BoardCard4Img;
            BoardImg[4] = BoardCard5Img;

            PlayerDisplays = new PlayerDisplay[8];

            //int i = 0;
            //foreach (Control c in this.Controls)
            //{
            //    if (c is PlayerDisplay)
            //    {
            //        PlayerDisplays[i] = c as PlayerDisplay;
            //        i++;
            //    }
            //}

            PlayerDisplays[0] = playerDisplay1;
            PlayerDisplays[1] = playerDisplay2;
            PlayerDisplays[2] = playerDisplay3;
            PlayerDisplays[3] = playerDisplay4;
            PlayerDisplays[4] = playerDisplay5;
            PlayerDisplays[5] = playerDisplay6;
            PlayerDisplays[6] = playerDisplay7;
            PlayerDisplays[7] = playerDisplay8;

            int maxValue = 1000 / TIMER_INTERVAL * PLAYER_AFK_DELAY;

            foreach(var display in PlayerDisplays)
            {
                display.SetupAfkBar(maxValue);
            }

            UsernameLabel.Text = CurrentPlayer.Username;

            PotSizeLabel.Text = PotSize.ToString();
            foreach (var player in PlayerController.Players.Values)
            {
                PlayerDisplays[player.Place].UsernameLabel.Text = player.Username;
                PlayerDisplays[player.Place].MoneyLabel.Text = player.Money.ToString();
            }
            PlayerDisplays[CurrentPlayer.Place].UsernameLabel.Text = CurrentPlayer.Username;
            PlayerDisplays[CurrentPlayer.Place].MoneyLabel.Text = CurrentPlayer.Money.ToString();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _client.DisconnectFromRoom();
        }

        private void RaiseFButton_Click(object sender, EventArgs e)
        {
            HandleCurrentPlayerAction(PlayerAction.Raise, (double)RaiseAmountUD.Value);
        }

        private void CallFButton_Click(object sender, EventArgs e)
        {
            HandleCurrentPlayerAction(PlayerAction.Call);
        }

        private void FoldFButon_Click(object sender, EventArgs e)
        {
            HandleCurrentPlayerAction(PlayerAction.Fold);
        }

        private void HandlePlayerAction(int place, PlayerAction action, double raiseAmount = 0)
        {
            PlayerDisplays[PlayerController.PlayerToAct.Value].RefreshPlayerAfk();
            AfkTimer.Stop();

            string actionStr = action.ToString();
            if (raiseAmount != 0)
            {
                actionStr += " for " + raiseAmount;
            }

            CurrentBet += raiseAmount;

            if (PlayerController.HandleAction(action, CurrentBet))
                GameStateChanged();

            PotSize += PlayerController.GetMoneyToPot(place);

            if (PlayerController.HasGameEnded())
            {
                int winnerPlace = PlayerController.GetWinnerPlace();
                if (winnerPlace == -1)
                    throw new Exception("No active players");
                RefreshPlayerDisplay(winnerPlace, "Won " + PotSize);
                PotSize = 0;

                PlayerController.HandleGameEnding();
                RefreshGame().ConfigureAwait(false);
                RefreshPotDisplay();
                ActionButtonsBehaviour(false);
            }
            else
            {
                if (PlayerController.GameState == GameState.Showdown)
                    ActivePlayers = PlayerController.GetPlayingPlayersAmount();
                else
                {
                    PlayerDisplays[PlayerController.PlayerToAct.Value].SetupPlayerAfkAwaiting();
                    //if (AfkTimer == null)
                    //{
                    //    AfkTimer = new Timer();
                    //    AfkTimer.Enabled = true;
                    //    AfkTimer.Start();
                    //    AfkTimer.Interval = TIMER_INTERVAL;
                    //    AfkTimer.Tick += new EventHandler(AfkTimer_Tick);
                    //}
                    //else
                    //{
                    //    AfkTimer.Start();
                    //}
                }

                RefreshPlayerDisplay(place, actionStr);
                RefreshPotDisplay();
                ActionButtonsBehaviour(PlayerController.IsCurrentPlayerTurn(CurrentPlayer.Place));
            }
        }

        private void AfkTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            int playerToActPlace = PlayerController.PlayerToAct.Value;
            if (PlayerDisplays[playerToActPlace].IncreasePlayerAfk(1))
            {
                AfkTimer.Stop();
                PlayerDisplays[playerToActPlace].RefreshPlayerAfk();
                //if(playerToActPlace == CurrentPlayer.Place)
                //{
                //    if(CurrentBet == 0)
                //        HandleCurrentPlayerAction(PlayerAction.Check);
                //    else
                //        HandleCurrentPlayerAction(PlayerAction.Fold);
                //}
                //else
                {
                    if (CurrentBet == 0)
                        HandlePlayerAction(playerToActPlace, PlayerAction.Check);
                    else
                        HandlePlayerAction(playerToActPlace, PlayerAction.Fold);
                }
            }
        }
    }
}