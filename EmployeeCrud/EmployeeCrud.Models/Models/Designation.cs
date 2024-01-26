﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeCrud.Models.Models
{
    public class Designation
    {
        [Key]
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
    }
}
