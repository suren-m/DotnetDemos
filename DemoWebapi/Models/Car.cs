using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebapi.Models
{
    public class Car
    {
        public string Brand { get; set; }

        public IEnumerable<string> Models { get; set; }
    }
}
