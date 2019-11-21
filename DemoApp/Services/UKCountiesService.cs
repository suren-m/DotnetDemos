using DemoApp.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Services
{
    public class UKCountiesService
    {
        public static async Task<IEnumerable<UKCounty>> GetCountiesAsync(string url, CancellationToken ct, bool captureContext = false)
        {
            var _client = new RestClient();

            var data = await _client.GetAsync<List<UKCounty>>(url, ct, captureContext).ConfigureAwait(captureContext);

            return data;
        }
    }
}
