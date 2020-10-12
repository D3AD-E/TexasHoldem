using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexasHoldem.Client.Utils;

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

            var messageBox = new FlatMessageBox("Connecting", "Trying to reach the server...");
            messageBox.Show();
            Task.Run(() => Connect())
                .ContinueWith(t =>
                {
                    InvokeUI(() =>
                    {
                        messageBox.Close();
                    });
                    if (t.Exception != null)
                    {
                        ExeptionHandler.HandleExeption(t.Exception);
                        EnableControls(true);
                        return;
                    }
                    SignUp();
                });
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

        private Task Connect()
        {
            if (_client.Status == TexasHoldemCommonAssembly.Enums.Status.Disconnected)
            {
                try
                {
                    _client.Connect("localhost", 8888);
                }
                catch (Exception ex)
                {
                    return Task.FromException(ex);
                }
            }
            return Task.CompletedTask;
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