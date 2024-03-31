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
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {

            builder
               .HasOne(b => b.Payer)
               .WithMany(p => p.Bills)
               .HasForeignKey(b => b.PayerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(b => b.BillType)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.BillTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
