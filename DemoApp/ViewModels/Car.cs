using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.ViewModels
{
    public class Car
    {
        public string Brand { get; set; }

        public IEnumerable<string> Models { get; set; }

        public override string ToString()
        {
            return $"{this.Brand}";
        }
    }
}
