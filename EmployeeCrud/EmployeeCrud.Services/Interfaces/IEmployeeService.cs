using EmployeeCrud.Models.Models;
using EmployeeCrud.Services.DTO;

namespace EmployeeCrud.Services.Interfaces
{
    public interface IEmployeeService
    {
        ResponseDTO GetEmployees();
        ResponseDTO GetEmployeeById(int id);
        ResponseDTO GetEmployeePagination(int PageNo, int RowsPerPage);
        ResponseDTO GetEmployeeByEmail(string email);
        ResponseDTO AddEmployee(AddEmployeeDTO employee);
        ResponseDTO UpdateEmployee(UpdateEmployeeDTO employee);
        ResponseDTO DeleteEmployee(int id);
        ResponseDTO GetDesignations();
        ResponseDTO GetHobbies();
        ResponseDTO GetEmployeeHobbies();
        ResponseDTO GetDesignationById(int id);
        ResponseDTO GetHobbyById(int id);
    }
}
