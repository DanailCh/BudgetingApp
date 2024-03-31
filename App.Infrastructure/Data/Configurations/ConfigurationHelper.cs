using App.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data.Configurations
{
    public  class ConfigurationHelper
    {
        public  IdentityUser GuestUser { get; set; }
        public  IdentityUser AdminUser { get; set; }

        public  BillType ElectricityType { get; set; }
        public  BillType WaterType { get; set; }
        public  BillType HeatType { get; set; }
        public  BillType InternetType { get; set; }
        public  BillType RentType { get; set; }

        public  SeverityType LowSeverity { get; set; }
        public  SeverityType MediumSeverity { get; set; }
        public  SeverityType HighSeverity { get; set; }

        public ConfigurationHelper()
        {
            SeedUsers();
            SeedBillTypes();
            SeedSeverityTypes();
           
        }

        private void SeedSeverityTypes()
        {
            LowSeverity = new SeverityType()
            {
                Id = 1,
                Name = "Low",

            };
            MediumSeverity = new SeverityType()
            {
                Id = 2,
                Name = "Medium",

            };
            HighSeverity = new SeverityType()
            {
                Id = 3,
                Name = "High",

            };
        }

        private void SeedBillTypes()
        {
            ElectricityType = new BillType()
            {
                Id = 1,
                Name = "Electricity",

            };
            WaterType = new BillType()
            {
                Id = 2,
                Name = "Water",

            };
            HeatType = new BillType()
            {
                Id = 3,
                Name = "Heat",

            };
            InternetType = new BillType()
            {
                Id = 4,
                Name = "Internet",

            };
            RentType = new BillType()
            {
                Id = 5,
                Name = "Rent",

            };
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            AdminUser = new IdentityUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com"
            };

            AdminUser.PasswordHash =
                 hasher.HashPassword(AdminUser, "admin123");

            GuestUser = new IdentityUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest",
                NormalizedUserName = "GUEST",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com"
            };

            GuestUser.PasswordHash =
            hasher.HashPassword(GuestUser, "guest123");
        }
    }
}
