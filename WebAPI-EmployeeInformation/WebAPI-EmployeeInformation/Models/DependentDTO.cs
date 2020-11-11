using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefitCoverage.DataAdapter.DTO
{
    public class DependentDTO
    {
        public string DependentID { get; set; }
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }

        public DependentDTO() { }

        public DependentDTO(DataRow dr)
        {
            DependentID = !string.IsNullOrEmpty(dr["DependentID"].ToString()) ? Convert.ToString(dr["DependentID"]) : "";
            EmployeeID = !string.IsNullOrEmpty(dr["EmployeeID"].ToString()) ? Convert.ToString(dr["EmployeeID"]) : "";
            FirstName = !string.IsNullOrEmpty(dr["FirstName"].ToString()) ? Convert.ToString(dr["FirstName"]) : "";
            LastName = !string.IsNullOrEmpty(dr["LastName"].ToString()) ? Convert.ToString(dr["LastName"]) : "";
            Relationship = !string.IsNullOrEmpty(dr["Relationship"].ToString()) ? Convert.ToString(dr["Relationship"]) : "";
        }

    }
}
