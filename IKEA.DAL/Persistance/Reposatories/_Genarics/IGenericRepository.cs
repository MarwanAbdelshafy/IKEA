using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Employees;

namespace IKEA.DAL.Persistance.Reposatories._Genarics
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IQueryable<T> GetAll(bool WithNoTracking = true);

        Task<T>? GetById(int id);

        void  Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
