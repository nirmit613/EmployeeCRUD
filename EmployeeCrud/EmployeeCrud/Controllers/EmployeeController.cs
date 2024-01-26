using EmployeeCrud.Models.Models;
using EmployeeCrud.Services.DTO;
using EmployeeCrud.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCrud.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Methods
        [HttpGet("Employees")]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeService.GetEmployees());
        }
        [HttpGet("Designations")]
        public IActionResult GetDesignations()
        {
            return Ok(_employeeService.GetDesignations());
        }
        [HttpGet("Hobbies")]
        public IActionResult GetHobbies()
        {
            return Ok(_employeeService.GetHobbies());
        }
        [HttpGet("EmpHobby")]
        public IActionResult GetEmpHobbies()
        {
            return Ok(_employeeService.GetEmployeeHobbies());
        }

        [HttpGet("Paginated")]
        public IActionResult GetEmployeePagination(int PageNo, int RowsPerPage)
        {
            return Ok(_employeeService.GetEmployeePagination(PageNo, RowsPerPage));
        }
        [HttpGet("id")]
        public IActionResult GetEmployeeById(int id)
        {
            return Ok(_employeeService.GetEmployeeById(id));
        }
        [HttpGet("DesignationId")]
        public IActionResult GetDesignationById(int id)
        {
            return Ok(_employeeService.GetDesignationById(id));
        }
        [HttpGet("HobbyId")]
        public IActionResult GetHobbyById(int id)
        {
            return Ok(_employeeService.GetHobbyById(id));
        }

        [HttpPost("Employee")]
        public IActionResult AddEmployee(AddEmployeeDTO employee)
        {
         
            return Ok(_employeeService.AddEmployee(employee));
        }

        [HttpPut]
        public IActionResult Put(UpdateEmployeeDTO employee)
        {
            return Ok(_employeeService.UpdateEmployee(employee));
        }

        [HttpDelete("id")]
        public IActionResult DeleteEmployee(int id)
        {

            return Ok(_employeeService.DeleteEmployee(id));
        }
        #endregion

    }
}
