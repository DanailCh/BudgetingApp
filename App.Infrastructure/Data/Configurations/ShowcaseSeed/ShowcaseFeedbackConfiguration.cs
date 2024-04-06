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
    public class ShowcaseFeedbackConfiguration : IEntityTypeConfiguration<FeedbackMessage>
    {
        public void Configure(EntityTypeBuilder<FeedbackMessage> builder)
        {
            var data = new ConfigurationHelper();
            builder.HasData(new FeedbackMessage[]
            {
                data.Feedback1,
                data.Feedback2,
                data.Feedback3,
                data.Feedback4
            });
        }
    }
}
