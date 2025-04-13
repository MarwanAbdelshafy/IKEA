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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IKEA.DAL.Models.Identity;

namespace IKEA.DAL.Persistance.Data
{
    public class ApplcationDBContext : IdentityDbContext<ApplicationUser>
    {

        public ApplcationDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        

        public DbSet<Departmentt> departments { get; set; }
        public DbSet<Employeess> Employees { get; set; }
    }
}
