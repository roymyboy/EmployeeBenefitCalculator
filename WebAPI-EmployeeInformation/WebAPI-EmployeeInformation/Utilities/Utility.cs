using Newtonsoft.Json;
using System;

namespace EmployeeBenefitCoverage.Utilities
{
    public class Utility
    {
        /// <summary>
        /// parse give jason string to an object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inJson"></param>
        /// <returns></returns>
        public static T ParseJSONArray<T>(string inJson)
        {
            return JsonConvert.DeserializeObject<T>(inJson);
        }

        /// <summary>
        /// rounds decimal to 2 decimal place
        /// </summary>
        /// <param name="inValue"></param>
        /// <returns></returns>
        public static decimal RoundUpDecimal(decimal inValue)
        {
            return Math.Round(inValue, 2);
        }

        public static T ParseJSONArray<T>()
        {
            throw new NotImplementedException();
        }
    }
}
