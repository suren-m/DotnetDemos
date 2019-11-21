using DemoApp.Utils;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Services
{
    public class RestClient : IRestClient
    {
         private static readonly HttpClient _client;

        static RestClient()
        {      
            _client = new HttpClient
            {
                Timeout = _httpTimeOut               
            };
        }

        private static readonly TimeSpan _httpTimeOut = new TimeSpan(0, 5, 0); // 5 mins max
      
        public RestClient()
        {
        }

        public async Task<TReturnMessage> GetAsync<TReturnMessage>(string resourceUri, CancellationToken ct, bool captureContext = false)
            where TReturnMessage : class, new()
        {
            try
            {
                TraceUtil.PrintThreadInfo("Entering GetAsync", Thread.CurrentThread);
                HttpResponseMessage response;

                var uri = new Uri(resourceUri);

                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //when .cancel() is called
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }

                response = await _client.GetAsync(uri, ct).ConfigureAwait(captureContext);
                
                if (!response.IsSuccessStatusCode)
                {
                    var ex = new HttpRequestException($"{response.StatusCode} -- {response.ReasonPhrase}");

                    ex.Data.Add("StatusCode", response.StatusCode);
                    throw ex;
                }

                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(captureContext);

                TraceUtil.PrintThreadInfo("Returning From GetAsync", Thread.CurrentThread);

                return JsonSerializer.Deserialize<TReturnMessage>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
