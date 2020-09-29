using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class GameInfoServer : ServerMessageBase
    {
        public int ButtonPlace { get; set; }

        public int BBPlace { get; set; }

        public int PlayerToAct { get; set; }

        public double BBBet { get; set; }

        public double Ante { get; set; }
    }
}
