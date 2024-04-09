using App.Core.Contracts;
using App.Core.Models.BudgetSummary;
using App.Core.Services;
using App.Infrastructure.Data.Models;
using App.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Test.UnitTests
{
    public class SummaryLogicTests:UnitTestsBase
    {
        private ISummaryLogicService summaryLogicService;        
        [SetUp]
        public void SetUp()
        {
            SetUpBase();
            string dateBillArchiveString = "2024-04-01 00:00:00.0000000";
            DateTime dateBillArchive = DateTime.ParseExact(dateBillArchiveString, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);

            _data.RemoveRange(Bill1, Bill2, Bill3);
            _data.RemoveRange(Budget, Budget2);
            _data.Remove(Summary);
            _data.RemoveRange(Salary1,Salary2,Salary3,Salary4);
            _data.RemoveRange(ElectricityBill, WaterBill, HeatBill, InternetBill, RentBill);
            ElectricityBill = new Bill()
            {
                Id = 1,
                BillTypeId = 1,
                Cost = 100.00M,
                IsPayed = true,
                PayerId = 1,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = false
            };
            _data.Bills.Add(ElectricityBill);
            WaterBill = new Bill()
            {
                Id = 2,
                BillTypeId = 2,
                Cost = 100.00M,
                IsPayed = true,
                PayerId = 3,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = false
            };
            _data.Bills.Add(WaterBill);
            HeatBill = new Bill()
            {
                Id = 3,
                BillTypeId = 3,
                Cost = 100.00M,
                IsPayed = true,
                PayerId = 3,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = false
            };
            _data.Bills.Add(HeatBill);
            InternetBill = new Bill()
            {
                Id = 4,
                BillTypeId = 4,
                Cost = 100.00M,
                IsPayed = true,
                PayerId = 4,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = false
            };
            _data.Bills.Add(InternetBill);
            RentBill = new Bill()
            {
                Id = 5,
                BillTypeId = 5,
                Cost = 100.00M,
                IsPayed = true,
                PayerId = 4,
                UserId = Guest.Id,
                Date = dateBillArchive,
                IsArchived = false
            };
            _data.Bills.Add(RentBill);
            _data.SaveChanges();


            summaryLogicService = new SummaryLogicService(_data);
        }
        [TearDown]public void TearDown()=>
         TearDownBase();



        [Test]
        public async Task GetSummary_ShouldReturnCorrectString()
        {
            string dateString = "2024-04-01 00:00:00.0000000";
            DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
            List<MemberSalaryFormViewModel> model = new List<MemberSalaryFormViewModel>() { new MemberSalaryFormViewModel()
            {
                Id=1,
                Name="Victor",
                 Salary = 1000.00M,
            },new MemberSalaryFormViewModel()
            {
                Id=2,
                Name="Danail",
                Salary = 0.00M,
            },new MemberSalaryFormViewModel()
            {
                Id=3,
                Name="Pesho",
                Salary = 1000.00M,
            },new MemberSalaryFormViewModel()
            {
                Id=4,
                Name="Ivan",
                Salary = 2000.00M,
            },};

            var expectedSummaryText = "Total Household Income: 4000.00<br>\r<br>Total Household Expences: 500.00<br>\r<br>-Victor payed: 100.00 which is 25.00 less.<br>\r<br>-Danail payed: 0 which is exact.<br>\r<br>-Pesho payed: 200.00 which is 75.00 too much.<br>\r<br>-Ivan payed: 200.00 which is 50.00 less.";

            var result = await summaryLogicService.GetSummaryAsync(model, Guest.Id, date);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedSummaryText));
        }
        [Test]
        public async Task GetSummary_ShouldSaveCorrectDataToDatabase()
        {
            string dateString = "2024-04-01 00:00:00.0000000";
            DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
            List<MemberSalaryFormViewModel> model = new List<MemberSalaryFormViewModel>() { new MemberSalaryFormViewModel()
            {
                Id=1,
                Name="Victor",
                 Salary = 1000.00M,
            },new MemberSalaryFormViewModel()
            {
                Id=2,
                Name="Danail",
                Salary = 0.00M,
            },new MemberSalaryFormViewModel()
            {
                Id=3,
                Name="Pesho",
                Salary = 1000.00M,
            },new MemberSalaryFormViewModel()
            {
                Id=4,
                Name="Ivan",
                Salary = 2000.00M,
            },};
            

            var result = await summaryLogicService.GetSummaryAsync(model, Guest.Id, date);
            var budget = _data.HouseholdBudgets.Where(b => b.UserId == Guest.Id && b.Date == date).Any();
            var memberSalaries = _data.MemberSalaries.Where(b => b.UserId == Guest.Id && b.Date == date).ToList();
            int salaryId = memberSalaries.Where(m => m.HouseholdMemberId == 1).Select(m=>m.Id).First();
            var salary = memberSalaries.Find(s=>s.Id==salaryId);
            Assert.That(result, Is.Not.Null);
            Assert.That(memberSalaries, Is.Not.Null);
            Assert.That(budget, Is.True);
            Assert.That(salary.Salary, Is.EqualTo(1000.00M));
            Assert.That(salary.Date, Is.EqualTo(date));

        }

        public class  SummaryLogicArchiveTests : UnitTestsBase
        {
            private ISummaryLogicService summaryLogicService;
            [OneTimeSetUp] public void SetUp() => summaryLogicService = new SummaryLogicService(_data);
            [Test]
            public async Task ArchiveBills_ShouldArchiveCorrectBills()
            {

                int archivedBillBeforeCount = _data.Bills.Where(b => b.UserId == Guest.Id && b.IsArchived == true && b.DeletedOn == null).Count();
                await summaryLogicService.ArchiveBillsAsync(Guest.Id);
                int archivedBillAfterCount = _data.Bills.Where(b => b.UserId == Guest.Id && b.IsArchived == true && b.DeletedOn == null).Count();
                int nonArchivedBillAfterCount = _data.Bills.Where(b => b.UserId == Guest.Id && b.IsArchived == false && b.DeletedOn == null).Count();
                Assert.That(archivedBillBeforeCount, Is.LessThan(archivedBillAfterCount));
                Assert.That(nonArchivedBillAfterCount, Is.Zero);
            }
        }
    }
}
