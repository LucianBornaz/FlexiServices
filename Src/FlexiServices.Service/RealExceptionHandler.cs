using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FlexiServices.Service
{
    public class RealExceptionHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            MessageFault faultMessage = MessageFault.CreateFault(
                new FaultCode("Sender"),
                new FaultReason(error.Message),
                error, // This will go into the detail property
                new NetDataContractSerializer());

            fault = Message.CreateMessage(version, faultMessage, null);
        }
    }
}
