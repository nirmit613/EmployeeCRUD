using System.ComponentModel.DataAnnotations;

namespace EmployeeCrud.Services.DTO
{
    public class AddEmployeeDTO
    {
        [Required(ErrorMessage = "Employee name is Required")]
        [MaxLength(50, ErrorMessage = "Employee Name can't be longer than 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is Invalid")]
        [MaxLength(64, ErrorMessage = "Email can't be longer than 64 characters")]
        public string Email { get; set; }
        [MaxLength(10, ErrorMessage = "Mobile number cannot be longer than 10 digits")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "At least one hobby is Required")]
        public List<int> EmployeeHobbies { get; set; }

    }
}
