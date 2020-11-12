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

        /// <summary>
        /// generic execute method to interact with database
        /// </summary>
        /// <param name="inSql"></param>
        /// <param name="type"></param>
        /// <param name="Timeout"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
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

                    //retry 3 times to connect to database 
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

            Dispose();

            return resultSet;
        }

        /// <summary>
        /// dispose connection
        /// </summary>
        public void Dispose() => _connection.Dispose();
    }
}
