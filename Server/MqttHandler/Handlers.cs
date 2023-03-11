using System.Text;
using Microsoft.AspNetCore.SignalR;
using MQTTnet.Server;

namespace SobaBlazor.Server.MqttHandler
{
    public interface IHandlers
    {
        static abstract void OnNewMessage(MqttApplicationMessageInterceptorContext context);


    }
    public class Handlers :IHandlers, IHostedService
    {
        private static IHubContext<HubHandler> _hubcontext;

        public Handlers(IHubContext<HubHandler> hubcontext)
        {
            _hubcontext = hubcontext;
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