using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace gRPCDemo.Models
{

    [DataContract]
    public class UKCountiesResult
    {
        [DataMember(Order = 1)]
        public IEnumerable<UKCountiesData> UKCounties { get; set; }
    }
}
