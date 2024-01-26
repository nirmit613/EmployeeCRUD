using AutoMapper;
using EmployeeCrud.Models.Interfaces;
using EmployeeCrud.Models.Models;
using EmployeeCrud.Services.DTO;
using EmployeeCrud.Services.Interfaces;

namespace EmployeeCrud.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructors
        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Methods
        public ResponseDTO GetEmployees()
        {
            var response = new ResponseDTO();
            try
            {
                var data = _mapper.Map<List<Employee>>(_employeeRepository.GetEmployees().ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO GetDesignations()
        {
            var response = new ResponseDTO();
            try
            {
                var data = _mapper.Map<List<Designation>>(_employeeRepository.GetDesignations().ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO GetHobbies()
        {
            var response = new ResponseDTO();
            try
            {
                var data = _mapper.Map<List<Hobby>>(_employeeRepository.GetHobbys().ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetEmployeeHobbies()
        {
            var response = new ResponseDTO();
            try
            {
                var data = _mapper.Map<List<EmployeeHobby>>(_employeeRepository.GetEmployeesHobbys().ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server error";
                response.Error = ex.Message;
            }
            return response;

        }

        public ResponseDTO GetEmployeeById(int id) {
            var response = new ResponseDTO();
            try
            {
                var employee = _employeeRepository.GetEmployeeById(id);
                if(employee == null)
                {
                    response.Status = 404;
                    response.Message = "Employee not found";
                    response.Error = "Employee not found";
                    return response;
                }
                var result = _mapper.Map<Employee>(employee);
                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;

            }catch(Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO GetDesignationById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var designation = _employeeRepository.GetDesignationById(id);
                if (designation == null)
                {
                    response.Status = 404;
                    response.Message = "Designation not found";
                    response.Error = "Designation not found";
                    return response;
                }
                var result = _mapper.Map<Designation>(designation);
                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;

            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }

        public ResponseDTO GetHobbyById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var hobby = _employeeRepository.GetHobbyById(id);
                if (hobby == null)
                {
                    response.Status = 404;
                    response.Message = "Hobby not found";
                    response.Error = "Hobby not found";
                    return response;
                }
                var result = _mapper.Map<Hobby>(hobby);
                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;

            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetEmployeePagination(int page, int pageSize)
        {
            var response = new ResponseDTO();
            try
            {
                var employees = _mapper.Map<List<Employee>>(_employeeRepository.GetEmployeePagination(page, pageSize)).ToList();
                response.Status = 200;
                response.Message = "Ok";
                response.Data = employees;

            }catch( Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;

            }
            return response;
        }
        public ResponseDTO GetEmployeeByEmail(string email)
        {
            var response = new ResponseDTO();
            try
            {
                var employee = _employeeRepository.GetEmployeeByEmail(email);
                if(employee == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Employee not found";
                }
                var result = _mapper.Map<Employee>(employee);
                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;

            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO AddEmployee(AddEmployeeDTO employee)
        {
            var response = new ResponseDTO();
            try
            {
                var empEmail = _employeeRepository.GetEmployeeByEmail(employee.Email);
                if(empEmail != null)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Email Already exist";
                    return response;
                }
                var emp = new Employee()
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    MobileNumber = employee.MobileNumber,
                    Gender = employee.Gender,
                    DesignationId = employee.DesignationId,
                    EmployeeHobbies = employee.EmployeeHobbies?.Select(hobbyId => new EmployeeHobby { HobbyId = hobbyId }).ToList()
                };

                var empId = _employeeRepository.AddEmployee(emp);
                if(empId == 0)
                {
                    response.Status = 400;
                    response.Message = "Not created";
                    response.Error = "could not add user";
                    return response;
                }
                response.Status = 200;
                response.Message = "Created";
                response.Data = empId;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal server error";
                response.Error = ex.Message;

            }
            return response;
        }

        public ResponseDTO UpdateEmployee(UpdateEmployeeDTO employee)
        {

            var response = new ResponseDTO();
            
            try
            {
                var empById = _employeeRepository.GetEmployeeById(employee.Id);
                if (empById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Employee not found";
                    return response;
                }
                var empByEmail = _employeeRepository.GetEmployeeByEmail(employee.Email);
                if (empByEmail != null && empByEmail.Id != employee.Id)
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Email already exists";
                    return response;
                }
                var updatedEmployee = _mapper.Map<UpdateEmployeeDTO, Employee>(employee);
                updatedEmployee.EmployeeHobbies = employee.EmployeeHobbies.Select(hobbyId => new EmployeeHobby { HobbyId = hobbyId , EmployeeId = employee.Id }).ToList();

                var updateFlag = _employeeRepository.UpdateEmployee(updatedEmployee);

                if (updateFlag)
                {
                    response.Status = 204;
                    response.Message = "Updated";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Could not update Employee";
                }
                return response;
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = e.Message;
            }
            return response;
        }

        public ResponseDTO DeleteEmployee(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var empById = _employeeRepository.GetEmployeeById(id);
                if(empById == null)
                {
                    response.Status = 404;
                    response.Message = "Not found";
                    response.Error = "Employee not found";
                    return response;
                }
                var deleteFlag = _employeeRepository.DeleteEmployee(empById);
                if (deleteFlag)
                {
                    response.Status = 204;
                    response.Message = "Deleted";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not deleted";
                    response.Error = "Could not delete employee";
                }
            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server error";
                response.Error = ex.Message;

            }
            return response;
        }
        #endregion
    }
}
