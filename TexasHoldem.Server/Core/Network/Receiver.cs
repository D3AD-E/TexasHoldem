using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldemCommonAssembly.Network.Message;
using TexasHoldemCommonAssembly.Network.Message.Base;
using TexasHoldemServer.Utils;

namespace TexasHoldemServer.Core.Network
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

        private GameLogic Game;

        public Queue<MessageBase> MessageQueue { get; private set; }

        public long TotalBytesUsage { get; set; }

        public TcpClient Client { get; set; }

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
            Game.HandlePlayerAction(ID, PlayerAction.Fold, 0);
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
                if (type == typeof(ForceStart))
                {
                    ForceStartHandler();
                }
                else if (type == typeof(PlayerActionClient))
                {
                    PlayerActionHandlerAsync(msg as PlayerActionClient).ConfigureAwait(false);
                }
                else if (type == typeof(JoinRequest))
                {
                    JoinHandler(msg as JoinRequest);
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
            if(string.Compare(CurrentPhrase, req.Phrase)==0)
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
                JoinHandler(req);
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
                if(Server.Games.ContainsKey(req.RoomId))
                {
                    Game = Server.Games[req.RoomId];
                    JoinHandler(req);
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

            if(error)
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
            if (req.Username.Length>20|| req.Username.Length < 3 || password.Length < 8 || !password.Any(char.IsDigit) || !password.Any(char.IsDigit) || password.Length>50)
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

            if (user!= null)
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
                catch(Exception e)
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
        private void JoinHandler(RoomRequestsMessageBase req)
        {
            Console.WriteLine("Someone tried to join");

            JoinResponse res = new JoinResponse(req)
            {
                PlayersInRoom = Game.GetPlayers(),
                YourPlace = Game.AddPlayer(ID, this.Username),
                PotSize = Game.PotSize,
            };
            SendMessage(res);
            if (!res.HasError)
            {
                NewPlayerJoinedServer newPlayerJoinedMsg = new NewPlayerJoinedServer
                {
                    Player = new PlayerBase
                    {
                        Username = this.Username,
                        Place = res.YourPlace,
                    }
                };
                Server.SendMessageToAllExcept(newPlayerJoinedMsg, this);
            }
        }

        [Obsolete]
        private void JoinHandler(JoinRequest req)
        {
            Console.WriteLine("Someone tried to join");
            JoinResponse res = new JoinResponse(req)
            {
                PlayersInRoom = Game.GetPlayers(),
                YourPlace = Game.AddPlayer(ID, req.Username),
                PotSize = Game.PotSize,
            };
            SendMessage(res);
            if (!res.HasError)
            {
                NewPlayerJoinedServer newPlayerJoinedMsg = new NewPlayerJoinedServer
                {
                    Player = new PlayerBase
                    {
                        Username = req.Username,
                        Place = res.YourPlace,
                    }
                };
                Server.SendMessageToAllExcept(newPlayerJoinedMsg, this);
            }
        }

        private void ForceStartHandler()
        {
            Game.Start();
            for (int i = 0; i < Server.Receivers.Count; i++)
            {
                var msg = new CardInfoServer
                {
                    Cards = Game.DealCardsToHand(i),
                    ToHand = true,
                    Place = i
                };
                Server.Receivers[i].SendMessage(msg);
            }
            Console.WriteLine("Started a game");

            var gameInfo = new GameInfoServer
            {
                BBPlace = Game.GetBBPlace(),
                ButtonPlace = Game.GetButtonPlace(),
                PlayerToAct = Game.GetCurrentPlayerPlace(),
                BBBet = Game.BBlindBet
            };
            Server.SendMessageToAll(gameInfo);
        }

        private async Task PlayerActionHandlerAsync(PlayerActionClient msg)        //REDO
        {
            if (Game.HandlePlayerAction(ID, msg.Action, msg.RaiseAmount))
            {
                PlayerActionServer playerActionMsg = new PlayerActionServer
                {
                    Action = msg.Action,
                    RaiseAmount = msg.RaiseAmount,
                    PlayerPos = Game.GetCurrentPlayerPlace()
                };
                Server.SendMessageToAllExcept(playerActionMsg, this);
                Console.WriteLine("Player " + msg.Action);

                var Cards = Game.GetPendingCardsIfAny();
                if (Cards != null && Cards.Count()!=0)
                {
                    var cardMsg = new CardInfoServer
                    {
                        Cards = Cards,
                        ToHand = false
                    };
                    Server.SendMessageToAll(cardMsg);
                    Console.WriteLine("Sent cards to board");
                }
                else if (Game.HasGameEnded())
                {
                    var allPlayers = Game.GetAllPlayers();
                    foreach (var player in allPlayers)
                    {
                        var user = await Server.Service.GetUser(player.Username);
                        user.Money = player.Money;
                        await Server.Service.UpdateUser(user);

                        if(player.IsPlaying)
                        {
                            var gameEndMsg = new GameEndServer
                            {
                                Cards = player.Hand.ToList(),
                                Place = player.Place,
                                Showdown = true
                            };
                            Server.SendMessageToAllExcept(gameEndMsg, player.ID);
                        }
                    }


                    //var activePlayers = Game.GetActivePlayers();
                    //foreach (var player in activePlayers)
                    //{
                    //    var gameEndMsg = new GameEndServer
                    //    {
                    //        Cards = player.Hand.ToList(),
                    //        Place = player.Place,
                    //        Showdown = true
                    //    };
                    //    Server.SendMessageToAllExcept(gameEndMsg, player.ID);
                    //}
                }
            }
            else
                Console.WriteLine("Action failed");
        }
    }
}