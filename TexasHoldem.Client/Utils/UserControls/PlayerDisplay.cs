using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TexasHoldem.Client
{
    public partial class PlayerDisplay : UserControl
    {
        public PlayerDisplay()
        {
            InitializeComponent();
            //EmptySeatSetup();
        }

        private void EmptySeatSetup()
        {
            UsernameLabel.Text = "Empty seat";
            MoneyLabel.Text = string.Empty;
            ActionLabel.Text = string.Empty;
        }

        private void PlayerDisplay_Load(object sender, EventArgs e)
        {
            MoneyLabel.Text = string.Empty;
            ActionLabel.Text = string.Empty;
        }
    }
}
