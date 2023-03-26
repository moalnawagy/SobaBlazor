using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MQTTnet;
using MQTTnet.Client;
using SobaBlazor.Client;
using SobaBlazor.Client.JsInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var factory = new MqttFactory();
// var mqttClient = factory.CreateMqttClient();
// var options = new MqttClientOptionsBuilder()
//     .WithWebSocketServer("broker.hivemq.com:8000/mqtt")
//     .Build();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<MqttJsInterop>();
await builder.Build().RunAsync();
