using App.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data.Configurations.ShowcaseSeed
{
    public class ShowcaseSalaryConfiguration : IEntityTypeConfiguration<MemberSalary>
    {
        public void Configure(EntityTypeBuilder<MemberSalary> builder)
        {
            var data = new ConfigurationHelper();

            builder.HasData(new MemberSalary[]
            {
                data.Salary1,
                data.Salary2,
                data.Salary3,
                data.Salary4,
            });
        }
    }
}
