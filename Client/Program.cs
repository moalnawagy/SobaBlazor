using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using SobaBlazor.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
// MqttFactory factory = new MqttFactory();
// IMqttClient mqttClient = factory.CreateMqttClient();
// IMqttClientOptions options = new MqttClientOptionsBuilder()
//     .WithTcpServer("127.0.0.1", 1883) // Port is optional
//     .Build();
// await mqttClient.ConnectAsync(options, CancellationToken.None);
// builder.Services.AddScoped(sp => mqttClient);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
