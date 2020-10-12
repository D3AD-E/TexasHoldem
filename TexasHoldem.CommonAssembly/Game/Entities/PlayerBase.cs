using System;

namespace TexasHoldemCommonAssembly.Game.Entities
{
    [Serializable]
    public class PlayerBase
    {
        public double Money { get; set; }

        public string Username { get; set; }

        public int Place { get; set; }

        public PlayerBase()
        {
            //Money = 1000;
        }
    }
}