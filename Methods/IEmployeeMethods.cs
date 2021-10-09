using BusinessModels;
using System.Collections.Generic;
using System.Data;

namespace EmployeeManagementAPI.Methods
{
    public interface IEmployeeMethods
    {
        public int Delete(int ID);
        public IList<Employee> GetAllData();
        public Employee GetDataByID(int id);
        public int Save(Employee employee);
        public int Update(Employee employee);

    }
}