using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TexasHoldem.Client.Forms
{
    public partial class FlatMessageBox : Form
    {
        public FlatMessageBox(string topText, string bottomText)
        {
            InitializeComponent();

            BottomLabel.MaximumSize = new Size(panel1.Width-30, 0);
            BottomLabel.AutoSize = true;

            TopLabel.Text = topText;
            BottomLabel.Text = bottomText;

            //if(panel1.Width + panel1.Location.X < TopLabel.Size.Width + TopLabel.Location.X)
            {
                TopLabel.Left = panel1.Location.X + (panel1.Width-TopLabel.Size.Width) / 2;
            }
        }

        public FlatMessageBox(string toptextText, string bottomText, Color topColor , Color botColor) :this(toptextText, bottomText)
        {
            TopLabel.ForeColor = topColor;
            BottomLabel.ForeColor = botColor;
        }

        private void OKFButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
