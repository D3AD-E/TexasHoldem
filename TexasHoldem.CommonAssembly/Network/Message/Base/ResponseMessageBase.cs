using System;

namespace TexasHoldemCommonAssembly.Network.Message
{
    [Serializable]
    public class ResponseMessageBase : MessageBase
    {
        public bool DeleteCallbackAfterInvoke { get; set; }

        public ResponseMessageBase(RequestMessageBase request)
        {
            DeleteCallbackAfterInvoke = true;
            CallbackID = request.CallbackID;
        }
    }
}