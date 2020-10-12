using System;
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
            InvokeUI(() => AfkProgressBar.Value += by);

            if (AfkProgressBar.Value == AfkProgressBar.Maximum)
            {
                return true;
            }
            else
                return false;
        }

        public void RefreshPlayerAfk()
        {
            InvokeUI(() =>
            {
                AfkProgressBar.Value = 0;
                AfkProgressBar.Hide();
                ActionLabel.Show();
            });
        }

        public void SetupPlayerAfkAwaiting()
        {
            InvokeUI(() =>
            {
                AfkProgressBar.Show();
                ActionLabel.Hide();
            });
        }

        private void InvokeUI(Action action)
        {
            this.Invoke(action);
        }
    }
}