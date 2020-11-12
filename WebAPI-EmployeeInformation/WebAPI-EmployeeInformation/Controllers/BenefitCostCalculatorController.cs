﻿using EmployeeBenefitCoverage.DataAdapter.Interfaces;
using EmployeeBenefitCoverage.EmployeeBenefitCostCalculator.Interfaces;
using EmployeeBenefitCoverage.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeBenefitCoverage.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BenefitCostCalculatorController : ControllerBase
    {
        private IEmployeeDataAdapter _employeAdapter;
        private IBenefitCostCalculator _benefitCostCalculator;

        public BenefitCostCalculatorController(IEmployeeDataAdapter employeeAdapter, IBenefitCostCalculator benefitCostCalculator)
        {
            _employeAdapter = employeeAdapter;
            _benefitCostCalculator = benefitCostCalculator;
        }

        /// <summary>
        /// Controller to calculate the employee salary give thier emloyeeID 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CalculateBenefitCost")]
        public ActionResult Get(string employeeID)
        {
            EmployeeInformation employeeRecord = _employeAdapter.GetByUniqueID(employeeID);

            if (string.IsNullOrEmpty(employeeRecord.Employee.EmployeeID))
            {
                return BadRequest(string.Format($"Given EmployeeID: {employeeID} doesnot exist. Please provide the right employeeID"));
            }

            EmployeeSalaryInformation _salary = _benefitCostCalculator.CalculateEmployeeSalary(employeeRecord);

            string result = JsonConvert.SerializeObject(_salary);

            return Ok(result);
        }
    }
}