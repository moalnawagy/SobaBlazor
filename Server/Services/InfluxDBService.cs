using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;

namespace SobaBlazor.Server.Services;


    public class InfluxDBService
    {
        private readonly string _token;

        public InfluxDBService(IConfiguration configuration)
        {
            _token = configuration.GetValue<string>("InfluxDB:Token");
        }

        public async void Write( string mesurement,string tag,string tagValue, double record )
        {
            using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
            using var write = client.GetWriteApi();
            var point = PointData.Measurement(mesurement)
                .Tag(tag, tagValue)
                .Field("value", record)
                .Timestamp(DateTime.UtcNow, WritePrecision.Ns);
            write.WritePoint(point,bucket:"IOT", org:"Soba");
            var data = QueryAsync();
            Console.WriteLine(data);
            
            
            // action(write);
        }

        public async Task<List<FluxTable>> QueryAsync()
        {
            using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
            var query = await client.GetQueryApi().QueryAsync("from(bucket:\"IOT\") |> range(start: 0)", "my-org");;
            return query;
        }
    }
