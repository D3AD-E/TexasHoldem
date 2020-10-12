using System;
using System.Drawing;
using System.Windows.Forms;
using TexasHoldem.Client.Forms;

namespace TexasHoldem.Client.Utils
{
    public static class ExeptionHandler
    {
        public static void HandleExeption(Exception exception, bool critical = false)
        {
            var errorMsg = new FlatMessageBox("Error", exception.Message, Color.Red, Color.Black);
            errorMsg.ShowDialog();
            if (critical)
                Application.Exit();
        }
    }
}