using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexasHoldem.Client.Core.Game;
using TexasHoldem.Client.Core.Network;
using TexasHoldem.Client.Forms;
using TexasHoldem.Client.Utils;
using TexasHoldem.CommonAssembly.Game.Entities;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldemCommonAssembly.Network.Message;

namespace TexasHoldem.Client
{
    public partial class GameForm : Form
    {
        private readonly Core.Network.Client _client;

        private System.Timers.Timer _afkTimer;

        private List<Card> _board;

        private PictureBox[] _boardImg;

        private PlayerDisplay[] _playerDisplays;

        private readonly Player _currentPlayer;

        // private double PotSize;

        private Stack<Pot> _pots;

        private double _bigBlindBet;

        private double _currentBet;

        private double _raiseJump;

        private readonly PlayerController _playerController;

        private int _activePlayers;

        private bool _isGameInProgress;

        private const string PATH = @"../../Pics/cards.png";

        private const int TIMER_INTERVAL = 200;//in ms

        private const int PLAYER_AFK_DELAY = 60;//in sec

        public GameForm(string username, double money, JoinResponse res)
        {
            _client = Core.Network.Client.Instance;
            _client.MessageReceived += Client_MessageReceived;
            _playerController = new PlayerController();
            _board = new List<Card>();
            _pots = new Stack<Pot>();

            _currentPlayer = new Player
            {
                Username = username,
                Money = money,
                Place = res.YourPlace
            };
            if (res.Pots != null)
                _pots = res.Pots;

            foreach (var player in res.PlayersInRoom)
            {
                _playerController.Players.Add(player.Place, new Player(player));
            }

            _playerController.Players.Add(_currentPlayer.Place, _currentPlayer);
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
            _afkTimer.Stop();
            if (_activePlayers < 1)
            {
                ExeptionHandler.HandleExeption(new Exception("Desync with server happenned"), true);
            }
            if (msg.Showdown)
            {
                _playerController.Players[msg.Place].Hand[0] = msg.Cards[0];
                _playerController.Players[msg.Place].Hand[1] = msg.Cards[1];

                _playerDisplays[msg.Place].CardImg0.Image = TextureHelper.CardToImage(msg.Cards[0], PATH);
                _playerDisplays[msg.Place].CardImg1.Image = TextureHelper.CardToImage(msg.Cards[1], PATH);
            }
            _activePlayers--;
            if (_activePlayers == 1)
            {
                GameEndingHandler();
            }
        }

        private void GameEndingHandler()
        {
            foreach (var player in _playerController.Players.Values)
            {
                if (!player.IsPlaying || player.Holding != null)
                    continue;
                DetermineHolding(player);
            }
            DetermineWinner();

            HandleGameEnd();
        }

        private async Task RefreshGame()
        {
            foreach (var player in _playerController.Players.Values)
            {
                if (player.IsDisconnected)
                {
                    _playerDisplays[player.Place].EmptySeatSetup();
                    _playerController.Players.Remove(player.Place);
                    continue;
                }
                else
                {
                    _playerDisplays[player.Place].CardImg0.Image = TextureHelper.CardToImage(null, string.Empty);
                    _playerDisplays[player.Place].CardImg1.Image = TextureHelper.CardToImage(null, string.Empty);
                }
            }
            await Task.Delay(2800);
            _board.Clear();

            foreach (var pictureBox in _boardImg)
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
            while (_pots.Count > 0)
            {
                var pot = _pots.Pop();
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
                    RefreshPlayerDisplay(Winner.Place, "Won " + pot.Size);
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
                    RefreshPlayerDisplay(player.Place, "Won " + prize);
                    i++;
                    if (i == winnersAmount)
                        break;
                }
            }
        }

