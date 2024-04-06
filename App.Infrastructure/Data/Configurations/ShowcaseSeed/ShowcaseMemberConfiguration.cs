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
    public class ShowcaseMemberConfiguration : IEntityTypeConfiguration<HouseholdMember>
    {
        public void Configure(EntityTypeBuilder<HouseholdMember> builder)
        {
            var data = new ConfigurationHelper();
            builder.HasData(new HouseholdMember[]
            {
                data.Member1,
                data.Member2,
                data.Member3,
                data.Member4,
            });
        }
    }
}
