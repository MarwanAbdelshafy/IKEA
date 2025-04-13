using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Department;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Reposatories._Genarics;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Persistance.Reposatories.Employee
{
    public class EmployeeReposatory :GenericRepository<Employeess> ,IEmployeeReposatory
    {
        private readonly ApplcationDBContext _dbContext;

        public EmployeeReposatory(ApplcationDBContext context) : base(context)
        {
            _dbContext = context;
        }
        //    private readonly ApplcationDBContext _dbContext;

        //    public EmployeeReposatory(ApplcationDBContext context)
        //    {
        //        _dbContext = context;
        //    }

        //    public IEnumerable<Employees> GetAll(bool WithNoTracking = true)
        //    {
        //        if (WithNoTracking)
        //            return _dbContext.Employees.Where(D => D.IsDeleted == false).AsNoTracking().ToList();

        //        return _dbContext.Employees.Where(D => D.IsDeleted == false).ToList();
        //    }

        //    public Employees? GetById(int id)
        //    {
        //        return _dbContext.Employees.Find(id);
        //    }

        //    public int Add(Employees employees)
        //    {
        //        _dbContext.Employees.Add(employees);
        //        return _dbContext.SaveChanges();
        //    }

        //    public int Update(Employees employees)
        //    {
        //        _dbContext.Employees.Update(employees);
        //        return _dbContext.SaveChanges();
        //    }

        //    public int Delete(Employees employees)
        //    {
        //        employees.IsDeleted = true;
        //        _dbContext.Employees.Update(employees);
        //        return _dbContext.SaveChanges();
        //    }
    }

}