        private void GameInfoHandler(GameInfoServer msg)            //BAD
        {
            _isGameInProgress = true;
            _playerController.Setup(msg.BBPlace, msg.ButtonPlace, msg.PlayerToAct, msg.BBBet);
            _bigBlindBet = msg.BBBet;
            _currentBet = msg.BBBet;
            _raiseJump = _currentBet;

            var pot = new Pot
            {
                Size = _bigBlindBet + _bigBlindBet / 2,
                Players = _playerController.Players.Values.ToList()
            };
            _pots.Push(pot);

            int BBPlace = _playerController.BBlind.Value;
            int SBPlace = _playerController.SBlind.Value;

            InvokeUI(() =>
            {
                _playerDisplays[BBPlace].ActionLabel.Text = "BB bet " + _bigBlindBet;
                _playerDisplays[BBPlace].MoneyLabel.Text = _playerController.Players[BBPlace].Money.ToString();

                _playerDisplays[SBPlace].ActionLabel.Text = "SB bet " + _bigBlindBet / 2;
                _playerDisplays[SBPlace].MoneyLabel.Text = _playerController.Players[SBPlace].Money.ToString();

                RaiseAmountUD.Minimum = (decimal)_currentBet * 2;
                RaiseAmountUD.Maximum = (decimal)(_currentPlayer.Money + _currentPlayer.CurrentBet);

                _playerDisplays[_playerController.PlayerToAct.Value].SetupPlayerAfkAwaiting();

                potsView.RefreshPots();
            });

            if (_afkTimer == null)
            {
                _afkTimer = new System.Timers.Timer();
                _afkTimer.Enabled = true;
                _afkTimer.Start();
                _afkTimer.Interval = TIMER_INTERVAL;
                _afkTimer.Elapsed += new System.Timers.ElapsedEventHandler(AfkTimer_Tick);
            }
            else
            {
                _afkTimer.Start();
            }
            ActionButtonsBehaviour(_playerController.IsCurrentPlayerTurn(_currentPlayer.Place));
        }

