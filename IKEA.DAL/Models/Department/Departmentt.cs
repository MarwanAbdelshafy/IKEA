using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Employees;

namespace IKEA.DAL.Models.Department
{
    public class Departmentt : ModelBase
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }

        //navigatial prop Many
        public virtual ICollection<Employeess> Employees { get; set; } = new HashSet<Employeess>();
    }
}
