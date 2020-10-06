using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TexasHoldem.Client.Core.Network;
using TexasHoldem.Client.Forms;

namespace TexasHoldem.Client
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var startUpForm = new StartupLoginForm();
            Application.Run(startUpForm);

            if(startUpForm.IsUserAuthenticated)
            {
                var roomForm = new RoomsBrowserForm(startUpForm.Username, startUpForm.Money);
                Application.Run(roomForm);

            }
            if(Core.Network.Client.Instance.Status == TexasHoldemCommonAssembly.Enums.Status.Connected)
            {
                Core.Network.Client.Instance.Disconnect();
            }
        }
    }
}
