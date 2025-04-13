using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Common.Enum_s;
using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Dto_s.Employees
{
    public class CreatedEmployeeDto
    {
        public int? Age { get; set; }

        [MaxLength(50,ErrorMessage ="Max Lenth of Name is 50 Chars")]   
        [MinLength(5,ErrorMessage ="Min Lenth of Name is 5 Chars")]   
        public string name { get; set; }

        public string? Address { get; set; }

        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name="Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

        [Display(Name = "Departmen")]

        public int? DepartmentId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
