using System.ComponentModel.DataAnnotations;

namespace EmployeeCrud.Services.DTO
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int DesignationId { get; set; }
        public List<int> EmployeeHobbies { get; set; }

    }
}
