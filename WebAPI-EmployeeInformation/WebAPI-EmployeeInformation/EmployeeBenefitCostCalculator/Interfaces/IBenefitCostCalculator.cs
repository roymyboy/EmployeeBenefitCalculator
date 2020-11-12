using EmployeeBenefitCoverage.Models;

namespace EmployeeBenefitCoverage.EmployeeBenefitCostCalculator.Interfaces
{
    public interface IBenefitCostCalculator
    {
        /// <summary>
        /// check if the employee qualifies for discount
        /// </summary>
        /// <param name="inEmployeeName"></param>
        /// <returns></returns>
        bool DoesQualifyForDiscount(string inEmployeeName);

        /// <summary>
        /// apply the discount give the cost of benefit per year and discount percentage
        /// </summary>
        /// <param name="percentageOfDiscount"></param>
        /// <param name="costOfBenefit"></param>
        /// <returns></returns>
        decimal ApplyDiscountPerPayCheck(decimal percentageOfDiscount, decimal costOfBenefit);

        /// <summary>
        /// Claculate the Employee cost benefit per enployee
        /// </summary>
        /// <param name="inEmployeeInformation"></param>
        /// <returns></returns>
        EmployeeSalaryInformation CalculateEmployeeSalary(EmployeeInformation inEmployeeInformation);
    }
}