        private void NewPlayerJoinedHandler(NewPlayerJoinedServer msg)
        {
            _playerController.Players.Add(msg.Player.Place, new Player(msg.Player, !_isGameInProgress));
            InvokeUI(() =>
            {
                _playerDisplays[msg.Player.Place].UsernameLabel.Text = msg.Player.Username;
                _playerDisplays[msg.Player.Place].MoneyLabel.Text = msg.Player.Money.ToString();

                _playerDisplays[msg.Player.Place].CardImg0.Image = TextureHelper.CardToImage(null, string.Empty);
                _playerDisplays[msg.Player.Place].CardImg1.Image = TextureHelper.CardToImage(null, string.Empty);
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
                _playerController.Players[msg.Place].Hand[0] = msg.Cards[0];
                _playerController.Players[msg.Place].Hand[1] = msg.Cards[1];

                _playerDisplays[msg.Place].CardImg0.Image = TextureHelper.CardToImage(msg.Cards[0], PATH);
                _playerDisplays[msg.Place].CardImg1.Image = TextureHelper.CardToImage(msg.Cards[1], PATH);
            }
            else
            {
                _board.AddRange(msg.Cards);

                for (int i = 0; i < _board.Count; i++)
                {
                    if (_boardImg[i].Image != null)
                        continue;

                    _boardImg[i].Image = TextureHelper.CardToImage(_board[i], PATH);
                }
                DetermineHolding(_playerController.Players[_currentPlayer.Place]);
                InvokeUI(() =>
                {
                    HoldingLabel.Text = _playerController.Players[_currentPlayer.Place].Holding.ToString();
                });
            }
        }

        private void DetermineHolding(Player player)
        {
            var toDetermine = new Card[player.Hand.Length + _board.Count];
            player.Hand.CopyTo(toDetermine, 0);
            _board.CopyTo(toDetermine, player.Hand.Length);

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
            ActionButtonsBehaviour(false);

            _client.SendPlayerAction(action, raiseAmount);

            HandlePlayerAction(_currentPlayer.Place, action, raiseAmount);
        }

        //private void RefreshPotDisplay()
        //{
        //    InvokeUI(() =>
        //    {
        //        PotSizeLabel.Text = PotSize.ToString();
        //    });
        //}

        private void RefreshPlayerDisplay(int displayNumber)
        {
            InvokeUI(() =>
            {
                _playerDisplays[displayNumber].ActionLabel.Text = string.Empty;
                _playerDisplays[displayNumber].MoneyLabel.Text = _playerController.Players[displayNumber].Money.ToString();
            });
        }

        private void RefreshPlayerDisplay(int displayNumber, string action)
        {
            InvokeUI(() =>
            {
                _playerDisplays[displayNumber].ActionLabel.Text = action;
                _playerDisplays[displayNumber].MoneyLabel.Text = _playerController.Players[displayNumber].Money.ToString();
            });
        }

        private void GameStateChanged()
        {
            _currentBet = 0;
            InvokeUI(() =>
            {
                RaiseAmountUD.Minimum = (decimal)_bigBlindBet;
                RaiseAmountUD.Maximum = (decimal)_currentPlayer.Money;

                potsView.RefreshPots();

                for (int i = 0; i < _playerDisplays.Length; i++)
                {
                    _playerDisplays[i].ActionLabel.Text = string.Empty;
                }
            });
        }

        private void ActionButtonsBehaviour(bool enabled)
        {
            if (enabled)
            {
                InvokeUI(() =>
                {
                    FoldFButton.Show();
                    CallFButton.Show();
                    RaiseFButton.Show();
                    RaiseAmountUD.Show();
                });
            }
            else
            {
                InvokeUI(() =>
                {
                    FoldFButton.Hide();
                    CallFButton.Hide();
                    RaiseFButton.Hide();
                    RaiseAmountUD.Hide();
                });
            }
        }

        private void InvokeUI(Action action)
        {
            this.Invoke(action);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            _boardImg = new PictureBox[5];
            _boardImg[0] = BoardCard1Img;
            _boardImg[1] = BoardCard2Img;
            _boardImg[2] = BoardCard3Img;
            _boardImg[3] = BoardCard4Img;
            _boardImg[4] = BoardCard5Img;

            _playerDisplays = new PlayerDisplay[8];

            //int i = 0;
            //foreach (Control c in this.Controls)
            //{
            //    if (c is PlayerDisplay)
            //    {
            //        PlayerDisplays[i] = c as PlayerDisplay;
            //        i++;
            //    }
            //}

            _playerDisplays[0] = playerDisplay1;
            _playerDisplays[1] = playerDisplay2;
            _playerDisplays[2] = playerDisplay3;
            _playerDisplays[3] = playerDisplay4;
            _playerDisplays[4] = playerDisplay5;
            _playerDisplays[5] = playerDisplay6;
            _playerDisplays[6] = playerDisplay7;
            _playerDisplays[7] = playerDisplay8;

            potsView.Pots = _pots;
            potsView.RefreshPots();

            int maxValue = 1000 / TIMER_INTERVAL * PLAYER_AFK_DELAY;

            foreach (var display in _playerDisplays)
            {
                display.SetupAfkBar(maxValue);
            }

            UsernameLabel.Text = _currentPlayer.Username;

            //PotSizeLabel.Text = PotSize.ToString();
            foreach (var player in _playerController.Players.Values)
            {
                _playerDisplays[player.Place].UsernameLabel.Text = player.Username;
                _playerDisplays[player.Place].MoneyLabel.Text = player.Money.ToString();
                _playerDisplays[player.Place].CardImg0.Image = TextureHelper.CardToImage(null, string.Empty);
                _playerDisplays[player.Place].CardImg1.Image = TextureHelper.CardToImage(null, string.Empty);
            }
            _playerDisplays[_currentPlayer.Place].UsernameLabel.Text = _currentPlayer.Username;
            _playerDisplays[_currentPlayer.Place].MoneyLabel.Text = _currentPlayer.Money.ToString();
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
            _afkTimer.Stop();
            _playerDisplays[_playerController.PlayerToAct.Value].RefreshPlayerAfk();

            string actionStr = action.ToString();
            if (raiseAmount != 0 && action == PlayerAction.Raise)
            {
                actionStr += " to " + raiseAmount;
                _raiseJump = raiseAmount - _currentBet;
                _currentBet = raiseAmount;
            }

            _playerController.HandleAction(action, _currentBet);

            _pots.Peek().Size += _playerController.GetMoneyToPot(place);

            if (_playerController.HasOrbitEnded())
            {
                HandlePots();
                _playerController.HandleOrbitChange();
                GameStateChanged();

                if (_playerController.IsAllIn())
                {
                    _activePlayers = _playerController.GetPlayingPlayersAmount();
                    foreach (var player in _playerController.Players.Values)
                        RefreshPlayerDisplay(player.Place, actionStr);
                    potsView.RefreshPots();
                    return;
                }
            }

            if (_playerController.HasGameEnded())
            {
                int winnerPlace = _playerController.GetWinnerPlace();
                if (winnerPlace == -1)
                    throw new Exception("No active players");
                double sum = 0;
                while (_pots.Count > 0)
                {
                    var pot = _pots.Pop();
                    sum += pot.Size;
                }

                RefreshPlayerDisplay(winnerPlace, "Won " + sum);
                HandleGameEnd();
            }
            else
            {
                if (_playerController.GameState == GameState.Showdown)
                    _activePlayers = _playerController.GetPlayingPlayersAmount();
                else
                {
                    _playerDisplays[_playerController.PlayerToAct.Value].SetupPlayerAfkAwaiting();
                    _afkTimer.Start();
                }

                foreach (var player in _playerController.Players.Values)
                    RefreshPlayerDisplay(player.Place, actionStr);
                potsView.RefreshPots();

                if (_playerController.IsCurrentPlayerTurn(_currentPlayer.Place))
                {
                    ActionButtonsBehaviour(true);
                    HandleRaizeSizeGui(_currentPlayer.Money < _currentBet);
                }
            }
        }

        private void HandleGameEnd()
        {
            _isGameInProgress = false;
            if (_currentPlayer.Money == 0)
            {
                var messageBox = new FlatMessageBox("Insufficient funds", "You cannot play the game without money, try adding it via add money option");
                messageBox.ShowDialog();
                this.Close();
            }
            _playerController.HandleGameEnding();
            RefreshGame().ConfigureAwait(false);
            potsView.RefreshPots();
            ActionButtonsBehaviour(false);
        }

        private void HandlePots()
        {
            var currPot = _pots.Peek();

            var players = _playerController.GetPlayingPlayersForPots().OrderBy(i => i.CurrentBet).ToList();
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

                        _pots.Push(pot);
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
                if (pot.Equals(currPot))
                    return;

                pot.Size = players[sameBetIndexStart].CurrentBet * playersInPot;
                currPot.Size -= pot.Size;

                _pots.Push(pot);
            }
        }

