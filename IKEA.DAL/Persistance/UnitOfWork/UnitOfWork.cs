using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Reposatories.Departments;
using IKEA.DAL.Persistance.Reposatories.Employee;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplcationDBContext dBContext;

        public IDepartmentReposatory departmentReposatory { get; }
        public IEmployeeReposatory employeeReposatory { get;  }

        public UnitOfWork(ApplcationDBContext dBContext)
        {
            this.dBContext = dBContext;
            departmentReposatory=new DepartmentReposatory(this.dBContext);
            employeeReposatory = new EmployeeReposatory(this.dBContext);
        }

        public async Task<int> complete()
        {
            return await dBContext.SaveChangesAsync();
        }

        //public void  dispose()
        //{
        //     dBContext.Dispose();
        //}
    }
}
