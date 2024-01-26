using EmployeeCrud.Models.Models;

namespace EmployeeCrud.Models.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        IEnumerable<Employee> GetEmployeePagination(int PageNo, int RowsPerPage);
        Employee GetEmployeeByEmail(string email);
        bool EmployeeHasHobby(int employeeId, int hobbyId);
        int AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
        IEnumerable<Designation> GetDesignations();
        IEnumerable<Hobby> GetHobbys();
        IEnumerable<EmployeeHobby> GetEmployeesHobbys();
        Designation GetDesignationById(int id);
        Hobby GetHobbyById(int id);
    }
}
