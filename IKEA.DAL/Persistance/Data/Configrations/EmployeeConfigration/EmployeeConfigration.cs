using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Common.Enum_s;
using IKEA.DAL.Models.Employees;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKEA.DAL.Persistance.Data.Configrations.EmployeeConfigration
{
    public class EmployeeConfigration : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");

            builder.Property(E => E.Gender).HasConversion
                (
                (gender)=>gender.ToString(),
                (gender)=>(Gender)Enum.Parse(typeof(Gender), gender)
                );
            
            builder.Property(E => E.EmployeeType).HasConversion
                (
                (type)=> type.ToString(),
                (type) =>(EmployeeType)Enum.Parse(typeof(Employees), type)
                );

            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");

            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()");
        }
    }
}
