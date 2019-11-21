using DemoWebapi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoWebapi.Services
{
    public class CarsService : IDataService<Car>
    {
        /// <summary>
        /// Cars!
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            try

            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Cars.json");

                var jsonData = await File.ReadAllTextAsync(filePath);

                var results = JsonSerializer.Deserialize<List<Car>>(
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
