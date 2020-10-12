using System;
using System.Windows.Forms;

namespace TexasHoldem.Client.Forms
{
    public partial class CreateRoomForm : Form
    {
        public string RoomName { get; private set; }

        public int MaxPlayers { get; private set; }

        public CreateRoomForm()
        {
            InitializeComponent();
        }

        private void CancelFButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateFButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RoomNameTextB.Text))
                return;
            RoomName = RoomNameTextB.Text;
            MaxPlayers = (int)MaxPlayersUD.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}