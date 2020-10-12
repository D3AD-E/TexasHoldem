using System;
using System.Drawing;
using System.Windows.Forms;
using TexasHoldem.Client.Utils;

namespace TexasHoldem.Client.Forms
{
    public partial class AddMoneyForm : Form
    {
        private readonly Core.Network.Client _client;
        public string TypedMessage { get; private set; }

        public AddMoneyForm()
        {
            InitializeComponent();
            _client = Core.Network.Client.Instance;
            MoneyPhaseLabel.MaximumSize = new Size(panel1.Width - 20, 0);
            MoneyPhaseLabel.AutoSize = true;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            TypedMessage = textBox1.Text.Replace(System.Environment.NewLine, string.Empty);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddMoneyForm_Load(object sender, EventArgs e)
        {
            _client.RequestAddMoneyPhrase((senderClient, res) =>
            {
                if (res.HasError)
                {
                    ExeptionHandler.HandleExeption(res.Exception);
                }
                else
                    InvokeUI(() =>
                    {
                        MoneyPhaseLabel.Text = res.Phrase;
                    });
            });
        }

        private void InvokeUI(Action action)
        {
            this.Invoke(action);
        }
    }
}