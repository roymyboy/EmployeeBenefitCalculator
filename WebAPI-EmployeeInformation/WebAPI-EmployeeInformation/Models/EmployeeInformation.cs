using EmployeeBenefitCoverage.DataAdapter.DTO;
using System.Collections.Generic;

namespace EmployeeBenefitCoverage.Models
{
    /// <summary>
    /// represents raw information about of an employee and its dependents
    /// </summary>
    public class EmployeeInformation
    {
        public EmployeeDTO Employee;
        public List<DependentDTO> Dependents;

        public EmployeeInformation()
        {
            Employee = new EmployeeDTO();
            Dependents = new List<DependentDTO>();
        }
    }
}
