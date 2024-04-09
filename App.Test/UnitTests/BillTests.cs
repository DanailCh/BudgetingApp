using App.Core.Contracts;
using App.Core.Enum;
using App.Core.Models.Archive.Bill;
using App.Core.Models.Archive.MemberSalary;
using App.Core.Models.Bill;
using App.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Test.UnitTests
{
    public class BillTests : UnitTestsBase
    {
        private IBillService billService;

        [OneTimeSetUp]
        public void SetUp() => billService = new BillService(_data);

        [Test]
        public async Task AllBills_ShouldReturnCorrectCount()
        {
            string date = "2024-04-01 00:00:00.0000000";
            DateTime dateBillArchive = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
            AllArchivedBillsQueryModel model = new AllArchivedBillsQueryModel()
            {
                BillMonth = dateBillArchive,
                BillTypeId = 1,
                CurrentPage = 1,
            };
            var result = await billService.AllBillsAsync(Guest.Id, model);
            int expected = _data.Bills.Where(x => x.UserId == Guest.Id && x.IsArchived == true && x.Date == dateBillArchive && x.BillTypeId == 1).Count();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ArchivedBillsCount, Is.EqualTo(expected));
            Assert.That(result.ArchivedBills.Count, Is.EqualTo(expected));
            Assert.That(result.ArchivedBillsForDownload.Count, Is.EqualTo(expected));

        }

        [Test]
        public async Task AllBills_ShouldReturnInCorrectOrder()
        {

            AllArchivedBillsQueryModel model = new AllArchivedBillsQueryModel()
            {
                SortingDate = BillsSorting.DateAscending
            };
            var result = await billService.AllBillsAsync(Guest.Id, model);
            var resultBillId = result.ArchivedBills.Select(x => x.Id).First();
            var expectedBillId = _data.Bills.Where(x => x.UserId == Guest.Id && x.IsArchived == true).OrderBy(x => x.Date).Select(x => x.Id).First();

            Assert.That(resultBillId, Is.EqualTo(expectedBillId));

            AllArchivedBillsQueryModel model2 = new AllArchivedBillsQueryModel()
            {
                SortingDate = BillsSorting.DateAscending,
                SortingCost = BillsSorting.CostCheapest
            };
            var result2 = await billService.AllBillsAsync(Guest.Id, model2);
            var resultBillId2 = result2.ArchivedBills.Select(x => x.Id).First();
            var expectedBillId2 = _data.Bills.Where(x => x.UserId == Guest.Id && x.IsArchived == true).OrderBy(x => x.Date).ThenBy(x => x.Cost).Select(x => x.Id).First();

            Assert.That(resultBillId2, Is.EqualTo(expectedBillId2));

            AllArchivedBillsQueryModel model3 = new AllArchivedBillsQueryModel()
            {
                SortingDate = BillsSorting.DateAscending,
                SortingCost = BillsSorting.CostExpensive
            };
            var result3 = await billService.AllBillsAsync(Guest.Id, model3);
            var resultBillId3 = result3.ArchivedBills.Select(x => x.Id).First();
            var expectedBillId3 = _data.Bills.Where(x => x.UserId == Guest.Id && x.IsArchived == true).OrderBy(x => x.Date).ThenByDescending(x => x.Cost).Select(x => x.Id).First();

            Assert.That(resultBillId3, Is.EqualTo(expectedBillId3));

            AllArchivedBillsQueryModel model4 = new AllArchivedBillsQueryModel()
            {
                SortingDate = BillsSorting.DateDescending
            };
            var result4 = await billService.AllBillsAsync(Guest.Id, model4);
            var resultBillId4 = result4.ArchivedBills.Select(x => x.Id).First();
            var expectedBillId4 = _data.Bills.Where(x => x.UserId == Guest.Id && x.IsArchived == true).OrderByDescending(x => x.Date).Select(x => x.Id).First();

            Assert.That(resultBillId4, Is.EqualTo(expectedBillId4));

            AllArchivedBillsQueryModel model5 = new AllArchivedBillsQueryModel()
            {
                SortingDate = BillsSorting.DateDescending,
                SortingCost = BillsSorting.CostCheapest
            };
            var result5 = await billService.AllBillsAsync(Guest.Id, model5);
            var resultBillId5 = result5.ArchivedBills.Select(x => x.Id).First();
            var expectedBillId5 = _data.Bills.Where(x => x.UserId == Guest.Id && x.IsArchived == true).OrderByDescending(x => x.Date).ThenBy(x => x.Cost).Select(x => x.Id).First();

            Assert.That(resultBillId5, Is.EqualTo(expectedBillId5));

            AllArchivedBillsQueryModel model6 = new AllArchivedBillsQueryModel()
            {
                SortingDate = BillsSorting.DateDescending,
                SortingCost = BillsSorting.CostExpensive
            };
            var result6 = await billService.AllBillsAsync(Guest.Id, model6);
            var resultBillId6 = result6.ArchivedBills.Select(x => x.Id).First();
            var expectedBillId6 = _data.Bills.Where(x => x.UserId == Guest.Id && x.IsArchived == true).OrderByDescending(x => x.Date).ThenByDescending(x => x.Cost).Select(x => x.Id).First();

            Assert.That(resultBillId6, Is.EqualTo(expectedBillId6));

            AllArchivedBillsQueryModel model7 = new AllArchivedBillsQueryModel()
            {

                SortingCost = BillsSorting.CostCheapest
            };
            var result7 = await billService.AllBillsAsync(Guest.Id, model7);
            var resultBillId7 = result7.ArchivedBills.Select(x => x.Id).First();
            var expectedBillId7 = _data.Bills.Where(x => x.UserId == Guest.Id && x.IsArchived == true).OrderBy(x => x.Cost).Select(x => x.Id).First();

            Assert.That(resultBillId7, Is.EqualTo(expectedBillId7));

            AllArchivedBillsQueryModel model8 = new AllArchivedBillsQueryModel()
            {

                SortingCost = BillsSorting.CostExpensive
            };
            var result8 = await billService.AllBillsAsync(Guest.Id, model8);
            var resultBillId8 = result8.ArchivedBills.Select(x => x.Id).First();
            var expectedBillId8 = _data.Bills.Where(x => x.UserId == Guest.Id && x.IsArchived == true).OrderByDescending(x => x.Cost).Select(x => x.Id).First();

            Assert.That(resultBillId8, Is.EqualTo(expectedBillId8));
        }

        [Test]
        public async Task AllCurentMonthBills_ShouldReturnCorrectCount()
        {
            var date = _data.Bills.Where(b => b.UserId == Guest.Id && b.IsArchived == false).Select(b => b.Date).First();
            var expectedCount = _data.Bills.Where(b => b.UserId == Guest.Id && b.IsArchived == false).Count();
            var result = await billService.AllCurentMonthBillsAsync(Guest.Id, date);
            var resultBill = result.First();
            Assert.That(result.Count(),Is.EqualTo(expectedCount));
            
        }

        [Test]
        public async Task BillBelongsToUser_ShouldReturnCorrectBool()
        {
            bool isTrue = await billService.BillBelongsToUserAsync(1, Guest.Id);
            bool isFalse = await billService.BillBelongsToUserAsync(1, Guest2.Id);
            Assert.That(isTrue, Is.True);
            Assert.That(isFalse, Is.False);
        }

        [Test]
        public async Task BillExists_ShouldReturnCorrectBool()
        {
            bool isTrue = await billService.BillExistsAsync(1);
            bool isFalse = await billService.BillExistsAsync(100);
            Assert.That(isTrue, Is.True);
            Assert.That(isFalse, Is.False);
        }

        [Test]
        public async Task BillIsPayed_ShouldReturnCorrectBool()
        {
            int payedBillId = _data.Bills.Where(b => b.IsPayed == true).Select(b => b.Id).First();
            int notPayedBillId = _data.Bills.Where(b => b.IsPayed == false).Select(b => b.Id).First();
            bool isTrue = await billService.BillIsPayedAsync(payedBillId);
            bool isFalse = await billService.BillIsPayedAsync(notPayedBillId);
            Assert.That(isTrue, Is.True);
            Assert.That(isFalse, Is.False);
        }

        [Test]
        public async Task CreateBill_ShouldCreateBill()
        {
            BillFormModel model = new BillFormModel()
            {
                BillTypeId = 1,
                Cost = 10,
                PayerId = 1,
            };
            int countBefore = _data.Bills.Count();
            await billService.CreateBillAsync(model, Guest.Id);
            int countAfter = _data.Bills.Count();
            Assert.That(countBefore, Is.LessThan(countAfter));


        }

        [Test]
        public async Task DeleteBillById_ShouldSetDeletedOnOnCorectBill()
        {
            int billId = _data.Bills.Where(b => b.DeletedOn == null).Select(b => b.Id).First();
            var before = _data.Bills.Where(b => b.Id == billId).Select(b => b.DeletedOn).First();
            Assert.That(before, Is.Null);
            await billService.DeleteBillByIdAsync(billId);
            var after = _data.Bills.Where(b => b.Id == billId).Select(b => b.DeletedOn).First();
            Assert.That(after, Is.Not.Null);
        }

        [Test]
        public async Task EditBillById_ShouldEditRightBillWithCorectData()
        {
            var billId = _data.Bills.Where(b => b.IsPayed == false).Select(b => b.Id).First();
            var billTypeBefore = _data.Bills.Where(b=>b.Id==billId).Select(b=>b.BillTypeId);
            var billCostBefore = _data.Bills.Where(b => b.Id == billId).Select(b => b.Cost);
            BillFormModel model = new BillFormModel()
            {
                BillTypeId = 2,
                Cost = 0,
            };
            await billService.EditBillByIdAsync(model, billId);
            var billTypeAfter = _data.Bills.Where(b => b.Id == billId).Select(b => b.BillTypeId);
            var billCostAfter = _data.Bills.Where(b => b.Id == billId).Select(b => b.Cost);
            Assert.That(billTypeBefore,Is.EqualTo(billTypeAfter));
            Assert.That(billCostBefore,Is.EqualTo(billCostAfter));
        }

        [Test]
        public async Task FindBillById_ShouldReturnCorrectBill()
        {
            var expectedBill = _data.Bills.Find(1);
            var resultBill = await billService.FindBillByIdAsync(1);
            Assert.That(resultBill.Cost, Is.EqualTo(expectedBill.Cost));
            Assert.That(resultBill.BillTypeId, Is.EqualTo(expectedBill.BillTypeId));
        }

        [Test]
        public async Task GetAllBillTypes_ShouldReturnAllBillTypes()
        {
            int expectedCount=_data.BillTypes.Where(b=>b.DeletedOn==null&&(b.UserId==null||b.UserId==Guest.Id)).Count();
            var result = await billService.GetBillTypesAsync(Guest.Id);
            int resultCount=result.Count();
            Assert.That(resultCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task PayBill_ShouldPayBill()
        {
            var id=_data.Bills.Where(b=>b.IsPayed==false&&b.UserId==Guest.Id).Select(b=>b.Id).First();
            BillViewModel model = new BillViewModel()
            {
                PayerId = 1,
            };
            await billService.PayBillAsync(model, id);
            var after = _data.Bills.Find(id);
            Assert.That(after.PayerId,Is.EqualTo(1));
            Assert.That(after.IsPayed, Is.True);

        }

        [Test]
        public async Task GetFormatedDate_ShouldReturnCorrectFormat()
        {
            var expected = _data.Bills.Where(b => b.UserId == Guest.Id && b.IsArchived == true).Select(b => b.Date).First().AddMonths(1).ToString("MMMM yyyy");
            var result = await billService.GetFormatedDateAsync(Guest.Id);
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
