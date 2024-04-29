using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCrud.Models;

namespace EmployeeCrud.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployee();
        void InsertEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
         Employee GetOneEmployee(int id);
        void DeleteEmployee(Employee employee);
        // List<Designation> GetDesignations();
        void UpdateEmployeePayroll(int employeeId);

    }
}