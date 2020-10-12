using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TexasHoldem.CommonAssembly.Game.Entities;

namespace TexasHoldem.Client.Utils.UserControls
{
    public partial class PotsView : UserControl
    {
        public Stack<Pot> Pots { get; set; }

        public PotsView()
        {
            Pots = new Stack<Pot>();
            InitializeComponent();
        }

        public void RefreshPots()
        {
            var sb = new StringBuilder();
            double potSum = 0;

            foreach (var pot in Pots)
            {
                sb.Append("Pot: " + pot.Size + Environment.NewLine);
                potSum += pot.Size;
            }
            sb.Append("Sum: " + potSum);
            this.Invoke((Action)(() => label1.Text = sb.ToString()));
        }
    }
}