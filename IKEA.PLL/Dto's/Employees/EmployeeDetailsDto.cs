﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Common.Enum_s;

namespace IKEA.BLL.Dto_s.Employees
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int? Age { get; set; }

        public string? Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

        #region Admin

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string? departments { get; set; }

        public string? ImageName { get; set; }

        #endregion
    }
}
