using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApp.Services
{
    public interface IRestClient
    {
        Task<TReturnMessage> GetAsync<TReturnMessage>(string resourceUri, CancellationToken ct, bool captureContext)
            where TReturnMessage : class, new();
    }
}
