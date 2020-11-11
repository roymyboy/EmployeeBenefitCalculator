using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using EmployeeBenefitCoverage.Collections;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace EmployeeBenefitCoverage.Utilities
{
    public class Utility
    {
        public static T ParseJSONArray<T>(string inJson)
        {
            return JsonConvert.DeserializeObject<T>(inJson);
        } 
    }
}
