using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TexasHoldem.Client.Utils.UserControls
{
    public partial class TopBorder : UserControl
    {
        [Description("Does the border contain Minimize button"), Category("View")]
        public bool HasMinimize { get; set; }

        //[Description("Color of minimize button"), Category("View")]
        //public Color MinimizeColor { get; set; }
        //[Description("Color of Close button"), Category("View")]
        //public Color CloseColor { get; set; }

        public TopBorder()
        {
            HasMinimize = true;
            InitializeComponent();
        }

        private void TopBorder_Load(object sender, EventArgs e)
        {
            MinimizeFButton.Visible = HasMinimize;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void TopBorder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(((Form)this.TopLevelControl).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MinimizeFButton_Click(object sender, EventArgs e)
        {
            ((Form)this.TopLevelControl).WindowState = FormWindowState.Minimized;
        }

        private void CloseFButton_Click(object sender, EventArgs e)
        {
            ((Form)this.TopLevelControl).Close();
        }
    }
}