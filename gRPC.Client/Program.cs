using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using gRPCDemo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace gRPCDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await GetAllCounties();

            await GetCountiesStream();
        }

        static async Task GetAllCounties()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:15000");
            var client = new UKCounties.UKCountiesClient(channel);

            var reply = await client.GetUKCountiesAsync(new Empty());

            foreach (var county in reply.UKCountiesData)
            {
                Console.WriteLine($"{county.Name}, {county.Country}");
            }

            Console.WriteLine("\n....Done...\n");

        }

        static async Task GetCountiesStream()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:15000");
            var client = new UKCounties.UKCountiesClient(channel);

            var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));

            using var replies = client.GetUKCountiesStream(new Empty(), cancellationToken: cts.Token);

            try
            {
                await foreach (var countyData in replies.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                {
                    Console.WriteLine($"{countyData.Name}, {countyData.Country}");
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("\n..Stream cancelled from cancellation token..\n");
            }
                       
            Console.ReadKey();
        }

    }
    
}
