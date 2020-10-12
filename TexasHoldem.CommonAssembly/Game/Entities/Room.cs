using System;

namespace TexasHoldemCommonAssembly.Game.Entities
{
    [Serializable]
    public class Room
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double BBBet { get; set; }

        public double SBBet { get; set; }

        public int MaxPlayersAmount { get; set; }

        public int CurrentPlayersAmount { get; set; }
    }
}