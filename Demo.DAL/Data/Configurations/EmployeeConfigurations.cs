using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).IsRequired().HasMaxLength(50);
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");
            
         //   builder.Property(E => E.Age).HasAnnotation("Range",(22,45));
          //  builder.Property(E => E.Email).HasAnnotation("EmailAddress", "Invalid email address");
           // builder.Property(E => E.Address).HasAnnotation("RegularExpression", @"[0-9]{1,3}-[a-zA-Z]{4,10}-[a-zA-A]{5,10}$");
            
        }
    }
}
