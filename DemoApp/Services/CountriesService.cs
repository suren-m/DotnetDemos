
using DemoApp.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Services
{
    public class CountriesService
    {
        public static async Task<IEnumerable<Country>> GetCountriesAsync(string url, CancellationToken ct, bool captureContext = false)
        {
            var _client = new RestClient();

            var data = await _client.GetAsync<List<Country>>(url, ct, captureContext).ConfigureAwait(captureContext); 

            return data;
        }
    }
}
