using System;
using TexasHoldem.Server.Core.Network;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldem.Server.Core.Game.Entities
{
    public class PlayerData : Player
    {
        public Guid ID { get; private set; }

        public Receiver ClientReceiver { get; private set; }

        public PlayerData(Guid id, int place, string username, double money, Receiver receiver) : base()
        {
            ID = id;
            Username = username;
            Place = place;
            IsPlaying = false;
            Money = money;
            ClientReceiver = receiver;
        }
    }
}