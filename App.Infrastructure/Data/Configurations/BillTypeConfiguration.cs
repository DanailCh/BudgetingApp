﻿using App.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data.Configurations
{
    public class BillTypeConfiguration:IEntityTypeConfiguration<BillType>
    {
        public void Configure(EntityTypeBuilder<BillType> builder)
        {
            var data = new ConfigurationHelper();
            builder.HasData(new BillType[]
            {
               data.ElectricityType,
               data.WaterType,
               data.HeatType,
               data.InternetType,
               data.RentType
              
            });
        }
    }
}
