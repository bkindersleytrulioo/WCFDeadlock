using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Server;
using Shared.Ports;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseNetTcp(8524);
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.AllowSynchronousIO = true;
    options.ListenAnyIP(8084);
});

builder.Services.AddServiceModelServices()
    .AddServiceModelMetadata();

var app = builder.Build();

var tmp1 = app is IApplicationBuilder;

var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
serviceMetadataBehavior.HttpGetEnabled = true;
serviceMetadataBehavior.HttpGetUrl = new Uri("http://localhost:8084/metadata");

app.UseServiceModel(serviceBuilder =>
{
    var binding = new NetTcpBinding();

    serviceBuilder.AddService<MyService>()
        .AddServiceEndpoint<MyService, IMyContract>(binding, "/MyContract");
});

app.Run();