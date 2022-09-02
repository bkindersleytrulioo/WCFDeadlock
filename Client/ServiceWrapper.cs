using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Client
{
    public class ServiceWrapper<TChannel> : ClientBase<TChannel>, IDisposable
        where TChannel : class
    {
        public ServiceWrapper() { }

        public ServiceWrapper(string endpointConfigurationName, string url) : base(endpointConfigurationName, url)
        { }

        public ServiceWrapper(string endpointConfigurationName) : base(endpointConfigurationName) { }

        public ServiceWrapper(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress) { }

        public TChannel Client => Channel;

        public new void Close()
        {
            ((IDisposable)this).Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (State == CommunicationState.Faulted)
                    Abort();

                if (State != CommunicationState.Closed)
                    base.Close();

            }
            catch (CommunicationException)
            {
                Abort();
            }
            catch (TimeoutException)
            {
                Abort();
            }
            catch
            {
                Abort();
                throw;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
