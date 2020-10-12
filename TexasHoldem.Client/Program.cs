using System;
using System.Windows.Forms;
using TexasHoldem.Client.Forms;

namespace TexasHoldem.Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var startUpForm = new StartupLoginForm();
            Application.Run(startUpForm);

            if (startUpForm.IsUserAuthenticated)
            {
                var roomForm = new RoomsBrowserForm(startUpForm.Username, startUpForm.Money);
                Application.Run(roomForm);
            }
            if (Core.Network.Client.Instance.Status == TexasHoldemCommonAssembly.Enums.Status.Connected)
            {
                Core.Network.Client.Instance.Disconnect();
            }
        }
    }
}