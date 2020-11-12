using EmployeeBenefitCoverage.Controllers;
using EmployeeBenefitCoverage.DataAdapter.Interfaces;
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
using System.Text;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EmployeeInformationUnitTests.DataAdapterTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DataAdapterTests
    {
         Mock<IEmployeeDataAdapter> _employeeData;
        string jsonStr;
         public DataAdapterTests()
         {
            _employeeData = new Mock<IEmployeeDataAdapter>();
            jsonStr = ReadJsonFromFile();
         }


        [Test]
        public void GetByUniqueID_ReturnsEmployeeInformationObject()
        {
            //Arrange

            EmployeeInformation ExcpectedResult = Utility.ParseJSONArray<EmployeeInformation>(jsonStr);

            _employeeData.Setup(x => x.GetByUniqueID("2AC72CB1-2BE1-4E79-BBCB-5763ECB810CB")).Returns(ExcpectedResult);

            var controller = new EmployeeDataController(_employeeData.Object);

            //Act
            var Result = (OkObjectResult)controller.Get("2AC72CB1-2BE1-4E79-BBCB-5763ECB810CB");
            EmployeeInformation ActualResult = Utility.ParseJSONArray<EmployeeInformation>(Result.Value.ToString());

            //Assert
            Assert.AreEqual(ExcpectedResult.Employee.EmployeeID, ActualResult.Employee.EmployeeID);
            Assert.AreEqual(ExcpectedResult.Employee.FirstName, ActualResult.Employee.FirstName);
            Assert.AreEqual(ExcpectedResult.Employee.NumberOfDependents, ActualResult.Employee.NumberOfDependents);
        }

        [Test]
        public void Get_ReturnsAllEmployeeInformationObject()
        {
            //Arrange

            List<EmployeeInformation> listOdExpectedResult = new List<EmployeeInformation>();

            EmployeeInformation ExcpectedResult = Utility.ParseJSONArray<EmployeeInformation>(ReadJsonFromFile());

            listOdExpectedResult.Add(ExcpectedResult);

            _employeeData.Setup(x => x.Get()).Returns(listOdExpectedResult);

            var controller = new EmployeeDataController(_employeeData.Object);

            //Act
            var controllerResult = (OkObjectResult)controller.Get();

            List<EmployeeInformation> ActualResult = Utility.ParseJSONArray<List<EmployeeInformation>>(controllerResult.Value.ToString());


            //Assert
            Assert.AreEqual(listOdExpectedResult.Count, ActualResult.Count);
        }

        [Test]
        public void Post_AddEmployeeInformationObject()
        {
            //Arrange

            List<EmployeeInformation> listOdExpectedResult = new List<EmployeeInformation>();

            EmployeeInformation ExcpectedResult = Utility.ParseJSONArray<EmployeeInformation>(ReadJsonFromFile());

            listOdExpectedResult.Add(ExcpectedResult);

            _employeeData.Setup(x => x.Post(ExcpectedResult)).Returns("successfully inserted data");

            var controller = new EmployeeDataController(_employeeData.Object);

            //Act
            var ExpectedResult = (OkObjectResult)controller.Post(ReadJsonFromFile());

            //Assert
            Assert.AreEqual(200, ExpectedResult.StatusCode);
        }

        private string ReadJsonFromFile()
        {
            string json = string.Empty;

            using (StreamReader r = new StreamReader("./SampleDataType.json"))
            {
                json = r.ReadToEnd();
            }

            return json;
        }
    }
}
