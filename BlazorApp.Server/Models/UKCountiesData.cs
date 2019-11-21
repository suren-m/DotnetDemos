using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BlazorApp.Models
{
    [DataContract]
    public class UKCountiesData
    {
        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Abbreviation { get; set; }

        [DataMember(Order = 3)]
        public string Country { get; set; }
    }
}
