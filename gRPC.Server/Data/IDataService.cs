using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gRPCDemo
{
    public interface IDataService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
