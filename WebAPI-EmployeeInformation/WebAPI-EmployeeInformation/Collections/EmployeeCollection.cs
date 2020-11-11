using EmployeeBenefitCoverage.DataAdapter.DTO;
using EmployeeBenefitCoverage.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefitCoverage.Collections
{
    public class EmployeeCollection
    {
        public static List<EmployeeInformation> GetListOfEmployee(DataSet inEmployeeInfo)
        {
            List<EmployeeInformation> lstOfEmployee = new List<EmployeeInformation>();
            List<EmployeeDTO> employee = new List<EmployeeDTO>();
            List<DependentDTO> dependent = new List<DependentDTO>();

            foreach (DataRow row in inEmployeeInfo.Tables[0].Rows)
            {
                EmployeeDTO record = new EmployeeDTO(row);
                employee.Add(record);
            }

            foreach (DataRow row in inEmployeeInfo.Tables[1].Rows)
            {
                DependentDTO record = new DependentDTO(row);
                dependent.Add(record);
            }

            foreach (EmployeeDTO emp in employee)
            {
                EmployeeInformation employeeInfo = new EmployeeInformation();
                employeeInfo.Employee = emp;

                if (dependent.Exists(x => x.EmployeeID == emp.EmployeeID))
                {
                    List<DependentDTO> dependentPerEmployee = dependent.FindAll(x => x.EmployeeID == emp.EmployeeID);
                    employeeInfo.Dependents = dependentPerEmployee;
                }

                lstOfEmployee.Add(employeeInfo);
            }

            return lstOfEmployee;
        }
    }
}
