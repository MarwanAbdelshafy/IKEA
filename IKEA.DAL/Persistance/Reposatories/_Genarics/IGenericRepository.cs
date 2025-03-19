﻿using System;
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
        IEnumerable<T> GetAll(bool WithNoTracking = true);

        T? GetById(int id);

        int Add(T Entity);
        int Update(T Entity);
        int Delete(T Entity);
    }
}
