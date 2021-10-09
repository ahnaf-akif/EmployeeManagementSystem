using BusinessModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.DatabaseServices
{
    public interface IEmployeeService
    {
        public DataSet GetAllData();
        public Employee GetDataByID(int ID);
        public int Save(Employee employee);
        public int Update(Employee employee);
        public int Delete(int ID);
    }
}
