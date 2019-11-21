using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using gRPCDemo.Models;

namespace gRPCDemo
{
    public class UKCountiesDataService : IDataService<UKCountiesData>
    {
        public async Task<IEnumerable<UKCountiesData>> GetAllAsync()
        {
            try

            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "UkCounties.json");

                var jsonData = await File.ReadAllTextAsync(filePath);

                var results = JsonSerializer.Deserialize<List<UKCountiesData>>(
                        jsonData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return results;
            }
            catch (Exception ex) {
                throw ex;
            }
            
        }     
    }
}
