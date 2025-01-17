﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EmployeeBenefitCoverage.DataAdapter.DTO
{
    /// <summary>
    /// represents Employee raw information
    /// </summary>
    public class EmployeeDTO
    {
        [Key]
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal? AnnualSalary { get; set; }
        public decimal? CostOfBenefitAnnual { get; set; }
        public decimal? PercentageOfDiscount { get; set; }
        public int NumberOfDependents { get; set; }

        public EmployeeDTO() { }
        public EmployeeDTO(DataRow dr)
        {
            Fill(dr);
        }

        public void Fill(DataRow dr)
        {
            EmployeeID = !string.IsNullOrEmpty(dr["EmployeeID"].ToString()) ? Convert.ToString(dr["EmployeeID"]) : "";
            FirstName = !string.IsNullOrEmpty(dr["FirstName"].ToString()) ? Convert.ToString(dr["FirstName"]) : "";
            LastName = !string.IsNullOrEmpty(dr["LastName"].ToString()) ? Convert.ToString(dr["LastName"]) : "";
            Phone = !string.IsNullOrEmpty(dr["Phone"].ToString()) ? Convert.ToString(dr["Phone"]) : "";
            Email = !string.IsNullOrEmpty(dr["Email"].ToString()) ? Convert.ToString(dr["Email"]) : "";
            AnnualSalary = !string.IsNullOrEmpty(dr["AnnualSalary"].ToString()) ? Convert.ToDecimal(dr["AnnualSalary"]) : 0;
            NumberOfDependents = !string.IsNullOrEmpty(dr["NumberOfDependents"].ToString()) ? Convert.ToInt32(dr["NumberOfDependents"]) : 0;
            PercentageOfDiscount = !string.IsNullOrEmpty(dr["PercentageOfDiscount"].ToString()) ? Convert.ToDecimal(dr["PercentageOfDiscount"]) : 0;
            CostOfBenefitAnnual = !string.IsNullOrEmpty(dr["CostOfBenefitAnnual"].ToString()) ? Convert.ToDecimal(dr["CostOfBenefitAnnual"]) : 0 ;
        }
    }
}
