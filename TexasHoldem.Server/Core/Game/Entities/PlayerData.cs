using System;
using System.Collections.Generic;
using System.Text;
using TexasHoldemCommonAssembly.Enums;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldemServer.Core.Game.Entities
{
    public class PlayerData : Player
    {
        public Guid ID { get; private set; }

        public PlayerData(Guid id, int place, string username) :base()
        {
            ID = id;
            Username = username;
            Place = place;
        }
    }
}
