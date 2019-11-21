using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;


namespace gRPCDemo
{
    public class UKCountiesService : UKCounties.UKCountiesBase
    {
        private readonly ILogger<UKCountiesService> _logger;
        private readonly IDataService<UKCountiesData> _ukCountiesDataService;

        public UKCountiesService(ILogger<UKCountiesService> logger, IDataService<UKCountiesData> ukCountiesDataService)
        {
            _logger = logger;
            _ukCountiesDataService = ukCountiesDataService;
        }

        public override async Task<UKCountiesReply> GetUKCounties(Empty _, ServerCallContext context)
        {

            var response = await _ukCountiesDataService.GetAllAsync();

            await Task.Delay(2000); // some work

            return new UKCountiesReply
            {
                UKCountiesData = { response }
            };
        }

        public override async Task GetUKCountiesStream(
            Empty _,
            IServerStreamWriter<UKCountiesData> responseStream,
            ServerCallContext context)
        {
           
            var data = await _ukCountiesDataService.GetAllAsync();

            foreach(var item in data)
            {
                var county = new UKCountiesData
                {
                    Name = item.Name,
                    Abbreviation = item.Abbreviation,
                    Country = item.Country
                };

                _logger.LogInformation("Sending UKCounty response");

                await responseStream.WriteAsync(county);

                await Task.Delay(2000); 
         

                if (context.CancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("The client cancelled their request");
                }
            }            
        }


    }
}
