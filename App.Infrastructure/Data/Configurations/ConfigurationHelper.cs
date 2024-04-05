﻿using App.Infrastructure.Data.Models;
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
        public ApplicationUser  GuestUser { get; set; }
        public ApplicationUser  AdminUser { get; set; }
        public ApplicationUser MasterAdminUser { get; set; }

        public  BillType ElectricityType { get; set; }
        public  BillType WaterType { get; set; }
        public  BillType HeatType { get; set; }
        public  BillType InternetType { get; set; }
        public  BillType RentType { get; set; }

        public  SeverityType LowSeverity { get; set; }
        public  SeverityType MediumSeverity { get; set; }
        public  SeverityType HighSeverity { get; set; }

        public Status New { get; set; }
        public Status InProgress { get; set; }
        public Status Done { get; set; }

        public ConfigurationHelper()
        {
            SeedUsers();
            SeedBillTypes();
            SeedSeverityTypes();
            SeedStatus();

        }

        private void SeedStatus()
        {
            New = new Status()
            {
                Id = 1,
                Name = "New",

            };
            InProgress = new Status()
            {
                Id = 2,
                Name = "In Progress",

            };
            Done = new Status()
            {
                Id = 3,
                Name = "Done",

            };
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
            var hasher = new PasswordHasher<ApplicationUser>();

            AdminUser = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com",
                PasswordSetupRequired=true,
            };

            AdminUser.PasswordHash =
                 hasher.HashPassword(AdminUser, "admin123");

            GuestUser = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest",
                NormalizedUserName = "GUEST",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com",
                PasswordSetupRequired=false,
            };

            GuestUser.PasswordHash =
            hasher.HashPassword(GuestUser, "guest123");

            MasterAdminUser = new ApplicationUser()
            {
                Id = "7245911e-1ad6-46aa-a087-e4eb1445b500",
                UserName = "masteradmin",
                NormalizedUserName = "MASTERADMIN",
                Email = "madmin@mail.com",
                NormalizedEmail = "madmin@mail.com",
                PasswordSetupRequired= false,
            };

            MasterAdminUser.PasswordHash =
                 hasher.HashPassword(MasterAdminUser, "madmin123");
        }
    }
}
