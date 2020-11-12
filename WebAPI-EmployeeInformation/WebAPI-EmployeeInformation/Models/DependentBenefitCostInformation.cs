using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefitCoverage.Models
{
    /// <summary>
    /// represents dependents benefit cost information
    /// </summary>
    public class DependentBenefitCostInformation
    {
        public string FullName { get; set; }
        public decimal CostOfBenefit { get; set; }

        public DependentBenefitCostInformation()
        { }
    }
}
