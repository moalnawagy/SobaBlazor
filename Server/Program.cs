using mqttASP;
using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.Extensions;
using MQTTnet.Protocol;

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

builder.Services.AddMqttWebSocketServerAdapter();
builder.Services
    .AddHostedMqttServer(mqttServer => mqttServer.WithoutDefaultEndpoint().WithApplicationMessageInterceptor(Handlers.OnNewMessage).WithConnectionValidator(c =>
    {
        
        Console.WriteLine(c);
        Console.WriteLine(c.ClientId );

        c.ReasonCode = MqttConnectReasonCode.Success;
    }))
    .AddMqttConnectionHandler()
                
    .AddConnections();

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


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
