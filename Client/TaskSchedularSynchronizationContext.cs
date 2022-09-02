namespace Client
{
    /// <summary>
    /// Pulled from https://github.com/dotnet/samples/blob/2cf486af936261b04a438ea44779cdc26c613f98/csharp/parallel/ParallelExtensionsExtras/Extensions/TaskSchedulerExtensions.cs
    /// Mostly just to have a synchronization context; I don't have any reason to believe this is a "correct" implementation choice.
    /// </summary>
    public class TaskSchedulerSynchronizationContext : SynchronizationContext
    {
        /// <summary>The scheduler.</summary>
        private readonly TaskScheduler _scheduler;

        /// <summary>Initializes the context with the specified scheduler.</summary>
        /// <param name="scheduler">The scheduler to target.</param>
        internal TaskSchedulerSynchronizationContext(TaskScheduler scheduler) =>
            _scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));

        /// <summary>Dispatches an asynchronous message to the synchronization context.</summary>
        /// <param name="d">The System.Threading.SendOrPostCallback delegate to call.</param>
        /// <param name="state">The object passed to the delegate.</param>
        public override void Post(SendOrPostCallback d, object state) => Task.Factory.StartNew(() =>
            d(state), CancellationToken.None, TaskCreationOptions.None, _scheduler);

        /// <summary>Dispatches a synchronous message to the synchronization context.</summary>
        /// <param name="d">The System.Threading.SendOrPostCallback delegate to call.</param>
        /// <param name="state">The object passed to the delegate.</param>
        public override void Send(SendOrPostCallback d, object state)
        {
            var t = new Task(() => d(state));
            t.RunSynchronously(_scheduler);
            t.Wait();
        }
    }
}
