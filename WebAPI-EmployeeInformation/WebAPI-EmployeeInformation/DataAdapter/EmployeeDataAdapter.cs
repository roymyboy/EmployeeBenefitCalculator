using AutoMapper;
using EmployeeBenefitCoverage.Collections;
using EmployeeBenefitCoverage.DataAdapter.DTO;
using EmployeeBenefitCoverage.DataAdapter.Interfaces;
using EmployeeBenefitCoverage.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace EmployeeBenefitCoverage.DataAdapter
{
    public class EmployeeDataAdapter : Interfaces.IEmployeeDataAdapter
    {
        /// <summary>
        /// Student db context
        /// </summary>
        private readonly DatabaseConnection _DB;

        /// <summary>
        /// Initialize instance for <see cref="StudentDataAdapeter"/>
        /// </summary>
        public EmployeeDataAdapter(DatabaseConnection inConnection)
        {
            _DB = inConnection ?? throw new ArgumentNullException(nameof(inConnection));
        }

        public List<EmployeeInformation> Get(string sql)
        {
            DataSet ds = _DB.Execute(sql);

            List<EmployeeInformation> listOfEmployee = EmployeeCollection.GetListOfEmployee(ds);

            return listOfEmployee;
        }

        public EmployeeInformation GetByUniqueID(string sql, string uniqueID)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UniqueId", uniqueID);

            DataSet ds = _DB.Execute(sql, CommandType.StoredProcedure, 600, param);

            List<EmployeeInformation> listOfEmployee = EmployeeCollection.GetListOfEmployee(ds);

            if (listOfEmployee.Count > 0)
                return listOfEmployee.First();

            return new EmployeeInformation();
        }

        public string Post(string sql, EmployeeInformation employee)
        {
            DataTable employeeDt = CreateEmployeeDataTable(employee);
            DataTable dependentDt = CreateDependentDataTable(employee.Dependents);

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter()
            { 
                ParameterName = "@EmployeeDataTableType",
                TypeName = "dbo.EmployeeTableType",
                Value = employeeDt,
                SqlDbType = SqlDbType.Structured
            };

            param[1] = new SqlParameter()
            {
                ParameterName = "@DependentsDataTableType",
                TypeName = "dbo.DependentsTableType",
                Value = dependentDt,
                SqlDbType = SqlDbType.Structured
            };

            _ = _DB.Execute(sql, CommandType.StoredProcedure, 600, param);

            return "successfully inserted data";
        }


        #region helper method
        private DataTable CreateEmployeeDataTable(EmployeeInformation inEmployeeInfo)
        {
            DataTable table = new DataTable()
            { 
                Columns =
                {
                     new DataColumn("EmployeeID", typeof(string)),
                     new DataColumn("FirstName", typeof(string)),
                     new DataColumn("LastName", typeof(string)),
                     new DataColumn("Phone", typeof(string)),
                     new DataColumn("Email", typeof(string)),
                     new DataColumn("AnnualSalary", typeof(decimal)),
                     new DataColumn("NumberOfDependents", typeof(int))
                }
            };

            DataRow dr = table.NewRow();

            dr["EmployeeID"] = inEmployeeInfo.Employee.EmployeeID;
            dr["FirstName"] = inEmployeeInfo.Employee.FirstName;
            dr["LastName"] = inEmployeeInfo.Employee.LastName;
            dr["Phone"] = inEmployeeInfo.Employee.Phone;
            dr["Email"] = inEmployeeInfo.Employee.Email;
            dr["AnnualSalary"] = inEmployeeInfo.Employee.AnnualSalary;
            dr["NumberOfDependents"] = inEmployeeInfo.Dependents.Count;
            table.Rows.Add(dr);

            return table;
        }

        private DataTable CreateDependentDataTable(List<DependentDTO> dependentInfo)
        {
            DataTable table = new DataTable()
            {
                Columns =
                {
                    new DataColumn("DependentID", typeof(string)),
                    new DataColumn("EmployeeID", typeof(string)),
                    new DataColumn("FirstName", typeof(string)),
                    new DataColumn("LastName", typeof(string)),
                    new DataColumn("Relationship", typeof(string))
                }
            };

            foreach (DependentDTO record in dependentInfo)
            {
                DataRow dr = table.NewRow();

                dr["DependentID"] = record.DependentID;
                dr["EmployeeID"] = record.EmployeeID;
                dr["FirstName"] = record.FirstName;
                dr["LastName"] = record.LastName;
                dr["Relationship"] = record.Relationship;

                table.Rows.Add(dr);
            }

            return table;
        }

        #endregion
    }
}
