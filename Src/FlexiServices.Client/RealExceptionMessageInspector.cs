using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace FlexiServices.Client
{
    class RealExceptionMessageInspector : IClientMessageInspector
    {
        private const string DetailElementName = "Detail";
        private static readonly NetDataContractSerializer Serializer = new NetDataContractSerializer();

        #region IClientMessageInspector Members

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

                var exception = ReadFaultDetail(copy) as Exception;

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
        #endregion


        /// <summary>
        /// Used to locate the FaultDeail of the reply message which will be used to generate the new Exception
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        private static object ReadFaultDetail(Message reply)
        {

            using (var reader = reply.GetReaderAtBodyContents())
            {
                // Find <soap:Detail>
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && DetailElementName.Equals(reader.LocalName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        break;
                    }
                }
                if (reader.EOF)
                {
                    return null;
                }

                // Move to the contents of <soap:Detail>
                if (!reader.Read())
                {
                    return null;
                }

                // Deserialize the fault

                try
                {
                    return Serializer.ReadObject(reader);
                }

                catch (SerializationException ex)
                {
                    return new CommunicationException("SerializationException", ex);
                }
            }
        }
    }
}
