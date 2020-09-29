using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexasHoldem.Client.Core.Network;

namespace TexasHoldem.Client.Forms
{
    public partial class SignUpForm : Form
    {

        private readonly Core.Network.Client _client;
        public SignUpForm()
        {
            _client = Core.Network.Client.Instance;
            InitializeComponent();
        }

        private void SignUpFButton_Click(object sender, EventArgs e)
        {

            if (UsernameTextB.TextLength < 3)
            {
                errorProvider.SetError(Password1TextB, "Username must be at lest 3 character long!");
                return;
            }
            else if (Password1TextB.TextLength < 8)
            {
                errorProvider.SetError(Password1TextB, "Password must be at least 8 characters long!");
                return;
            }
            else if (!Password1TextB.Text.Any(char.IsDigit))
            {
                errorProvider.SetError(Password1TextB, "Password must contain a number!");
                return;
            }
            else if (!Password1TextB.Text.Any(char.IsUpper))
            {
                errorProvider.SetError(Password1TextB, "Password must contain a uppercase char");
                return;
            }
            else if (string.Compare(Password1TextB.Text, Password2TextB.Text) != 0)
            {
                errorProvider.SetError(Password1TextB, "Password do not match");
                return;
            }

            EnableControls(false);

            Task.Run(() => Connect()).ContinueWith(t=>EnableControls(true));
        }

        private void EnableControls(bool enabled)
        {
            InvokeUI(() =>
            {
                SignUpFButton.Enabled = enabled;
                CancelFButton.Enabled = enabled;

                UsernameTextB.Enabled = enabled;
                Password1TextB.Enabled = enabled;
                Password2TextB.Enabled = enabled;
            });
        }

        private void Connect()
        {
            if (_client.Status == TexasHoldemCommonAssembly.Enums.Status.Disconnected)
            {
                try
                {
                    _client.Connect("localhost", 8888);
                }
                catch (Exception ex)
                {
                    var messageBox = new FlatMessageBox("Error while connecting to server", ex.Message, Color.Red, Color.Black);
                    messageBox.ShowDialog();
                    return;
                }
            }
            SignUp();
        }

        private void SignUp()
        {
            _client.RequestSignUp(UsernameTextB.Text, Password1TextB.Text, (senderClient, res) =>
            {
                if (res.HasError)
                {
                    InvokeUI(() =>
                    {
                        errorProvider.SetError(Password1TextB, res.Exception.Message);
                    });
                }
                else
                {
                    var messageBox = new FlatMessageBox("Successfully registered", "You can now log in using your credentials", Color.LightSeaGreen, Color.Black);
                    messageBox.ShowDialog();
                    this.DialogResult = DialogResult.OK;
                    InvokeUI(() => this.Close());

                }
            });
        }

        private void InvokeUI(Action action)
        {
            this.Invoke(action);
        }

        private void CancelFButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
