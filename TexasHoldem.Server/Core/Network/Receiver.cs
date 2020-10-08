using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using TexasHoldem.Server.Enums;
using TexasHoldem.Server.Utils;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldemCommonAssembly.Network.Message;
using TexasHoldemCommonAssembly.Network.Message.Base;

namespace TexasHoldem.Server.Core.Network
{
    public class Receiver
    {
        private Thread receivingThread;
        private Thread sendingThread;

        public Server Server { get; private set; }

        public string Username { get; set; }

        public Status Status { get; set; }
        public Guid ID { get; private set; }
        //public Guid ConnectedRoomID { get; private set; }

        public GameLogic Game { get; private set; }

        public Queue<MessageBase> MessageQueue { get; private set; }

        public long TotalBytesUsage { get; set; }

        public TcpClient Client { get; set; }

        public int Place { get; private set; }

        private string CurrentPhrase;

        private bool UserAuthenticated;

        public Receiver()
        {
            UserAuthenticated = false;
            ID = Guid.NewGuid();
            MessageQueue = new Queue<MessageBase>();
            Status = Status.Connected;
        }

        public Receiver(TcpClient client, Server server)
            : this()
        {
            Server = server;
            Client = client;
            Client.ReceiveBufferSize = 1024;
            Client.SendBufferSize = 1024;
        }

        public void Start()
        {
            receivingThread = new Thread(ReceivingMethod)
            {
                IsBackground = true
            };
            receivingThread.Start();

            sendingThread = new Thread(SendingMethod)
            {
                IsBackground = true
            };
            sendingThread.Start();
        }

        public void SendMessage(MessageBase message)
        {
            MessageQueue.Enqueue(message);
        }

        private void SendingMethod()
        {
            while (Status != Status.Disconnected)
            {
                if (MessageQueue.Count > 0)
                {
                    var message = MessageQueue.Dequeue();
                    try
                    {
                        BinaryFormatter f = new BinaryFormatter();
                        f.Serialize(Client.GetStream(), message);
                    }
                    catch
                    {
                        Disconnect();
                    }
                }
                Thread.Sleep(30);
            }
        }

        private void ReceivingMethod()
        {
            while (Status != Status.Disconnected)
            {
                if (Client.Available > 0)
                {
                    TotalBytesUsage += Client.Available;

                    //try
                    {
                        BinaryFormatter f = new BinaryFormatter();
                        MessageBase msg = f.Deserialize(Client.GetStream()) as MessageBase;
                        OnMessageReceived(msg);
                    }
                    //catch (Exception e)
                    //{
                    //    Exception ex = new Exception("Unknown message received. Could not deserialize the stream.", e);
                    //    Console.WriteLine(e.Message);
                    //    Console.WriteLine(ex.Message);
                    //}
                }

                Thread.Sleep(30);
            }
        }

        private void DisconnectFromRoom()
        {
            if (Game.IsGameInProcess)
            {
                PlayerActionHandlerAsync(PlayerAction.Fold, 0).ConfigureAwait(false);
            }
            else
            {
                Game.PlayerDisconnected(Place);
            }
            //await money change
            Game = null;
        }

        private void Disconnect()
        {
            if (Status == Status.Disconnected)
                return;

            MessageQueue.Clear();

            Status = Status.Disconnected;
            Client.Client.Disconnect(false);
            Client.Close();
            Server.Receivers.Remove(this);
        }

        private void OnMessageReceived(MessageBase msg)
        {
            Type type = msg.GetType();
            if (type == typeof(DisconnectRequest))
            {
                Disconnect();
            }
            else if (UserAuthenticated)
            {
                if (type == typeof(PlayerActionClient))
                {
                    PlayerActionHandlerAsync(msg as PlayerActionClient).ConfigureAwait(false);
                }
                else if (type == typeof(ShowRoomsRequest))
                {
                    ShowRoomsHandler(msg as ShowRoomsRequest);
                }
                else if (type == typeof(JoinRoomRequest))
                {
                    JoinRoomHandler(msg as JoinRoomRequest);
                }
                else if (type == typeof(AddMoneyRequest))
                {
                    AddMoneyRequestHandlerAsync(msg as AddMoneyRequest).ConfigureAwait(false);
                }
                else if (type == typeof(AddMoneyPhraseRequest))
                {
                    AddMoneyRequestPhraseHandle(msg as AddMoneyPhraseRequest);
                }
                else if (type == typeof(CreateRoomRequest))
                {
                    CreateRoomHandler(msg as CreateRoomRequest);
                }
                else if (type == typeof(DisconnectFromRoomRequest))
                {
                    DisconnectFromRoom();
                }
            }
            else
            {
                if (type == typeof(SignUpRequest))
                {
                    RegisterHandler(msg as SignUpRequest).ConfigureAwait(false);
                }
                else if (type == typeof(AuthenticateRequest))
                {
                    AuthenticateHandler(msg as AuthenticateRequest).ConfigureAwait(false);
                }
            }
        }

