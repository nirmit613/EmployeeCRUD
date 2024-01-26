using EmployeeCrud.Models.Interfaces;
using EmployeeCrud.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrud.Models.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Fields
        private readonly AppDbContext _context;
        #endregion

        #region Constructors
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public IEnumerable<Employee> GetEmployees()

        {
            return _context.Employees.Include(e => e.Designation).Include(e => e.EmployeeHobbies)
            .ThenInclude(eh => eh.Hobby).ToList();
        }
        public IEnumerable<EmployeeHobby> GetEmployeesHobbys()
        {
            return _context.EmployeesHobbies.ToList();
        }
        public IEnumerable<Designation> GetDesignations()
        {
            return _context.Designations.ToList();
        }
        public IEnumerable<Hobby> GetHobbys()
        {
            return _context.Hobbies.ToList();
        }
        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Include(h=>h.EmployeeHobbies).FirstOrDefault(e => e.Id == id);

        }
        public Designation GetDesignationById(int id)
        {
            return _context.Designations.FirstOrDefault(d => d.DesignationId == id);
        }
        public List<EmployeeHobby> GetHobbiesByEmpId(int empId)
        {
            return _context.EmployeesHobbies
                .Where(eh => eh.EmployeeId == empId)
                .ToList();
        }

        public Hobby GetHobbyById(int id)
        {
            return _context.Hobbies.FirstOrDefault(h => h.HobbyId == id);
        }
        public IEnumerable<Employee> GetEmployeePagination(int PageNo,int RowsPerPage)
        {
            return _context.Employees.Skip((PageNo - 1)*RowsPerPage).Take(RowsPerPage).ToList();
        }

        public Employee GetEmployeeByEmail(string email)
        {
            return _context.Employees.FirstOrDefault(e=>e.Email == email);
        }

        public int AddEmployee(Employee employee)
        {
            _context.Add(employee);

            if (_context.SaveChanges() > 0)
                return employee.Id;
            else
                return 0;
        }

        public bool EmployeeHasHobby(int employeeId, int hobbyId)
        {
            return _context.EmployeesHobbies.Any(eh => eh.EmployeeId == employeeId && eh.Id == hobbyId);
        }
        public bool UpdateEmployee(Employee employee)
        {
           var existingEmployee = _context.Employees.Include(e => e.EmployeeHobbies).FirstOrDefault(e => e.Id == employee.Id);


            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.MobileNumber = employee.MobileNumber;
                existingEmployee.Gender = employee.Gender;


                foreach (var existingHobby in existingEmployee.EmployeeHobbies.ToList())
                {
                    if (!employee.EmployeeHobbies.Any(updatedHobby => updatedHobby.HobbyId == existingHobby.HobbyId))
                    {
                        existingEmployee.EmployeeHobbies.Remove(existingHobby);
                        _context.Entry(existingHobby).State = EntityState.Deleted; 
                    }
                }
                foreach (var hobby in employee.EmployeeHobbies)
                {
                    var existingHobby = existingEmployee.EmployeeHobbies.FirstOrDefault(h => h.HobbyId == hobby.HobbyId);
                    if (existingHobby != null)
                    {
                        existingHobby.HobbyId = hobby.HobbyId;
                    }
                    else
                    {
                        // Add new hobby
                        var newHobby = new EmployeeHobby
                        {
                            HobbyId = hobby.HobbyId,
                            EmployeeId = hobby.EmployeeId,
                            // Set other properties as needed
                        };
                        existingEmployee.EmployeeHobbies.Add(newHobby);
                        _context.Entry(newHobby).State = EntityState.Added; // Ensure new hobby is marked as added
                    }
                }

                _context.Entry(existingEmployee).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }

            return false;
        }
        public bool DeleteEmployee(Employee employee) 
        {
            _context.Remove(employee);
            return _context.SaveChanges() > 0;
        
        }
        #endregion

    }
}
