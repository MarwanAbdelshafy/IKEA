using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Persistance.Reposatories.Departments;
using IKEA.DAL.Persistance.Reposatories.Employee;

namespace IKEA.DAL.Persistance.UnitOfWork
{
    public interface IUnitOfWork//:IDisposable
    {
        public IDepartmentReposatory departmentReposatory { get;  }
        public IEmployeeReposatory employeeReposatory { get;  }

        public Task<int> complete();
        //void dispose();
    }
}
