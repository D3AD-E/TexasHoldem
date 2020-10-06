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
            EmptySeatSetup();
        }

        public void EmptySeatSetup()
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
        public void SetupAfkBar(int maxValue)
        {
            AfkProgressBar.Maximum = maxValue;
            AfkProgressBar.Value = 0;
        }
        public bool IncreasePlayerAfk(int by)
        {
            AfkProgressBar.Value += by;
            if (AfkProgressBar.Value == AfkProgressBar.Maximum)
            {
                return true;
            }
            else
                return false;
        }
        public void RefreshPlayerAfk()
        {
            AfkProgressBar.Value = 0;
            AfkProgressBar.Hide();
            ActionLabel.Show();
        }
        public void SetupPlayerAfkAwaiting()
        {
            AfkProgressBar.Show();
            ActionLabel.Hide();
        }
    }
}
