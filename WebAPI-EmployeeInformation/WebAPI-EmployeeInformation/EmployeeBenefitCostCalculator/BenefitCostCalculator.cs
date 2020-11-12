using EmployeeBenefitCoverage.DataAdapter.DTO;
using EmployeeBenefitCoverage.EmployeeBenefitCostCalculator.Interfaces;
using EmployeeBenefitCoverage.Models;
using System;
using EmployeeBenefitCoverage.Utilities;

namespace EmployeeBenefitCoverage.EmployeeBenefitCostCalculator
{
    /// <summary>
    /// Calculates the cost of the benefit per employee and its dependents
    /// </summary>
    public class BenefitCostCalculator : IBenefitCostCalculator
    {
        public EmployeeSalaryInformation CalculateEmployeeSalary(EmployeeInformation inEmployeeInformation)
        {
            EmployeeSalaryInformation employeeSalaryInfo = CalculateEmployeeSalary(inEmployeeInformation.Employee);

            foreach (DependentDTO dependent in inEmployeeInformation.Dependents)
            {
                DependentBenefitCostInformation dependentsCostInfo = CalculateDependentBenefitCost(dependent);
                employeeSalaryInfo.SalaryAfterBenefitDeduction -= dependentsCostInfo.CostOfBenefit;
                employeeSalaryInfo.dependentsBenifitCostInfo.Add(dependentsCostInfo);
            }

            return employeeSalaryInfo;
        }

        /// <summary>
        /// Claculates the Employee cost benefit per enployee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private EmployeeSalaryInformation CalculateEmployeeSalary(EmployeeDTO employee)
        {
            EmployeeSalaryInformation inEmployeeInfo = new EmployeeSalaryInformation();

            inEmployeeInfo.EmployeeFullName = string.Concat(employee.FirstName, " ", employee.LastName);
            inEmployeeInfo.EmployeeCostOfBeneFit = Utility.RoundUpDecimal((decimal)(employee.CostOfBenefitAnnual / 26));
            inEmployeeInfo.SalaryBeforeBenefitDeduction = (decimal)(employee.AnnualSalary / 26);

            decimal EmployeSalaryAfterDeduction = inEmployeeInfo.SalaryBeforeBenefitDeduction;

            //Discount for Employee if qualifies
            if (DoesQualifyForDiscount(employee.FirstName))
            {
                inEmployeeInfo.EmployeeCostOfBeneFit = ApplyDiscountPerPayCheck((decimal)employee.PercentageOfDiscount, (decimal)employee.CostOfBenefitAnnual);
            }

            EmployeSalaryAfterDeduction -= inEmployeeInfo.EmployeeCostOfBeneFit;

            inEmployeeInfo.SalaryAfterBenefitDeduction = Utility.RoundUpDecimal(EmployeSalaryAfterDeduction);

            return inEmployeeInfo;
        }

        /// <summary>
        /// clalulates cost of benefit for each dependent
        /// </summary>
        /// <param name="dependent"></param>
        /// <returns></returns>
        private DependentBenefitCostInformation CalculateDependentBenefitCost(DependentDTO dependent)
        {
            DependentBenefitCostInformation dependentsInfo = new DependentBenefitCostInformation();

            dependentsInfo.FullName = string.Concat(dependent.FirstName + " " + dependent.LastName);
            dependentsInfo.CostOfBenefit = Utility.RoundUpDecimal((decimal)(dependent.CostOfBenefitAnnual / 26));

            if (DoesQualifyForDiscount(dependent.FirstName))
            {
                dependentsInfo.CostOfBenefit = ApplyDiscountPerPayCheck((decimal)dependent.PercentageOfDiscount, (decimal)dependent.CostOfBenefitAnnual);
            }

            return dependentsInfo;
        }

        /// <summary>
        /// checks if the employee qualifies for discount
        /// </summary>
        /// <param name="inEmployeeName"></param>
        /// <returns></returns>
        public bool DoesQualifyForDiscount(string inEmployeeName)
        {
            if (!string.IsNullOrEmpty(inEmployeeName) && inEmployeeName.ToLower().StartsWith("a"))
                return true;

            return false;
        }

        /// <summary>
        /// applies the discount give the cost of benefit per year and discount percentage
        /// </summary>
        /// <param name="percentageOfDiscount"></param>
        /// <param name="costOfBenefit"></param>
        /// <returns></returns>
        public decimal ApplyDiscountPerPayCheck(decimal percentageOfDiscount, decimal costOfBenefit)
        {
            return Math.Round((costOfBenefit - (costOfBenefit * (percentageOfDiscount / 100))) / 26, 2);
        }
    }
}

