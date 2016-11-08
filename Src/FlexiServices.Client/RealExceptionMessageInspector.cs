using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FlexiServices.Client
{
    class RealExceptionMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (reply.IsFault)
            {
                // Create a copy of the original reply to allow default WCF processing
                var buffer = reply.CreateBufferedCopy(Int32.MaxValue);

                // Create a copy to work with
                var copy = buffer.CreateMessage();

                // Restore the original message 
                reply = buffer.CreateMessage();

                var exception = copy.GetException();

                if (exception != null)
                {
                    throw exception;
                }
            }

        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }
    }
}
