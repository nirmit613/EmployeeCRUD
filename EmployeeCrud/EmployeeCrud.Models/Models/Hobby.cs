using System.ComponentModel.DataAnnotations;

namespace EmployeeCrud.Models.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId { get; set; }
        public string HobbyName { get; set; }
    }
}
