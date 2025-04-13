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

        public IQueryable<T> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
                return _dbContext.Set<T>().Where(D => D.IsDeleted == false).AsNoTracking();

            return _dbContext.Set<T>().Where(D => D.IsDeleted == false);
        }

        public async Task<T?> GetById(int id)
        {

            var itme = await _dbContext.Set<T>().FindAsync(id);

            return itme;
            //return _dbContext.Set<T>().FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            //var itme = _dbContext.Set<T>().Find(id);
            //return itme;


            // return _dbContext.Set<T>().Find(id);
        }

        public void Add(T Item)
        {
            _dbContext.Set<T>().Add(Item);
            //return _dbContext.SaveChanges();
        }

        public void Update(T Item)
        {
            _dbContext.Set<T>().Update(Item);
           // return _dbContext.SaveChanges();
        }

        public void Delete(T Item)
        {
            Item.IsDeleted = true;
            _dbContext.Set<T>().Update(Item);
           // return _dbContext.SaveChanges();
        }

    }
}
