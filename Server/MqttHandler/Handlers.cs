using System.Text;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Microsoft.AspNetCore.SignalR;
using MQTTnet.Server;
using SobaBlazor.Server.Services;

namespace SobaBlazor.Server.MqttHandler
{
    public interface IHandlers
    {
        static abstract void OnNewMessage(MqttApplicationMessageInterceptorContext context);
    }
    public class Handlers :IHandlers, IHostedService
    {
        private static IHubContext<HubHandler> _hubcontext;
        private static InfluxDBService _service;
        private static string[]? _topicId;

        

        public Handlers(IHubContext<HubHandler> hubcontext, InfluxDBService service)
        {
            _hubcontext = hubcontext;
            _service = service;
        }
        public async static void OnNewMessage(MqttApplicationMessageInterceptorContext context)
        {
            // Convert Payload to string
            var payload = context.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(context.ApplicationMessage?.Payload);

            Console.WriteLine(
                " TimeStamp: {0} -- Message: ClientId = {1}, Topic = {2}, Payload = {3}, QoS = {4}, Retain-Flag = {5}",

                DateTime.Now,
                context.ClientId,
                context.ApplicationMessage?.Topic,
                payload,
                context.ApplicationMessage?.QualityOfServiceLevel,
                context.ApplicationMessage?.Retain);
            // topic example sensor/humidity/abe26dc6-173f-42e1-8f27-58471912fd7f
            _topicId = context.ApplicationMessage?.Topic.Split("/");
            _service.Write(_topicId[1], "Id", _topicId[2], Convert.ToDouble(payload) );
            
            
            await _hubcontext.Clients.All.SendAsync("ReceiveMessage", "message");
            
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}