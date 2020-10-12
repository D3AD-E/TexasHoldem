namespace TexasHoldemCommonAssembly.Game.Entities
{
    public class Player : PlayerBase
    {
        public Card[] Hand { get; set; }

        public Holding Holding { get; set; }

        public bool IsPlaying { get; set; }

        public double PreviousBet { get; set; }

        public double CurrentBet { get; set; }

        public bool IsDisconnected { get; set; }

        public Player() : base()
        {
            IsDisconnected = false;
            PreviousBet = 0;
            CurrentBet = 0;
            this.Hand = new Card[2];
            this.IsPlaying = true;
        }

        public Player(PlayerBase player, bool isPlaying = true) : this()
        {
            this.Money = player.Money;
            this.Username = player.Username;
            this.Place = player.Place;
            this.Hand = new Card[2];
            this.IsPlaying = isPlaying;
        }
    }
}