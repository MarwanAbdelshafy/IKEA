using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Department;
using Microsoft.EntityFrameworkCore;
using IKEA.DAL.Models.Employees;

namespace IKEA.DAL.Persistance.Data
{
    public class ApplcationDBContext : DbContext
    {
        public DbSet<Department> departments { get; set; }
        public DbSet<Employees> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



        public ApplcationDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
