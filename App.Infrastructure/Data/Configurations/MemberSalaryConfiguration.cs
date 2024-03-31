using App.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace App.Infrastructure.Data.Configurations
{
    public class MemberSalaryConfiguration : IEntityTypeConfiguration<MemberSalary>
    {
        public void Configure(EntityTypeBuilder<MemberSalary> builder)
        {
            builder
               .HasOne(m => m.HouseholdMember)
               .WithMany(h => h.MemberSalaries)
               .HasForeignKey(m => m.HouseholdMemberId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
