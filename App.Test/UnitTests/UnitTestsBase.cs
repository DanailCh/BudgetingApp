using App.Infrastructure.Data.Models;
using App.Test.Mocks;
using HouseholdBudgetingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Test.UnitTests
{
    public class UnitTestsBase
    {
        protected ApplicationDbContext _data;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            _data = DataBaseMock.Instance;
            SeedDatabase();
        }

        [OneTimeTearDown]
        public void TearDownBase()=>_data.Dispose();

        public ApplicationUser Guest { get; private set; }
        public ApplicationUser Guest2 { get; private set; }
        public ApplicationUser Admin { get; private set; }
        public ApplicationUser MasterAdmin { get; private set; }

        public HouseholdMember Member1 { get; set; }
        public HouseholdMember Member2 { get; set; }
        public HouseholdMember Member3 { get; set; }
        public HouseholdMember Member4 { get; set; }
        public HouseholdMember Member5 { get; set; }
        public HouseholdMember Member6 { get; set; }

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
        public FeedbackMessage Feedback5 { get; set; }

        private void SeedDatabase()
        {
            string dateBillArchiveString = "2024-04-01 00:00:00.0000000";
            DateTime dateBillArchive = DateTime.ParseExact(dateBillArchiveString, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
            string dateBillString = "2024-05-01 00:00:00.0000000";
            DateTime dateBill = DateTime.ParseExact(dateBillString, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
            string dateFeedbackString = "2024-04-06 18:30:15.6776936";
            DateTime dateFeedback = DateTime.ParseExact(dateFeedbackString, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
            string dateFeedbackStringNew = "2024-05-06 18:30:15.6776936";
            DateTime dateFeedbackNew = DateTime.ParseExact(dateFeedbackStringNew, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);

            Admin = new ApplicationUser()
            {
                Id = "AdminId",
                UserName = "admin",
                Email = "admin@mail.com",
                PasswordSetupRequired = true,
            };
            _data.Users.Add(Admin);
            Guest = new ApplicationUser()
            {
                Id = "GuestId",
                UserName = "guest",
                Email = "guest@mail.com",
                PasswordSetupRequired = false,
            };
            _data.Users.Add(Guest);
            Guest2 = new ApplicationUser()
            {
                Id = "Guest2Id",
                UserName = "guest2",
                Email = "guest2@mail.com",
                PasswordSetupRequired = false,
            };
            _data.Users.Add(Guest2);
            MasterAdmin = new ApplicationUser()
            {
                Id = "MasterAdminId",
                UserName = "masteradmin",
                Email = "madmin@mail.com",
                PasswordSetupRequired = false,
            };
            _data.Users.Add(MasterAdmin);

            Member1 = new HouseholdMember()
            {
                Id = 1,
                Name = "Victor",
                UserId = Guest.Id,

            };
            _data.HouseholdMembers.Add(Member1);
            Member2 = new HouseholdMember()
            {
                Id = 2,
                Name = "Danail",
                UserId = Guest.Id,

            };
            _data.HouseholdMembers.Add(Member2);
            Member3 = new HouseholdMember()
            {
                Id = 3,
                Name = "Pesho",
                UserId = Guest.Id,

            };
            _data.HouseholdMembers.Add(Member3);
            Member4 = new HouseholdMember()
            {
                Id = 4,
                Name = "Ivan",
                UserId = Guest.Id,

            };
            _data.HouseholdMembers.Add(Member4);
            Member5 = new HouseholdMember()
            {
                Id = 5,
                Name = "Sasha",
                UserId = Guest2.Id,

            };
            _data.HouseholdMembers.Add(Member5);
            Member6 = new HouseholdMember()
            {
                Id = 6,
                Name = "Petq",
                UserId = Guest2.Id,

            };
            _data.HouseholdMembers.Add(Member6);

            ElectricityBill = new Bill()
            {
                Id = 1,
                BillTypeId = 1,
                Cost = 150.00M,
                IsPayed = true,
                PayerId = 1,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = true
            };
            _data.Bills.Add(ElectricityBill);
            WaterBill = new Bill()
            {
                Id = 2,
                BillTypeId = 2,
                Cost = 70.00M,
                IsPayed = true,
                PayerId = 2,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = true
            };
            _data.Bills.Add(WaterBill);
            HeatBill = new Bill()
            {
                Id = 3,
                BillTypeId = 3,
                Cost = 150.00M,
                IsPayed = true,
                PayerId = 3,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = true
            };
            _data.Bills.Add(HeatBill);
            InternetBill = new Bill()
            {
                Id = 4,
                BillTypeId = 4,
                Cost = 20.00M,
                IsPayed = true,
                PayerId = 4,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = true
            };
            _data.Bills.Add(InternetBill);
            RentBill = new Bill()
            {
                Id = 5,
                BillTypeId = 5,
                Cost = 1000.00M,
                IsPayed = true,
                PayerId = 4,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = true
            };
            _data.Bills.Add(RentBill);

            Bill1 = new Bill()
            {
                Id = 6,
                BillTypeId = 1,
                Cost = 100.00M,
                IsPayed = false,
                PayerId = null,
                UserId = Guest.Id,
                Date = dateBill,
                IsArchived = false
            };
            _data.Bills.Add(Bill1);
            Bill2 = new Bill()
            {
                Id = 7,
                BillTypeId = 3,
                Cost = 150.00M,
                IsPayed = false,
                PayerId = null,
                UserId = Guest.Id,
                Date = dateBill,
                IsArchived = false
            };
            _data.Bills.Add(Bill2);
            Bill3 = new Bill()
            {
                Id = 8,
                BillTypeId = 5,
                Cost = 1050.00M,
                IsPayed = false,
                PayerId = null,
                UserId = Guest.Id,
                Date = dateBill,
                IsArchived = false
            };
            _data.Bills.Add(Bill3);

            Budget = new HouseholdBudget()
            {
                Id = 1,
                Date = dateBillArchive,
                Income = 6500.00M,
                Expences = 1390.00M,
                UserId = Guest.Id,
            };
            _data.HouseholdBudgets.Add(Budget);

            Summary = new EndMonthSummary()
            {
                Id = 1,
                Date = dateBillArchive,
                Summary = "Total Household Income: 6500.00<br> <br>Total Household Expences: 1390.00<br> <br>-Victor payed: 150.00 which is 106.62 less.<br> <br>-Danail payed: 70.00 which is 314.92 less.<br> <br>-pesho payed: 150.00 which is 170.77 less.<br> <br>-ivan payed: 1020.00 which is 592.31 too much.",
                IsResolved = false,
                UserId = Guest.Id,

            };
            _data.EndMonthSummaries.Add(Summary);

            Salary1 = new MemberSalary()
            {
                Id = 1,
                HouseholdMemberId = 1,
                Date = dateBillArchive,
                Salary = 1200.00M,
                UserId = Guest.Id,
            };
            _data.MemberSalaries.Add(Salary1);
            Salary2 = new MemberSalary()
            {
                Id = 2,
                HouseholdMemberId = 2,
                Date = dateBillArchive,
                Salary = 1800.00M,
                UserId = Guest.Id,
            };
            _data.MemberSalaries.Add(Salary2);
            Salary3 = new MemberSalary()
            {
                Id = 3,
                HouseholdMemberId = 3,
                Date = dateBillArchive,
                Salary = 1500.00M,
                UserId = Guest.Id,
            };
            _data.MemberSalaries.Add(Salary3);
            Salary4 = new MemberSalary()
            {
                Id = 4,
                HouseholdMemberId = 4,
                Date = dateBillArchive,
                Salary = 2000.00M,
                UserId = Guest.Id,
            };
            _data.MemberSalaries.Add(Salary4);

            Feedback1 = new FeedbackMessage()
            {
                Id = 1,
                Title = "Title for Feedback1",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                SenderId = Guest.Id,
                Date = dateFeedbackNew,
                SeverityTypeId = null,
                Comment = null,
                StatusId = 1,
                IsReadByUser = true,
            };
            _data.FeedbackMessages.Add(Feedback1);
            Feedback2 = new FeedbackMessage()
            {
                Id = 2,
                Title = "Title for Feedback2",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Dictum fusce ut placerat orci nulla pellentesque dignissim enim sit.",
                SenderId = Guest.Id,
                Date = dateFeedback,
                SeverityTypeId = 1,
                Comment = "Your feedback has been acknowledged and we are working on solving the problem",
                StatusId = 2,
                IsReadByUser = false,
            };
            _data.FeedbackMessages.Add(Feedback2);
            Feedback3 = new FeedbackMessage()
            {
                Id = 3,
                Title = "Title for Feedback3",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                SenderId = Guest.Id,
                Date = dateFeedback,
                SeverityTypeId = 3,
                Comment = "Your feedback has been acknowledged and we are working on solving the problem",
                StatusId = 2,
                IsReadByUser = true,
            };
            _data.FeedbackMessages.Add(Feedback3);
            Feedback4 = new FeedbackMessage()
            {
                Id = 4,
                Title = "Title for Feedback4",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                SenderId = Guest.Id,
                Date = dateFeedback,
                SeverityTypeId = 2,
                Comment = "We are happy to inform you that the issue has been resolved",
                StatusId = 3,
                IsReadByUser = false,
            };
            _data.FeedbackMessages.Add(Feedback4);
            Feedback5 = new FeedbackMessage()
            {
                Id = 5,
                Title = "Title for Feedback5",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                SenderId = Guest2.Id,
                Date = dateFeedback,
                SeverityTypeId = 2,
                Comment = "We are happy to inform you that the issue has been resolved",
                StatusId = 3,
                IsReadByUser = false,
            };
            _data.FeedbackMessages.Add(Feedback5);

            _data.SaveChanges();
        }
    }
}
