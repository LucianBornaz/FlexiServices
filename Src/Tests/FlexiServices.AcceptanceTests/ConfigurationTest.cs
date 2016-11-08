using System;
using System.ServiceModel;
using FlexiServices.AcceptanceTests.Service;
using NUnit.Framework;

namespace FlexiServices.AcceptanceTests
{
    [TestFixture]
    public class ConfigurationTest
    {
        private ServiceHost _serviceHost;
        private ICalculatorService _client;

        [SetUp]
        public void SetUp()
        {
            _serviceHost = CreateService();
            _serviceHost.Open();

            _client = CreateClient();
        }

        private ServiceHost CreateService()
        {
            _serviceHost = new ServiceHost(typeof(CalculatorService));
            return _serviceHost;
        }

        private ICalculatorService CreateClient()
        {
            var factory = new ChannelFactory<ICalculatorService>("*");
            return factory.CreateChannel();
        }

        [TearDown]
        public void TearDown()
        {
            var channel = _client as IClientChannel;
            if (channel != null && (channel.State != CommunicationState.Closed && channel.State != CommunicationState.Closing))
                channel.Close();

            if (_serviceHost != null && (_serviceHost.State != CommunicationState.Closed && _serviceHost.State != CommunicationState.Closing))
                _serviceHost.Close();
        }

        [Test]
        public void Should_Throw_NotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() => _client.Sum(1, 1));
        }
    }
}
