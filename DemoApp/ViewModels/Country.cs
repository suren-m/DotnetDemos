using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.ViewModels
{
    public class Country
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.Name} - {this.Code}";
        }
    }
}
