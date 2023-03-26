using app.Invocables;
using MQTTnet.AspNetCore.Extensions;
using MQTTnet.Protocol;
using SobaBlazor.Server;
using SobaBlazor.Server.MqttHandler;
using SobaBlazor.Server.Services;
using Coravel;
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(
    o =>
    {
        o.ListenAnyIP(1883, l => l.UseMqtt()); // MQTT pipeline
        o.ListenAnyIP(5072); // Default HTTP pipeline
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScheduler();
builder.Services.AddTransient<WriteRandomPlaneAltitudeInvocable>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<HubHandlerBase, HubHandler>();
builder.Services.AddHostedService<Handlers>();
builder.Services.AddSingleton<InfluxDBService>();
builder.Services
    .AddHostedMqttServer(mqttServer => mqttServer.WithoutDefaultEndpoint().WithApplicationMessageInterceptor(  Handlers.OnNewMessage).WithConnectionValidator(c =>
    {
        
        Console.WriteLine(c);
        Console.WriteLine(c.ClientId );

        c.ReasonCode = MqttConnectReasonCode.Success;
    }))
    .AddMqttConnectionHandler()
                
    .AddConnections();

builder.Services.AddMqttWebSocketServerAdapter();
builder.Services.AddMqttTcpServerAdapter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseMqttServer(server =>
{
    // Todo: Do something with the server
});


app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapHub<HubHandler>("/hub");
app.UseMqttEndpoint(path:"/mqtt");

// ((IApplicationBuilder)app).ApplicationServices.UseScheduler(scheduler =>
// {
//     scheduler
//         .Schedule<WriteRandomPlaneAltitudeInvocable>()
//         .EveryFiveSeconds();
// });
app.Run();
