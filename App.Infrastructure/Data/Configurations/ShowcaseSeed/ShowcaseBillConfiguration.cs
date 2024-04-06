using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Infrastructure.Data.Models;

namespace App.Infrastructure.Data.Configurations.ShowcaseSeed
{
    public class ShowcaseBillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            var data = new ConfigurationHelper();           

            builder.HasData(new Bill[]
            {
                data.ElectricityBill,
                data.WaterBill,
                data.HeatBill,
                data.InternetBill,
                data.RentBill,
                data.Bill1,
                data.Bill2,
                data.Bill3
            });
               
        }
    }
}
