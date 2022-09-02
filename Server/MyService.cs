using Shared.Ports;

namespace Server
{
    internal class MyService : IMyContract
    {
        public Task<string> Operation1Async(string name)
        {
            return Task.FromResult($"Hello {name}");
        }

        public string Operation2(string name)
        {
            return $"Hello {name}";
        }
    }
}