        private void AddMoneyRequestPhraseHandle(AddMoneyPhraseRequest req)
        {
            var res = new AddMoneyPhraseResponse(req);
            var phaseGetter = new Phrases();
            string phrase = phaseGetter.GetRandomString();

            CurrentPhrase = phrase;
            res.Phrase = phrase;
            SendMessage(res);
        }

        private async Task AddMoneyRequestHandlerAsync(AddMoneyRequest req)
        {
            var res = new AddMoneyResponse(req);
            if (string.Compare(CurrentPhrase, req.Phrase) == 0)
            {
                try
                {
                    var user = await Server.Service.GetUser(Username);
                    user.Money += 1000;
                    await Server.Service.UpdateUser(user);
                }
                catch (Exception ex)
                {
                    res.HasError = true;
                    res.Exception = ex;
                }
            }
            else
            {
                res.HasError = true;
                res.Exception = new Exception("Phrases do not match");
            }
            SendMessage(res);
        }

        private void CreateRoomHandler(CreateRoomRequest req)
        {
            try
            {
                Game = Server.CreateRoom(req.Name, req.MaxPlayers);
                JoinHandlerAsync(req).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var res = new JoinResponse(req);
                res.HasError = true;
                res.Exception = ex;
                SendMessage(res);
            }
        }

