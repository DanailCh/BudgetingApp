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
    public class ShowcaseBudgetConfiguration:IEntityTypeConfiguration<HouseholdBudget>
    {
        public void Configure(EntityTypeBuilder<HouseholdBudget> builder)
        {
            var data = new ConfigurationHelper();

            builder.HasData(data.Budget);

        }
    }
}