        private void HandleRaizeSizeGui(bool overBet)
        {
            if (overBet)
            {
                InvokeUI(() =>
                {
                    RaiseAmountUD.Hide();
                    RaiseFButton.Hide();
                });
            }
            else
            {
                if (_currentBet == 0)
                    InvokeUI(() => RaiseAmountUD.Minimum = (decimal)_bigBlindBet);
                else
                {
                    double raise = _currentBet + _raiseJump;
                    if (raise > _currentPlayer.Money)
                        InvokeUI(() => RaiseAmountUD.Minimum = (decimal)(_currentPlayer.Money));
                    else
                        InvokeUI(() =>
                        {
                            RaiseAmountUD.Minimum = (decimal)(raise);
                            RaiseAmountUD.Maximum = (decimal)(_currentPlayer.Money + _currentPlayer.CurrentBet);
                        });
                }
            }
        }

        private void AfkTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            int playerToActPlace = _playerController.PlayerToAct.Value;
            if (_playerDisplays[playerToActPlace].IncreasePlayerAfk(1))
            {
                _afkTimer.Stop();
                _playerDisplays[playerToActPlace].RefreshPlayerAfk();
                //if(playerToActPlace == CurrentPlayer.Place)
                //{
                //    if(CurrentBet == 0)
                //        HandleCurrentPlayerAction(PlayerAction.Check);
                //    else
                //        HandleCurrentPlayerAction(PlayerAction.Fold);
                //}
                //else
                {
                    if (_currentBet == 0)
                        HandlePlayerAction(playerToActPlace, PlayerAction.Check);
                    else
                        HandlePlayerAction(playerToActPlace, PlayerAction.Fold);
                }
            }
        }
    }
}