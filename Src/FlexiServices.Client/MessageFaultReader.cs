using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace FlexiServices.Client
{
    public static class MessageFaultReader
    {
        private const string DetailElementName = "Detail";
        private static readonly NetDataContractSerializer Serializer = new NetDataContractSerializer();

        /// <summary>
        /// Used to locate the FaultDeail of the reply message which will be used to generate the new Exception
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        public static Exception GetException(this Message reply)
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
                    return (Exception) Serializer.ReadObject(reader);
                }
                catch (SerializationException ex)
                {
                    return new CommunicationException("Could not deserialize the exception", ex);
                }
            }
        }
    }
}
