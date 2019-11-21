using DemoWebapi.Models;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoWebapi.Services
{
    public class UKCountiesService : IDataService<UKCounty>
    {
        public async Task<IEnumerable<UKCounty>> GetAllAsync()
        {
            try

            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "UkCounties.json");

                var jsonData = await File.ReadAllTextAsync(filePath);

                var results = JsonSerializer.Deserialize<List<UKCounty>>(
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
