using EmployeeBenefitCoverage.BuildEmployeeAndDependentRelation;
using EmployeeBenefitCoverage.DataAdapter.DTO;
using EmployeeBenefitCoverage.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        /// <summary>
        /// Get all the employee and its dependent information available in Database
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<EmployeeInformation> Get()
        {
            string sql = "EmployeeInformation..SelEmployeeData";
            DataSet ds = _DB.Execute(sql);

            List<EmployeeInformation> listOfEmployee = BuildEmployeeInformation.GetListOfEmployee(ds);

            return listOfEmployee;
        }

        /// <summary>
        /// get an employee and its deppendent information give employee's unique ID
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="uniqueID"></param>
        /// <returns></returns>
        public EmployeeInformation GetByUniqueID(string uniqueID)
        {
            string sql = "EmployeeInformation..SelEmployeeDataByID";
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@UniqueId", uniqueID);

            DataSet ds = _DB.Execute(sql, CommandType.StoredProcedure, 600, param);

            List<EmployeeInformation> listOfEmployee = new List<EmployeeInformation>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                listOfEmployee = BuildEmployeeInformation.GetListOfEmployee(ds);
            }

            if (listOfEmployee.Count > 0)
                return listOfEmployee.First();

            return new EmployeeInformation();
        }

        /// <summary>
        /// create employees information in database
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public string Post(EmployeeInformation employee)
        {
            string sql = "EmployeeInformation..InsEmployeeData";

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
                     new DataColumn("CostOfBenefitAnnual", typeof(decimal)),
                     new DataColumn("PercentageOfDiscount", typeof(decimal)),
                     new DataColumn("NumberOfDependents", typeof(int))
                }
            };

            DataRow dr = table.NewRow();

            dr["EmployeeID"] = inEmployeeInfo.Employee.EmployeeID;
            dr["FirstName"] = inEmployeeInfo.Employee.FirstName;
            dr["LastName"] = inEmployeeInfo.Employee.LastName;
            dr["Phone"] = inEmployeeInfo.Employee.Phone;
            dr["Email"] = inEmployeeInfo.Employee.Email;
            dr["AnnualSalary"] = (inEmployeeInfo.Employee.AnnualSalary is 0) ? 52000 : inEmployeeInfo.Employee.AnnualSalary;
            dr["NumberOfDependents"] = inEmployeeInfo.Dependents.Count;


            if (inEmployeeInfo.Employee.CostOfBenefitAnnual == null || inEmployeeInfo.Employee.CostOfBenefitAnnual  == 0)
            {
                dr["CostOfBenefitAnnual"] = 1000;
            }
            else
            {
                dr["CostOfBenefitAnnual"] = inEmployeeInfo.Employee.CostOfBenefitAnnual;
            }

            if (inEmployeeInfo.Employee.PercentageOfDiscount == null || inEmployeeInfo.Employee.PercentageOfDiscount  == 0)
            {
                dr["PercentageOfDiscount"] = 10;
            }
            else
            {
                dr["PercentageOfDiscount"] = inEmployeeInfo.Employee.PercentageOfDiscount;
            }

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
                    new DataColumn("Relationship", typeof(string)),
                    new DataColumn("PercentageOfDiscount", typeof(decimal)),
                    new DataColumn("CostOfBenefitAnnual", typeof(decimal))
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

                if (record.PercentageOfDiscount == null || record.PercentageOfDiscount == 0)
                {
                    dr["PercentageOfDiscount"] = 10;
                }
                else
                {
                    dr["PercentageOfDiscount"] = record.PercentageOfDiscount;
                }

                if (record.CostOfBenefitAnnual == null || record.CostOfBenefitAnnual == 0)
                {
                    dr["CostOfBenefitAnnual"] = 500;
                }
                else
                {
                    dr["CostOfBenefitAnnual"] = record.CostOfBenefitAnnual;
                }
             
                table.Rows.Add(dr);
            }

            return table;
        }

        #endregion
    }
}
