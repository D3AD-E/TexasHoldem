using System;
using System.Collections.Generic;
using System.Net.Sockets;
using TexasHoldem.Server.Core.Services;
using TexasHoldemCommonAssembly.Game.Entities;
using TexasHoldemCommonAssembly.Network.Message;

namespace TexasHoldem.Server.Core.Network
{
    public class Server
    {
        public List<Receiver> Receivers { get; }

        private const int PORT = 8888;

        public bool IsStarted { get; set; }
        public TcpListener Listener { get; private set; }

        public event Delegates.ClientBasicDelegate ClientConnected;

        public Dictionary<Guid, GameLogic> Games { get; private set; }

        public IUserService Service { get; private set; }

        public Server(IServiceProvider provider)
        {
            Service = (IUserService)provider.GetService(typeof(IUserService));
            Receivers = new List<Receiver>();
            Games = new Dictionary<Guid, GameLogic>();
            CreateRoom("ASDAS", 8);
        }

        public void Start()
        {
            if (!IsStarted)
            {
                Console.WriteLine("Server started on port " + PORT);
                Listener = new TcpListener(System.Net.IPAddress.Any, PORT);
                Listener.Start();
                IsStarted = true;

                WaitForConnection();
            }
        }

        public List<Room> GetRooms()
        {
            List<Room> rooms = new List<Room>();
            foreach (var key in Games.Keys)
            {
                var toAdd = new Room
                {
                    Id = key,
                    BBBet = Games[key].BBlindBet,
                    SBBet = Games[key].BBlindBet / 2,
                    Name = Games[key].Name,
                    MaxPlayersAmount = Games[key].MaxPlayerAmount,
                    CurrentPlayersAmount = Games[key].CurrentPlayerAmount
                };
                rooms.Add(toAdd);
            }
            return rooms;
        }

        public GameLogic CreateRoom(string name, int maxPlayers)
        {
            var id = Guid.NewGuid();
            Games.Add(id, new GameLogic(name, maxPlayers));
            return Games[id];
        }

        public void Stop()
        {
            if (IsStarted)
            {
                Listener.Stop();
                IsStarted = false;
            }
        }

        private void WaitForConnection()
        {
            Listener.BeginAcceptTcpClient(new AsyncCallback(ConnectionHandler), null);
        }

        private void ConnectionHandler(IAsyncResult ar)
        {
            lock (Receivers)
            {
                Receiver newClient = new Receiver(Listener.EndAcceptTcpClient(ar), this);
                newClient.Start();
                Receivers.Add(newClient);

                OnClientConnected(newClient);
            }

            WaitForConnection();
        }

        public virtual void OnClientConnected(Receiver receiver)
        {
            ClientConnected?.Invoke(receiver);
            Console.WriteLine("Client connected");
        }

        public void SendMessageToAll(ServerMessageBase msg, GameLogic inGame)
        {
            foreach (var receiver in Receivers)
            {
                if (receiver.Game == inGame)
                    receiver.SendMessage(msg);
            }
        }

        public void SendMessageToAllExcept(ServerMessageBase msg, Receiver notToSendTo)
        {
            foreach (var receiver in Receivers)
            {
                if (receiver == notToSendTo)
                    continue;
                receiver.SendMessage(msg);
            }
        }

        public void SendMessageToAllExcept(ServerMessageBase msg, Receiver notToSendTo, GameLogic inGame)
        {
            foreach (var receiver in Receivers)
            {
                if (receiver == notToSendTo || receiver.Game != inGame)
                    continue;
                receiver.SendMessage(msg);
            }
        }

        public void SendMessageToAllExcept(ServerMessageBase msg, Guid notToSendTo, GameLogic inGame)
        {
            foreach (var receiver in Receivers)
            {
                if (receiver.ID == notToSendTo || receiver.Game != inGame)
                    continue;
                receiver.SendMessage(msg);
            }
        }
    }
}