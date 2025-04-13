using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Department;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Reposatories._Genarics;

namespace IKEA.DAL.Persistance.Reposatories.Employee
{
    public interface IEmployeeReposatory:IGenericRepository<Employeess>
    {
        //IEnumerable<Employees> GetAll(bool WithNoTracking = true);

        //Employees? GetById(int id);

        //int Add(Employees employees);
        //int Update(Employees employees);
        //int Delete(Employees employees);
    }
}
