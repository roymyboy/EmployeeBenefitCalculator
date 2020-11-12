using System.Collections.Generic;

namespace EmployeeBenefitCoverage.Models
{
    /// <summary>
    /// Represents employee salary information with benefit cost of each deppendents
    /// </summary>
    public class EmployeeSalaryInformation
    {
        public string EmployeeFullName { get; set; }
        public decimal EmployeeCostOfBeneFit { get; set; }
        public decimal SalaryAfterBenefitDeduction { get; set; }
        public decimal SalaryBeforeBenefitDeduction { get; set; }

        public List<DependentBenefitCostInformation> dependentsBenifitCostInfo;

        public EmployeeSalaryInformation()
        {
            dependentsBenifitCostInfo = new List<DependentBenefitCostInformation>();
        }
    }
}