        private void JoinRoomHandler(JoinRoomRequest req)
        {
            try
            {
                if (Server.Games.ContainsKey(req.RoomId))
                {
                    Game = Server.Games[req.RoomId];
                    JoinHandlerAsync(req).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                var res = new JoinResponse(req);
                res.HasError = true;
                res.Exception = ex;
                SendMessage(res);
                return;
            }
        }

        private void ShowRoomsHandler(ShowRoomsRequest req)
        {
            ShowRoomsResponse res = new ShowRoomsResponse(req)
            {
                Rooms = Server.GetRooms()
            };
            SendMessage(res);
        }

        private async Task AuthenticateHandler(AuthenticateRequest req)
        {
            var user = await Server.Service.GetUser(req.Username);
            bool error;
            if (user == null)
            {
                error = true;
            }
            else
            {
                error = !BCrypt.Net.BCrypt.Verify(req.Password, user.Password);
            }

            if (error)
            {
                AuthenticateResponse res = new AuthenticateResponse(req)
                {
                    HasError = true,
                    Exception = new Exception("Incorrect username or password")
                };
                SendMessage(res);
            }
            else
            {
                AuthenticateResponse res = new AuthenticateResponse(req)
                {
                    Money = user.Money
                };
                UserAuthenticated = true;
                Username = req.Username;
                SendMessage(res);
            }
        }

        private async Task RegisterHandler(SignUpRequest req)
        {
            string password = req.Password;
            if (req.Username.Length > 20 || req.Username.Length < 3 || password.Length < 8 || !password.Any(char.IsDigit) || !password.Any(char.IsDigit) || password.Length > 50)
            {
                var res = new SignUpResponse(req)
                {
                    HasError = true,
                    Exception = new Exception("Invalid message received")
                };
                SendMessage(res);
                return;
            }

            var user = await Server.Service.GetUser(req.Username);

            if (user != null)
            {
                var res = new SignUpResponse(req)
                {
                    HasError = true,
                    Exception = new Exception("Such username is already registered")
                };
                SendMessage(res);
            }
            else
            {
                try
                {
                    await Server.Service.CreateNewUser(req.Username, password);
                    var res = new SignUpResponse(req);
                    SendMessage(res);
                }
                catch (Exception e)
                {
                    var res = new SignUpResponse(req)
                    {
                        HasError = true,
                        Exception = e
                    };
                    SendMessage(res);
                }
            }
        }

        private async Task JoinHandlerAsync(RoomRequestsMessageBase req)
        {
            Console.WriteLine("Someone tried to join");
            var user = await Server.Service.GetUser(Username);

            var players = Game.GetPlayers();
            Place = Game.AddPlayer(ID, user.Username, user.Money, this);

            if (Place == -1)
            {
                JoinResponse resErr = new JoinResponse(req)
                {
                    HasError = true,
                    Exception = new Exception("No available places")
                };
                SendMessage(resErr);
                return;
            }

            JoinResponse res = new JoinResponse(req)
            {
                PlayersInRoom = players,
                YourPlace = Place,
                PotSize = Game.PotSize,
            };

            SendMessage(res);

            if (!res.HasError)
            {
                var newPlayerJoinedMsg = new NewPlayerJoinedServer
                {
                    Player = new PlayerBase
                    {
                        Username = user.Username,
                        Place = res.YourPlace,
                        Money = user.Money
                    }
                };
                Server.SendMessageToAllExcept(newPlayerJoinedMsg, this, Game);

                if (!Game.IsGameInProcess && Game.GetAllPlayers().Count > 1)
                {
                    await StartNewGameWithDelay(500);
                }
            }
        }

        private void StartGame()
        {
            Game.Start();
            var players = Game.GetAllPlayers();
            foreach (var player in players)
            {
                var msg = new CardInfoServer
                {
                    Cards = Game.DealCardsToHand(player),
                    ToHand = true,
                    Place = player.Place
                };
                player.ClientReceiver.SendMessage(msg);
            }
            Console.WriteLine("Started a game");

            var gameInfo = new GameInfoServer
            {
                BBPlace = Game.GetBBPlace(),
                ButtonPlace = Game.GetButtonPlace(),
                PlayerToAct = Game.GetCurrentPlayerPlace(),
                BBBet = Game.BBlindBet
            };
            Server.SendMessageToAll(gameInfo, Game);
        }

        private async Task PlayerActionHandlerAsync(PlayerActionClient msg)        //REDO
        {
            await PlayerActionHandlerAsync(msg.Action, msg.RaiseAmount);
        }

        public void CurrentPlayerFoldByTime()
        {
            PlayerActionHandlerAsync(PlayerAction.Fold, 0).ConfigureAwait(false);
        }

        private async Task PlayerActionHandlerAsync(PlayerAction action, double raiseAmount)
        {
            if (Game.HandlePlayerAction(ID, action, raiseAmount))
            {
                Game.CurrentPlayerAfkTimerStop();

                PlayerActionServer playerActionMsg = new PlayerActionServer
                {
                    Action = action,
                    RaiseAmount = raiseAmount,
                    PlayerPos = Game.GetCurrentPlayerPlace()
                };

                Server.SendMessageToAllExcept(playerActionMsg, this, Game);
                Console.WriteLine("Player " + action);

                if (Game.HasOrbitEnded())
                {
                    var endType = Game.HasGameEnded();
                    if (endType != GameEndType.None)
                    {
                        var allPlayers = Game.GetAllPlayers();

                        foreach (var player in allPlayers)
                        {
                            var user = await Server.Service.GetUser(player.Username);
                            user.Money = player.Money;
                            await Server.Service.UpdateUser(user);

                            if (player.IsDisconnected)
                                Game.PlayerDisconnected(player.Place);
                        }

                        if (endType == GameEndType.Showdown)
                            foreach (var player in allPlayers)
                            {
                                if (player.IsPlaying)
                                {
                                    var gameEndMsg = new GameEndServer
                                    {
                                        Cards = player.Hand.ToList(),
                                        Place = player.Place,
                                        Showdown = true
                                    };
                                    Server.SendMessageToAllExcept(gameEndMsg, player.ID, Game);
                                }
                            }

                        await StartNewGameWithDelay();
                        return;
                    }
                    else
                    {
                        var Cards = Game.GetPendingCards();

                        var cardMsg = new CardInfoServer
                        {
                            Cards = Cards,
                            ToHand = false
                        };
                        Server.SendMessageToAll(cardMsg, Game);
                        Console.WriteLine("Sent cards to board");
                    }
                }

                Game.CurrentPlayerAfkTimerStart();
            }
            else
                Console.WriteLine("Action failed");
        }

        private async Task StartNewGameWithDelay(int delay = 3000)
        {
            await Task.Delay(delay);
            StartGame();
        }
    }
}