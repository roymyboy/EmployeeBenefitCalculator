using System;
using System.Data;

namespace EmployeeBenefitCoverage.DataAdapter.DTO
{
    /// <summary>
    /// representws Dependents raw information
    /// </summary>
    public class DependentDTO
    {
        public string DependentID { get; set; }
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public decimal? PercentageOfDiscount { get; set; }
        public decimal? CostOfBenefitAnnual { get; set; }

        public DependentDTO() { }

        public DependentDTO(DataRow dr)
        {
            DependentID = !string.IsNullOrEmpty(dr["DependentID"].ToString()) ? Convert.ToString(dr["DependentID"]) : "";
            EmployeeID = !string.IsNullOrEmpty(dr["EmployeeID"].ToString()) ? Convert.ToString(dr["EmployeeID"]) : "";
            FirstName = !string.IsNullOrEmpty(dr["FirstName"].ToString()) ? Convert.ToString(dr["FirstName"]) : "";
            LastName = !string.IsNullOrEmpty(dr["LastName"].ToString()) ? Convert.ToString(dr["LastName"]) : "";
            Relationship = !string.IsNullOrEmpty(dr["Relationship"].ToString()) ? Convert.ToString(dr["Relationship"]) : "";
            PercentageOfDiscount = !string.IsNullOrEmpty(dr["PercentageOfDiscount"].ToString()) ? Convert.ToDecimal(dr["PercentageOfDiscount"]) : 0;
            CostOfBenefitAnnual = !string.IsNullOrEmpty(dr["CostOfBenefitAnnual"].ToString()) ? Convert.ToDecimal(dr["CostOfBenefitAnnual"]) : 0;
        }
    }
}
