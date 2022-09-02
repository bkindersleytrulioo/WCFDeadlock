# WCFDeadlock

- run the Server application
- run the Client application

The synchronous call produces something like

```
Sync call produced error in 7947ms - This request operation sent to net.tcp://localhost:8524/MyContract did not receive a reply within the configured timeout (00:00:04.9896988).  The time allotted to this operation may have been a portion of a longer timeout.  This may be because the service is still processing the operation or because the service was unable to send a reply message.  Please consider increasing the operation timeout (by casting the channel/proxy to IContextChannel and setting the OperationTimeout property) and ensure that the service is able to connect to the client.
```

- uncommenting the `SetSynchronizationContext` line  in the Client resolves the timeout
- so does uncommenting the `Task.Yield` line
- so does uncommenting the re-initialization of the client