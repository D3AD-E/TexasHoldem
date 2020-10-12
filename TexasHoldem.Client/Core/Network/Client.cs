using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using TexasHoldem.CommonAssembly.Network.Message;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Network.Message;

namespace TexasHoldem.Client.Core.Network
{
    public class Client
    {
        private Thread ReceivingThread;
        private Thread SendingThread;
        private List<ResponseCallbackObject> CallBacks;

        public TcpClient TcpClient { get; set; }

        public string Address { get; private set; }

        public int Port { get; private set; }

        public Status Status { get; private set; }

        public Queue<MessageBase> MessageQueue { get; private set; }

        public event Delegates.MessageReceivedDelegate MessageReceived;

        private static Client _instance;

        public static Client Instance
        {
            get { return _instance ??= new Client(); }
        }

        private Client()
        {
            CallBacks = new List<ResponseCallbackObject>();
            MessageQueue = new Queue<MessageBase>();
            Status = Status.Disconnected;
        }

        public void Connect(string address, int port)
        {
            Address = address;
            Port = port;
            TcpClient = new TcpClient();
            TcpClient.Connect(Address, Port);
            Status = Status.Connected;
            TcpClient.ReceiveBufferSize = 1024;
            TcpClient.SendBufferSize = 1024;

            ReceivingThread = new Thread(ReceivingMethod)
            {
                IsBackground = true
            };
            ReceivingThread.Start();

            SendingThread = new Thread(SendingMethod)
            {
                IsBackground = true
            };
            SendingThread.Start();
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
                    MessageBase m = MessageQueue.Dequeue();

                    BinaryFormatter f = new BinaryFormatter();
                    try
                    {
                        f.Serialize(TcpClient.GetStream(), m);
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
                if (TcpClient.Available > 0)
                {
                    BinaryFormatter f = new BinaryFormatter();
                    MessageBase msg = f.Deserialize(TcpClient.GetStream()) as MessageBase;
                    OnMessageReceived(msg);
                }

                Thread.Sleep(30);
            }
        }

        protected virtual void OnMessageReceived(MessageBase msg)
        {
            Type type = msg.GetType();
            if (msg is ResponseMessageBase)
            {
                InvokeMessageCallback(msg, (msg as ResponseMessageBase).DeleteCallbackAfterInvoke);
            }
            else
            {
                if (msg is ServerMessageBase)
                {
                    var args = new MessageReceivedEventArgs(msg as ServerMessageBase);
                    MessageReceived?.Invoke(args);
                }
            }
        }

        public void DisconnectFromRoom()
        {
            try
            {
                SendMessage(new DisconnectFromRoomRequest());
            }
            catch { }
        }

        public void Disconnect()
        {
            MessageQueue.Clear();
            CallBacks.Clear();

            try
            {
                SendMessage(new DisconnectRequest());
            }
            catch { }
            //Thread.Sleep(1000);
            Status = Status.Disconnected;
            TcpClient.Client.Disconnect(false);
            TcpClient.Close();
            //OnClientDisconnected();
        }

        private void AddCallback(Delegate callBack, MessageBase msg)
        {
            if (callBack != null)
            {
                Guid callbackID = Guid.NewGuid();
                ResponseCallbackObject responseCallback = new ResponseCallbackObject()
                {
                    ID = callbackID,
                    CallBack = callBack
                };

                msg.CallbackID = callbackID;
                CallBacks.Add(responseCallback);
            }
        }

        private void InvokeMessageCallback(MessageBase msg, bool deleteCallback)
        {
            var callBackObject = CallBacks.SingleOrDefault(x => x.ID == msg.CallbackID);

            if (callBackObject != null)
            {
                if (deleteCallback)
                {
                    CallBacks.Remove(callBackObject);
                }
                callBackObject.CallBack.DynamicInvoke(this, msg);
            }
        }

        public void RequestHeartbeat(Action<Client, HeartbeatResponse> callback)
        {
            var request = new HeartbeatRequest();
            AddCallback(callback, request);
            SendMessage(request);
        }

        public void RequestAddMoneyPhraseConfirm(string phrase, Action<Client, AddMoneyResponse> callback)
        {
            var request = new AddMoneyRequest
            {
                Phrase = phrase
            };
            AddCallback(callback, request);
            SendMessage(request);
        }

        public void RequestAddMoneyPhrase(Action<Client, AddMoneyPhraseResponse> callback)
        {
            var request = new AddMoneyPhraseRequest();
            AddCallback(callback, request);
            SendMessage(request);
        }

        public void RequestCreateRoom(string name, int maxPlayers, Action<Client, JoinResponse> callback)
        {
            var request = new CreateRoomRequest
            {
                Name = name,
                MaxPlayers = maxPlayers
            };
            AddCallback(callback, request);
            SendMessage(request);
        }

        public void RequestJoinRoom(Guid roomId, Action<Client, JoinResponse> callback)
        {
            var request = new JoinRoomRequest
            {
                RoomId = roomId
            };
            AddCallback(callback, request);
            SendMessage(request);
        }

        public void RequestShowRooms(Action<Client, ShowRoomsResponse> callback)
        {
            var request = new ShowRoomsRequest();
            AddCallback(callback, request);
            SendMessage(request);
        }

        public void RequestAuthenticate(string username, string password, Action<Client, AuthenticateResponse> callback)
        {
            var request = new AuthenticateRequest
            {
                Username = username,
                Password = password
            };
            AddCallback(callback, request);
            SendMessage(request);
        }

        public void RequestSignUp(string username, string password, Action<Client, SignUpResponse> callback)
        {
            var request = new SignUpRequest
            {
                Username = username,
                Password = password
            };
            AddCallback(callback, request);
            SendMessage(request);
        }

        public void SendPlayerAction(PlayerAction action, double raiseAmount = 0)
        {
            var message = new PlayerActionClient
            {
                Action = action,
                RaiseAmount = raiseAmount
            };

            SendMessage(message);
        }
    }
}