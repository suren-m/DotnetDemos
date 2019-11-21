using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.ViewModels
{
    public class UKCounty
    {
        public string Abbreviation { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public override string ToString()
        {
            return $"{this.Name} - {this.Country}";
        }
    }
}
