using TexasHoldemCommonAssembly.Network.Message;

namespace TexasHoldem.Client.Core.Network
{
    public class MessageReceivedEventArgs
    {
        public ServerMessageBase Message { get; private set; }

        public MessageReceivedEventArgs(ServerMessageBase msg)
        {
            Message = msg;
        }
    }
}