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
    public class CountriesService : IDataService<Country>
    {
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            try

            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Countries.json");

                var jsonData = await File.ReadAllTextAsync(filePath);

                //using System.Text.Json; - No need for NewtonSoft.Json
                var results = JsonSerializer.Deserialize<List<Country>>(
                        jsonData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
