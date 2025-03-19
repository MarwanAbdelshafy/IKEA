using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Department;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Reposatories._Genarics;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Persistance.Reposatories.Departments
{
    public class DepartmentReposatory :GenericRepository<Department>, IDepartmentReposatory
    {
        private readonly ApplcationDBContext _dbContext;

        public DepartmentReposatory(ApplcationDBContext context):base(context) 
        {
            _dbContext = context;
        }
        //private readonly ApplcationDBContext _dbContext; 

        //public DepartmentReposatory(ApplcationDBContext context)
        //{
        //    _dbContext = context;
        //}

        //public IEnumerable<Department> GetAll(bool WithNoTracking = true)
        //{
        //    if (WithNoTracking)
        //        return _dbContext.departments.Where(D=>D.IsDeleted==false).AsNoTracking().ToList();

        //    return _dbContext.departments.Where(D => D.IsDeleted == false).ToList();
        //}

        //public Department? GetById(int id)
        //{
        //    return _dbContext.departments.Find(id);
        //}

        //public int Add(Department department)
        //{
        //    _dbContext.departments.Add(department);
        //    return _dbContext.SaveChanges();
        //}

        //public int Update(Department department)
        //{
        //    _dbContext.departments.Update(department);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    department.IsDeleted = true;
        //    _dbContext.departments.Update(department);
        //    return _dbContext.SaveChanges();
        //}
    }

}
