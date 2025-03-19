using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Department;
using IKEA.DAL.Persistance.Reposatories._Genarics;

namespace IKEA.DAL.Persistance.Reposatories.Departments
{
    public interface IDepartmentReposatory:IGenericRepository<Department>
    {
        //IEnumerable<Department> GetAll(bool WithNoTracking = true);

        //Department? GetById(int id);

        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);
    }
}
