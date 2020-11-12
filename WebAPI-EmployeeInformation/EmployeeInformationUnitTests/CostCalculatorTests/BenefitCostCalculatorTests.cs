using EmployeeBenefitCoverage.Controllers;
using EmployeeBenefitCoverage.DataAdapter.Interfaces;
using EmployeeBenefitCoverage.EmployeeBenefitCostCalculator;
using EmployeeBenefitCoverage.EmployeeBenefitCostCalculator.Interfaces;
using EmployeeBenefitCoverage.Models;
using EmployeeBenefitCoverage.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Assert = NUnit.Framework.Assert;

namespace EmployeeInformationUnitTests.CostCalculatorTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BenefitCostCalculatorTests
    {
        [Test]
        public void CalculateCostOfBenefits_ReturnCostInformations()
        {
            //Arrange
            var _benefitCostCalculator = new Mock<IBenefitCostCalculator>() { CallBase = true};
            var _employeeAdapter = new Mock<IEmployeeDataAdapter>();

            EmployeeInformation information = Utility.ParseJSONArray<EmployeeInformation>(ReadJsonFromFile("./SampleDataType.json"));
            EmployeeSalaryInformation _employeeSalary = Utility.ParseJSONArray<EmployeeSalaryInformation>(ReadJsonFromFile("./SampleResult.json"));

            _employeeAdapter.Setup(x => x.GetByUniqueID("2AC72CB1-2BE1-4E79-BBCB-5763ECB810CB")).Returns(information);
            _benefitCostCalculator.Setup(y => y.CalculateEmployeeSalary(information)).Returns(_employeeSalary);

            var controller = new BenefitCostCalculatorController(_employeeAdapter.Object, _benefitCostCalculator.Object);

            //act
            var ExppectedResult = (OkObjectResult)controller.Get("2AC72CB1-2BE1-4E79-BBCB-5763ECB810CB");
            EmployeeSalaryInformation ExpEmployeeSalary = Utility.ParseJSONArray<EmployeeSalaryInformation>(ExppectedResult.Value.ToString());

            //Assert
            Assert.AreEqual(_employeeSalary.EmployeeFullName, ExpEmployeeSalary.EmployeeFullName);
            Assert.AreEqual(_employeeSalary.SalaryAfterBenefitDeduction, ExpEmployeeSalary.SalaryAfterBenefitDeduction);

        }

        private string ReadJsonFromFile(string path)
        {
            string json = string.Empty;

            using (StreamReader r = new StreamReader(path))
            {
                json = r.ReadToEnd();
            }

            return json;
        }
    }
}
