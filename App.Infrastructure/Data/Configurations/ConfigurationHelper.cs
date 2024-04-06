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
        /// <summary>
        /// Initial seed for all three types of users
        /// </summary>
        public ApplicationUser  GuestUser { get; set; }
        public ApplicationUser  AdminUser { get; set; }
        public ApplicationUser MasterAdminUser { get; set; }
        
        /// <summary>
        /// Seed for Default BillTypes
        /// </summary>
        public  BillType ElectricityType { get; set; }
        public  BillType WaterType { get; set; }
        public  BillType HeatType { get; set; }
        public  BillType InternetType { get; set; }
        public  BillType RentType { get; set; }

        /// <summary>
        /// Seed for all severity types
        /// </summary>
        public SeverityType LowSeverity { get; set; }
        public SeverityType MediumSeverity { get; set; }
        public SeverityType HighSeverity { get; set; }

        /// <summary>
        /// Seed for all statuses
        /// </summary>
        public Status New { get; set; }
        public Status InProgress { get; set; }
        public Status Done { get; set; }

        /// <summary>
        /// Seed for project showcase
        /// </summary>
        public HouseholdMember Member1 { get; set; }
        public HouseholdMember Member2 { get; set; }
        public HouseholdMember Member3 { get; set; }
        public HouseholdMember Member4 { get; set; }

        public Bill ElectricityBill { get; set; }
        public Bill WaterBill { get; set; }
        public Bill HeatBill { get; set; }
        public Bill InternetBill { get; set; }
        public Bill RentBill { get; set; }

        public Bill Bill1 { get; set; }
        public Bill Bill2 { get; set; }
        public Bill Bill3 { get; set; }

        public EndMonthSummary Summary { get; set; }
        
        public HouseholdBudget Budget { get; set; }

        public MemberSalary Salary1 { get; set; }
        public MemberSalary Salary2 { get; set; }
        public MemberSalary Salary3 { get; set; }
        public MemberSalary Salary4 { get; set; }

        public FeedbackMessage Feedback1 { get; set; }
        public FeedbackMessage Feedback2 { get; set; }
        public FeedbackMessage Feedback3 { get; set; }
        public FeedbackMessage Feedback4 { get; set; }

        public ConfigurationHelper()
        {
            SeedUsers();
            SeedBillTypes();
            SeedMembers();
            SeedBills();
            SeedSymmary();
            SeedBudget();
            SeedSalaries();            
            SeedSeverityTypes();
            SeedStatus();
            SeedFeedback();
        }

        private void SeedFeedback()
        {
            Feedback1 = new FeedbackMessage()
            {
                Id = 1,
                Title = "Title for Feedback1",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",               
                SenderId = GuestUser.Id,
                Date = DateTime.Now,
                SeverityTypeId=null,
                Comment=null,
                StatusId=1,
                IsReadByUser=true,
            };
            Feedback2 = new FeedbackMessage()
            {
                Id = 2,
                Title = "Title for Feedback2",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Dictum fusce ut placerat orci nulla pellentesque dignissim enim sit.",
                SenderId = GuestUser.Id,
                Date = DateTime.Now.AddDays(-10),
                SeverityTypeId = 1,
                Comment = "Your feedback has been acknowledged and we are working on solving the problem",
                StatusId = 2,
                IsReadByUser = false,
            };
            Feedback3 = new FeedbackMessage()
            {
                Id = 3,
                Title = "Title for Feedback3",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                SenderId = GuestUser.Id,
                Date = DateTime.Now.AddDays(-15),
                SeverityTypeId = 3,
                Comment = "Your feedback has been acknowledged and we are working on solving the problem",
                StatusId = 2,
                IsReadByUser = true,
            };
            Feedback4 = new FeedbackMessage()
            {
                Id = 4,
                Title = "Title for Feedback4",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                SenderId = GuestUser.Id,
                Date = DateTime.Now.AddMonths(-1),
                SeverityTypeId = 2,
                Comment = "We are happy to inform you that the issue has been resolved",
                StatusId = 3,
                IsReadByUser = false,
            };
        }

        private void SeedSalaries()
        {
            Salary1 = new MemberSalary()
            {
                Id = 1,
                HouseholdMemberId=1,
                Date = GetDate(),               
                Salary = 1200.00M,
                UserId = GuestUser.Id,
            };
            Salary2 = new MemberSalary()
            {
                Id = 2,
                HouseholdMemberId = 2,
                Date = GetDate(),
                Salary = 1800.00M,
                UserId = GuestUser.Id,
            };
            Salary3 = new MemberSalary()
            {
                Id = 3,
                HouseholdMemberId = 3,
                Date = GetDate(),
                Salary = 1500.00M,
                UserId = GuestUser.Id,
            };
            Salary4 = new MemberSalary()
            {
                Id = 4,
                HouseholdMemberId = 4,
                Date = GetDate(),
                Salary = 2000.00M,
                UserId = GuestUser.Id,
            };
        }

        private void SeedBudget()
        {
            Budget = new HouseholdBudget()
            {
                Id = 1,
                Date = GetDate(),
                Income=6500.00M,
                Expences=1390.00M,
                UserId = GuestUser.Id,
            };
        }

        private void SeedSymmary()
        {
            Summary = new EndMonthSummary()
            {
                Id = 1,
                Date = GetDate(),
                Summary= "Total Household Income: 6500.00<br> <br>Total Household Expences: 1390.00<br> <br>-Victor payed: 150.00 which is 106.62 less.<br> <br>-Danail payed: 70.00 which is 314.92 less.<br> <br>-pesho payed: 150.00 which is 170.77 less.<br> <br>-ivan payed: 1020.00 which is 592.31 too much.",
                IsResolved= false,
                UserId = GuestUser.Id,

            };
        }

        private void SeedMembers()
        {
            Member1 = new HouseholdMember()
            {
                Id = 1,
                Name = "Victor",
                UserId= GuestUser.Id,

            };
            Member2 = new HouseholdMember()
            {
                Id = 2,
                Name = "Danail",
                UserId = GuestUser.Id,

            };
            Member3 = new HouseholdMember()
            {
                Id = 3,
                Name = "Pesho",
                UserId = GuestUser.Id,

            };
            Member4 = new HouseholdMember()
            {
                Id = 4,
                Name = "Ivan",
                UserId = GuestUser.Id,

            };
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

        private void SeedBills()
        {
           
            ElectricityBill = new Bill()
            {
                Id=1,
                BillTypeId = 1,
                Cost = 150.00M,
                IsPayed = true,
                PayerId = 1,
                UserId = GuestUser.Id,
                Date = GetDate(),
                IsArchived = true
            };
            WaterBill = new Bill()
            {
                Id=2,
                BillTypeId = 2,
                Cost = 70.00M,
                IsPayed = true,
                PayerId = 2,
                UserId = GuestUser.Id,
                Date = GetDate(),
                IsArchived = true
            };
            HeatBill = new Bill()
            {
                Id = 3,
                BillTypeId =3,
                Cost = 150.00M,
                IsPayed = true,
                PayerId = 3,
                UserId = GuestUser.Id,
                Date = GetDate(),
                IsArchived = true
            };
            InternetBill = new Bill()
            {
                Id = 4,
                BillTypeId = 4,
                Cost = 20.00M,
                IsPayed = true,
                PayerId = 4,
                UserId = GuestUser.Id,
                Date = GetDate(),
                IsArchived = true
            };
            RentBill = new Bill()
            {
                Id = 5,
                BillTypeId = 5,
                Cost = 1000.00M,
                IsPayed = true,
                PayerId = 4,
                UserId = GuestUser.Id,
                Date = GetDate(),
                IsArchived = true
            };
            Bill1 = new Bill()
            {
                Id = 6,
                BillTypeId = 1,
                Cost = 100.00M,
                IsPayed = false,
                PayerId = null,
                UserId = GuestUser.Id,
                Date = GetDate().AddMonths(1),
                IsArchived = false
            };
            Bill2 = new Bill()
            {
                Id = 7,
                BillTypeId = 3,
                Cost = 150.00M,
                IsPayed = false,
                PayerId = null,
                UserId = GuestUser.Id,
                Date = GetDate().AddMonths(1),
                IsArchived = false
            };
            Bill3 = new Bill()
            {
                Id = 8,
                BillTypeId = 5,
                Cost = 1050.00M,
                IsPayed = false,
                PayerId = null,
                UserId = GuestUser.Id,
                Date = GetDate().AddMonths(1),
                IsArchived = false
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

        private DateTime GetDate()
        {
            DateTime date = DateTime.Now;
            return new DateTime(date.Year, date.Month, 1);
        }
    }
}
