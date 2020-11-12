using System.Collections.Generic;
using EmployeeBenefitCoverage.DataAdapter.Interfaces;
using EmployeeBenefitCoverage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace EmployeeBenefitCoverage.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmployeeDataController : ControllerBase
    {
        private IEmployeeDataAdapter _employeAdapter;

        public EmployeeDataController(IEmployeeDataAdapter employeeAdapter)
        {
            _employeAdapter = employeeAdapter;
        }

        /// <summary>
        /// Controller to add employee record in the database
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddRecord")]
        [RequestSizeLimit(100_000_000)]
        public ActionResult Post([FromBody] object requestBody)
        {
            EmployeeInformation employeeRecord = Utilities.Utility.ParseJSONArray<EmployeeInformation>(requestBody.ToString());

            var res = _employeAdapter.Post(employeeRecord);

            string result = JsonConvert.SerializeObject(res);

            return Ok(result);
        }

        /// <summary>
        /// Controller to extract available  employee information by give employee ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SingleRecord")]
        public ActionResult Get(string employeeID)
        {
            EmployeeInformation employeeRecord = _employeAdapter.GetByUniqueID(employeeID);

            if (string.IsNullOrEmpty(employeeRecord.Employee.EmployeeID))
            {
                return BadRequest(string.Format($"Given EmployeeID: {employeeID} didnot return any employee data. Please provide a valid employee ID"));
            }

            string result = JsonConvert.SerializeObject(employeeRecord);
            
            return Ok(result);
        }

        /// <summary>
        /// Controller to extract available employee and its dependents from Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllRecords")]
        public ActionResult Get()
        {
            List<EmployeeInformation> employeeRecords =  _employeAdapter.Get();

            if (employeeRecords.Count == 0)
            {
                return BadRequest(string.Format($"There is currently no employee record in the database."));
            }

            string result = JsonConvert.SerializeObject(employeeRecords);
            
            return Ok(result);
        }
    }
}
