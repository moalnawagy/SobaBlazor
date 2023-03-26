using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using SobaBlazor.Server.Services;

namespace app.Invocables;
public class WriteRandomPlaneAltitudeInvocable : IInvocable
    {
        private readonly InfluxDBService _service;
        private static readonly Random _random = new Random();

        public WriteRandomPlaneAltitudeInvocable(InfluxDBService service)
        {
            _service = service;
        }

        public Task Invoke()
        {
            // _service.Write(write =>
            // {
            //     var point = PointData.Measurement("altitude")
            //         .Tag("plane", "test-plane")
            //         .Field("value", _random.Next(1000, 5000))
            //         .Timestamp(DateTime.UtcNow, WritePrecision.Ns);
            //     write.WritePoint(point,bucket:"IOTDevice", org:"Soba");
            // });

            return Task.CompletedTask;
        }
    }
