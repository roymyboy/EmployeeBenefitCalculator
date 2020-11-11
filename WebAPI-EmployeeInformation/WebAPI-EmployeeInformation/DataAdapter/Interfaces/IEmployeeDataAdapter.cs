using EmployeeBenefitCoverage.DataAdapter.DTO;
using EmployeeBenefitCoverage.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefitCoverage.DataAdapter.Interfaces
{
    public interface IEmployeeDataAdapter
    {
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        List<EmployeeInformation> Get(string inSQL);

        ///// <summary>
        ///// Get employee by id
        ///// </summary>
        ///// <returns></returns>
        EmployeeInformation GetByUniqueID(string inSQL, string uniqueID);

        ///// <summary>
        ///// Add new entity
        ///// </summary>
        ///// <returns></returns>
        string Post(string inSQL, EmployeeInformation newRecord);
    }
}
