using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal static class BindingHelper
    {
        private static readonly string _baseUrl = "localhost:8524";
        private static readonly TimeSpan _inactivityTimeout = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan _openCloseTimeout = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan _leaseTimeout = TimeSpan.FromSeconds(10);

        public static Binding GetBinding()
        {
            var tcpBinding = new NetTcpBinding
            {
                CloseTimeout = _openCloseTimeout,
                OpenTimeout = _openCloseTimeout,
                ReceiveTimeout = _inactivityTimeout,
                SendTimeout = _inactivityTimeout,

                TransferMode = TransferMode.Buffered,
                ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas
                {
                    MaxDepth = 32,
                    MaxBytesPerRead = 4096,
                    MaxNameTableCharCount = 16384
                },
                ReliableSession = new OptionalReliableSession
                {
                    Ordered = true,
                    InactivityTimeout = _inactivityTimeout,
                    Enabled = false
                }
            };
            var customBinding = new CustomBinding(tcpBinding);
            var transportElement = customBinding.Elements.Find<TcpTransportBindingElement>();
            transportElement.ConnectionPoolSettings.LeaseTimeout = _leaseTimeout;
            transportElement.ConnectionPoolSettings.IdleTimeout = _inactivityTimeout;
            return customBinding;
        }

        public static EndpointAddress GetAddress(string path)
        {
            return new EndpointAddress(new Uri($"net.tcp://{_baseUrl}/{path}"));
        }
    }
}
