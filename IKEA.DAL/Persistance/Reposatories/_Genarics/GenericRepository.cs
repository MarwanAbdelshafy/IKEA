using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Persistance.Reposatories._Genarics
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly ApplcationDBContext _dbContext;

        public GenericRepository(ApplcationDBContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<T> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
                return _dbContext.Set<T>().Where(D => D.IsDeleted == false).AsNoTracking().ToList();

            return _dbContext.Set<T>().Where(D => D.IsDeleted == false).ToList();
        }

        public T? GetById(int id)
        {
            var itme = _dbContext.Set<T>().Find(id);

            return itme;
           // return _dbContext.Set<T>().Find(id);
        }

        public int Add(T  Item)
        {
            _dbContext.Set<T>().Add(Item);
            return _dbContext.SaveChanges();
        }

        public int Update(T Item)
        {
            _dbContext.Set<T>().Update(Item);
            return _dbContext.SaveChanges();
        }

        public int Delete(T Item)
        {
            Item.IsDeleted = true;
            _dbContext.Set<T>().Update(Item);
            return _dbContext.SaveChanges();
        }
    }
}
