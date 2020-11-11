using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace EmployeeBenefitCoverage.DataAdapter
{
    public class DatabaseConnection : IDisposable
    {
        public SqlConnection _connection { get; }
        /// <summary>
        /// Initialize instance for <see cref="DatabaseConnection"/>
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public DatabaseConnection(string inConnectionString)
        {
            _connection = new SqlConnection(inConnectionString);
        }

        public DataSet Execute(string inSql,CommandType type = CommandType.StoredProcedure, int Timeout = 600, params SqlParameter[] sqlParams)
        {
            int retryCount = 0;
            DataSet resultSet;
            try
            {
                SqlConnection conn = _connection;
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    bool isConnOpen;
                    do
                    {
                        conn.Open();
                        if (conn.State == ConnectionState.Open)
                        {
                            isConnOpen = true;
                        }
                        else
                        {
                            isConnOpen = false;
                            retryCount++;
                        }
                    } while (isConnOpen == false && retryCount < 3);

                    if (retryCount > 3 && isConnOpen == false)
                    {
                        throw new Exception("Unable to open database connection. Aborting the process");
                    }
                }

                SqlCommand cmd = new SqlCommand(inSql, conn);
                cmd.CommandType = type;
                cmd.Parameters.AddRange(sqlParams);
                cmd.CommandTimeout = Timeout;

                using SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                resultSet = new DataSet();
                adapter.Fill(resultSet);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            { 
                throw;
            }

            return resultSet;
        }

        public void Dispose() => _connection.Dispose();
    }
}
