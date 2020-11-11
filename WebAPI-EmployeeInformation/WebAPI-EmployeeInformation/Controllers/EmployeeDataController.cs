using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeBenefitCoverage.Collections;
using EmployeeBenefitCoverage.DataAdapter.DTO;
using EmployeeBenefitCoverage.DataAdapter.Interfaces;
using EmployeeBenefitCoverage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace EmployeeBenefitCoverage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeDataController : ControllerBase
    {
        private IEmployeeDataAdapter _employeAdapter;

        public EmployeeDataController(IEmployeeDataAdapter employeeAdapter)
        {
            _employeAdapter = employeeAdapter;
        }

        [HttpPost]
        [Route("AddRecord")]
        [RequestSizeLimit(100_000_000)]
        public ActionResult Post([FromBody] object requestBody)
        {
            EmployeeInformation employeeRecord = Utilities.Utility.ParseJSONArray<EmployeeInformation>(requestBody.ToString());

            var res = _employeAdapter.Post("EmployeeInformation..InsEmployeeData", employeeRecord);
            
            return Ok(res);
        }

        [HttpGet]
        [Route("SingleRecord")]
        public ActionResult Get(string employeeID)
        {
            var employeeRecords = _employeAdapter.GetByUniqueID("EmployeeInformation..SelEmployeeDataByID", employeeID);

            string result = JsonConvert.SerializeObject(employeeRecords);

            return Ok(result);
        }

        [HttpGet]
        [Route("AllRecords")]
        public ActionResult Get()
        {
            List<EmployeeInformation> employeeRecords =  _employeAdapter.Get("EmployeeInformation..SelEmployeeData");

            string result = JsonConvert.SerializeObject(employeeRecords);

            return Ok(result);
        }
    }
}
