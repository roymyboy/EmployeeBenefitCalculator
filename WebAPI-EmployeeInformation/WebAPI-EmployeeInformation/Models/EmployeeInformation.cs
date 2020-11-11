using EmployeeBenefitCoverage.DataAdapter.DTO;
using System.Collections.Generic;

namespace EmployeeBenefitCoverage.Models
{
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
