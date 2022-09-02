using System.Diagnostics;
using Client;

var client = new WCFClient();

//uncommenting this resolves timeout
//SynchronizationContext.SetSynchronizationContext(new TaskSchedulerSynchronizationContext(TaskScheduler.Default));
Console.WriteLine($"SynchronizationContext is null: {SynchronizationContext.Current == null}");

var sw = Stopwatch.StartNew();
try
{
    var result = await client.Operation1Async("MyName");
    Console.WriteLine($"Finished async call in {sw.ElapsedMilliseconds}ms - {result}");
}
catch (Exception e)
{
    Console.WriteLine($"Async call produced error in {sw.ElapsedMilliseconds}ms - {e.Message}");
}

//uncommenting this also resolves timeout
//https://stackoverflow.com/questions/37087103/wcf-client-hangs-on-any-operation-after-awaiting-async-operation-in-console-appl
//await Task.Yield();

try
{
    var result = client.Operation2("MyName");
    Console.WriteLine($"Finished sync call in {sw.ElapsedMilliseconds}ms - {result}");
}
catch (Exception e)
{
    Console.WriteLine($"Sync call produced error in {sw.ElapsedMilliseconds}ms - {e.Message}");
}
