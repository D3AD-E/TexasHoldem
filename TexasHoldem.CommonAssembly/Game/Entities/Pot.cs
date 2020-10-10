using System;
using System.Collections.Generic;
using System.Text;
using TexasHoldemCommonAssembly.Game.Entities;

namespace TexasHoldem.CommonAssembly.Game.Entities
{
    public class Pot
    {
        public List<Player> Players { get; set; }

        public double Size { get; set; }

        public Pot()
        {
            Size = 0;
            Players = new List<Player>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Pot))
                return false;

            var pot = obj as Pot;
            foreach (var player in pot.Players)
            {
                if (!this.Players.Contains(player))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return Players.GetHashCode();
        }
    }
}
