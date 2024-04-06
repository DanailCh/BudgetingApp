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
    public class ShowcaseSymmaryConfiguration : IEntityTypeConfiguration<EndMonthSummary>
    {
        public void Configure(EntityTypeBuilder<EndMonthSummary> builder)
        {
            var data = new ConfigurationHelper();
            builder.HasData(data.Summary);
        }
    }
}
