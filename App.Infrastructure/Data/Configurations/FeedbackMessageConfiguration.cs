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
    public class FeedbackMessageConfiguration : IEntityTypeConfiguration<FeedbackMessage>
    {
        public void Configure(EntityTypeBuilder<FeedbackMessage> builder)
        {
            builder
                .HasOne(b => b.SeverityType)
                .WithMany(p => p.Messages)
                .HasForeignKey(b => b.SeverityTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(b => b.Status)
                .WithMany(p => p.Messages)
                .HasForeignKey(b => b.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
