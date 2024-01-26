using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCrud.Models.Models
{
    public class EmployeeHobby
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("Hobby")]
        public int HobbyId { get; set; }
        public Hobby Hobby { get; set; }
    }
}
