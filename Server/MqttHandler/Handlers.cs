using System;
using System.Text;
using MQTTnet.Server;

namespace mqttASP
{
    public class Handlers
    {
        public static void OnNewMessage(MqttApplicationMessageInterceptorContext context)
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
            

        }
        
    }
}