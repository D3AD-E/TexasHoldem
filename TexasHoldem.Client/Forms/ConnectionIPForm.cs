using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TexasHoldem.Client.Forms
{
    public partial class ConnectionIPForm : Form
    {
        public string Ip { get; private set; }
        public ConnectionIPForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ip = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
