using System.ServiceModel;
using System.ServiceModel.Channels;
using Shared.Ports;

namespace Client
{
    internal class WCFClient : ServiceWrapper<IMyContract>
    {
        private static readonly Binding _binding = BindingHelper.GetBinding();
        private static readonly EndpointAddress _endpointAddress = BindingHelper.GetAddress("MyContract");

        public WCFClient() : base(_binding, _endpointAddress)
        {
        }

        public Task<string> Operation1Async(string name)
        {
            return Client.Operation1Async(name);
        }

        public string Operation2(string name)
        {
            return Client.Operation2(name);
        }
    }
}
