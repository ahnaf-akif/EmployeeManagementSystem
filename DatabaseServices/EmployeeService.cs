using BusinessModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.DatabaseServices
{
    internal class EmployeeService: IEmployeeService
    {
        private Employee employee;
        private IConfiguration config;
        public EmployeeService(IConfiguration config)
        {
            this.employee = new Employee();
            this.config = config;
        }

        DataSet IEmployeeService.GetAllData()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter;
            using (SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction;
                SqlCommand cmd = sqlConnection.CreateCommand();
                sqlTransaction = sqlConnection.BeginTransaction("Database Transaction");
                cmd.Connection = sqlConnection;
                cmd.Transaction = sqlTransaction;
                try
                {
                    cmd.CommandText = "SELECT * FROM Employee";
                    adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds, "Employee");
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        sqlTransaction.Rollback();
                        throw new Exception(ex.Message, ex.InnerException);
                    }
                }
                return ds;
            }
        }

        Employee IEmployeeService.GetDataByID(int ID)
        {

            using (SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction;
                SqlCommand cmd = sqlConnection.CreateCommand();
                sqlTransaction = sqlConnection.BeginTransaction("Database Transaction");
                cmd.Connection = sqlConnection;
                cmd.Transaction = sqlTransaction;
                try
                {
                    cmd.CommandText = string.Format("SELECT * FROM Employee WHERE Id={0}", ID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        employee.ID = Convert.ToInt32(reader["Id"]);
                        employee.FirstName = reader["FirstName"].ToString();
                        employee.MiddleName = reader["MiddleName"].ToString();
                        employee.LastName = reader["LastName"].ToString();
                    }
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        sqlTransaction.Rollback();
                        throw new Exception(ex.Message, ex.InnerException);
                    }
                }
                return (Employee)employee;
            }
        }
        int IEmployeeService.Save(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("DevConnection")))
            {
                int maxID = 0;
                sqlConnection.Open();
                SqlTransaction sqlTransaction;
                SqlCommand cmd = sqlConnection.CreateCommand();
                sqlTransaction = sqlConnection.BeginTransaction("Database Transaction");
                cmd.Connection = sqlConnection;
                cmd.Transaction = sqlTransaction;
                try
                {
                    cmd.CommandText = string.Format("INSERT INTO Employee (FirstName, MiddleName, LastName) VALUES('{0}','{1}','{2}')", employee.FirstName,employee.MiddleName,employee.LastName);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = string.Format("SELECT MAX(Id) FROM Employee");
                    maxID = Convert.ToInt32(cmd.ExecuteScalar());
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        sqlTransaction.Rollback();
                        throw new Exception(ex.Message, ex.InnerException);
                    }
                }
                return maxID;
            }
        }

        int IEmployeeService.Update(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("DevConnection")))
            {
                int rowsAffected = 0;
                sqlConnection.Open();
                SqlTransaction sqlTransaction;
                SqlCommand cmd = sqlConnection.CreateCommand();
                sqlTransaction = sqlConnection.BeginTransaction("Database Transaction");
                cmd.Connection = sqlConnection;
                cmd.Transaction = sqlTransaction;
                try
                {
                    cmd.CommandText = string.Format("UPDATE Employee SET FirstName='{0}', MiddleName='{1}', LastName='{2}' WHERE Id={3}", employee.FirstName, employee.MiddleName, employee.LastName,employee.ID);
                    rowsAffected=cmd.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if(ex.InnerException!=null)
                    {
                        sqlTransaction.Rollback();
                        throw new Exception(ex.Message, ex.InnerException);
                    }
                }
                return rowsAffected;
            }
        }
        int IEmployeeService.Delete(int ID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("DevConnection")))
            {
                int rowsAffected = 0;
                sqlConnection.Open();
                SqlTransaction sqlTransaction;
                SqlCommand cmd = sqlConnection.CreateCommand();
                sqlTransaction = sqlConnection.BeginTransaction("Database Transaction");
                cmd.Connection = sqlConnection;
                cmd.Transaction = sqlTransaction;
                try
                {
                    cmd.CommandText = string.Format("DELETE Employee WHERE Id={0}", ID);
                    rowsAffected=cmd.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        sqlTransaction.Rollback();
                        throw new Exception(ex.Message,ex.InnerException);
                    }
                }
                return rowsAffected;
            }
        }
    }
}
