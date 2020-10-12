using System;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class MessageBase
    {
        public Guid CallbackID { get; set; }
        public bool HasError { get; set; }
        public Exception Exception { get; set; }

        public MessageBase()
        {
            Exception = new Exception();
        }
    }
}