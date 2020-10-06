using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexasHoldem.Client.Core;
using TexasHoldem.Client.Forms;
using TexasHoldem.Client.Utils;

namespace TexasHoldem.Client
{
    public partial class StartupLoginForm : Form
    {
        public Core.Network.Client _client;

        public bool IsUserAuthenticated{get; private set;}

        public double Money { get; private set; }

        public string Username { get; private set; }

        public StartupLoginForm()
        {
            InitializeComponent();
            RememberMeCheckB.Checked = Properties.Settings.Default.RememberPassLogin;

            if (RememberMeCheckB.Checked)
            {
                UsernameTextB.Text = Properties.Settings.Default.Username;
                PasswordTextB.Text = Properties.Settings.Default.Password;
            }

            _client = Core.Network.Client.Instance;
            IsUserAuthenticated = false;
        }

        private void StartupLoginForm_Load(object sender, EventArgs e)
        {
            ActiveControl = UsernameTextB;
        }

        private void SignupFButton_Click(object sender, EventArgs e) //maybe not hide
        {
            var signUpForm = new SignUpForm();
            this.Hide();
            signUpForm.Show();
            signUpForm.Closed += (s, args) => this.Show();
        }

        private void RememberSettings()
        {
            bool isRemembering = RememberMeCheckB.Checked;

            Properties.Settings.Default.RememberPassLogin = isRemembering;

            if (isRemembering)
            {
                Properties.Settings.Default.Username = UsernameTextB.Text;
                Properties.Settings.Default.Password = PasswordTextB.Text;
            }
            else
            {
                Properties.Settings.Default.Username = string.Empty;
                Properties.Settings.Default.Password = string.Empty;
            }

            Properties.Settings.Default.Save();
        }

        private async Task Login()
        {
            if (string.IsNullOrEmpty(UsernameTextB.Text))
            {
                errorProvider.SetError(UsernameTextB, "Username coud not be empty");
            }
            else
            {
                EnableControls(false);
                await Task.Run(() => Connect())
                    .ContinueWith(t =>
                    {
                        if (t.Exception != null)
                        {
                            ExeptionHandler.HandleExeption(t.Exception);
                            EnableControls(true);
                            return;
                        }
                        _client.RequestAuthenticate(UsernameTextB.Text, PasswordTextB.Text, (senderClient, res) =>
                        {
                            EnableControls(true);
                            if (res.HasError)
                            {
                                InvokeUI(() =>
                                {
                                    errorProvider.SetError(PasswordTextB, res.Exception.Message);
                                });
                            }
                            else
                            {
                                RememberSettings();
                                IsUserAuthenticated = true;
                                Money = res.Money;
                                Username = UsernameTextB.Text;
                                InvokeUI(() => this.Close());

                            }
                        });
                    });
            }
        }

        private async void LoginFButton_ClickAsync(object sender, EventArgs e)
        {
            await Login();
        }

        private void EnableControls(bool enabled)
        {
            InvokeUI(() =>
            {
                SignupFButton.Enabled = enabled;
                LoginFButton.Enabled = enabled;

                UsernameTextB.Enabled = enabled;
                PasswordTextB.Enabled = enabled;

                RememberMeCheckB.Enabled = enabled;
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

        private void InvokeUI(Action action)
        {
            this.Invoke(action);
        }

        private void UsernameTextB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                ActiveControl = PasswordTextB;
            }
        }

        private void PasswordTextB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Login().ConfigureAwait(false);
            }
        }
    }
}