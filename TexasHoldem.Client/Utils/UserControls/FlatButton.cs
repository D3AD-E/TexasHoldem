using System;
using System.Drawing;
using System.Windows.Forms;

namespace TexasHoldem.Client.Utils.UserControls
{
    public class FlatButton : Button
    {
        private Color ForeColorPrev;

        public FlatButton() : base()
        {
            FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;

            FlatAppearance.MouseOverBackColor = BackColor;
            BackColorChanged += (s, e) =>
            {
                FlatAppearance.MouseOverBackColor = BackColor;
            };

            ForeColorPrev = ForeColor;

            this.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            this.FlatAppearance.BorderSize = 1;
            this.ForeColor = FlatAppearance.BorderColor;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            this.FlatAppearance.BorderSize = 0;
            this.ForeColor = ForeColorPrev;
        }
    }
}