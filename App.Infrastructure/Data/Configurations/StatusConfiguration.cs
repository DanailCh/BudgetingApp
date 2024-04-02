using App.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data.Configurations
{
    public class StatusConfiguration: IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            var data = new ConfigurationHelper();
            builder.HasData(new Status[]
            {
               data.New,
               data.InProgress,
               data.Done,

            });
        }
    }
}
