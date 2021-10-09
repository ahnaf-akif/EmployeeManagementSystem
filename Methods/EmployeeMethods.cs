using BusinessModels;
using EmployeeManagementAPI.DatabaseServices; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.Methods
{
    public class EmployeeMethods : IEmployeeMethods
    {
        private IEmployeeService employeeService;
        public EmployeeMethods(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        IList<Employee> IEmployeeMethods.GetAllData()
        {
            try
            {
                IList<Employee> employeeList = new List<Employee>();
                DataSet ds = employeeService.GetAllData();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Employee em = new Employee();
                    em.ID = Convert.ToInt32(dr["Id"]);
                    em.FirstName = dr["FirstName"].ToString();
                    em.MiddleName = dr["MiddleName"].ToString();
                    em.LastName = dr["LastName"].ToString();
                    employeeList.Add(em);
                }

                return employeeList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        Employee IEmployeeMethods.GetDataByID(int id)
        {
            try
            {
                return employeeService.GetDataByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }


        }
        int IEmployeeMethods.Save(Employee employee)
        {
            try
            {
                return employeeService.Save(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        int IEmployeeMethods.Update(Employee employee)
        {
            try
            {
                return employeeService.Update(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        int IEmployeeMethods.Delete(int ID)
        {
            try
            {
                return employeeService.Delete(ID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);

            }

        }
    }
}
