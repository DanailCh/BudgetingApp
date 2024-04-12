using App.Core.Contracts;
using App.Core.Enum;
using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.Bill;
using App.Core.Models.BudgetSummary;
using App.Core.Models.HouseholdMember;
using App.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Test.UnitTests
{
    public class BudgetSummaryTests:UnitTestsBase
    {
        private IBudgetSummaryService budgetSummaryService;

        [OneTimeSetUp]
        public void SetUp()
        {
            string dateStr = "2024-05-01 00:00:00.0000000";
            DateTime date = DateTime.ParseExact(dateStr, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
            List<MemberSalaryFormViewModel> model= new List<MemberSalaryFormViewModel>();
            string result = "Success";
            Task<string> str = Task.FromResult(result);
            var mockSummaryLogicService=new Mock<ISummaryLogicService>();
            mockSummaryLogicService
                .Setup(s => s.GetSummaryAsync(model, Guest2.Id, date))
                .Returns(str);
            mockSummaryLogicService
                .Setup(s => s.ArchiveBillsAsync(Guest2.Id));  
            

            IEnumerable<BillViewModel> list2 = new List<BillViewModel>();
            IEnumerable<BillViewModel> list1 = new List<BillViewModel>() { new BillViewModel(), new BillViewModel() };
            Task<DateTime> date2 = Task.FromResult(date);
            Task<IEnumerable<BillViewModel>> model2 = Task.FromResult(list2);
            Task<IEnumerable<BillViewModel>> model1 = Task.FromResult(list1);

            var mockBillService =new Mock<IBillService>();
            mockBillService
                .Setup(b => b.GetDateAsync(Guest2.Id))
                .Returns(date2);
            mockBillService
                .Setup(b => b.AllCurentMonthBillsAsync(Guest2.Id, date))
                .Returns(model2);
            mockBillService
                .Setup(b => b.GetDateAsync(Guest.Id))
                .Returns(date2);
            mockBillService
                .Setup(b => b.AllCurentMonthBillsAsync(Guest.Id, date))
                .Returns(model1);

            budgetSummaryService = new BudgetSummaryService(_data,mockSummaryLogicService.Object,mockBillService.Object);
        }

        [Test]
        public async Task NotAllBillsPayed_SouldReturnCorrectBool()
        {
            bool IsTrue = await budgetSummaryService.NotAllBillsPayedAsync(Guest.Id);
            bool IsFalse = await budgetSummaryService.NotAllBillsPayedAsync(Guest2.Id); 
            Assert.IsTrue(IsTrue);
            Assert.IsFalse(false);
        }

        [Test]
        public async Task AllSummaries_ShouldReturnCorrectCount()
        {
            var expected=_data.EndMonthSummaries.Where(s=>s.UserId==Guest.Id);
            var expectedSummary = expected.First();
            var result = await budgetSummaryService.AllSummariesAsync(Guest.Id);
            var resultSummary = result.First();
            Assert.That(result.Count, Is.EqualTo(expected.Count()));
            Assert.That(resultSummary.Summary, Is.EqualTo(expectedSummary.Summary));
            Assert.That(resultSummary.IsResolved, Is.EqualTo(expectedSummary.IsResolved));
            Assert.That(resultSummary.Id, Is.EqualTo(expectedSummary.Id));
            Assert.That(resultSummary.Date, Is.EqualTo(expectedSummary.Date.ToString("MMMM yyyy")));
        }

        [Test]
        public async Task CreateSummary_ShouldCreateSummary()
        {
            string dateStr = "2024-05-01 00:00:00.0000000";
            DateTime date = DateTime.ParseExact(dateStr, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
            List < MemberSalaryFormViewModel > model = new List<MemberSalaryFormViewModel>();

            int summariesCountBefore = _data.EndMonthSummaries.Where(s => s.UserId == Guest2.Id).Count();
            await budgetSummaryService.CreateSummary(model, Guest2.Id);
            int summariesCountAfter = _data.EndMonthSummaries.Where(s => s.UserId == Guest2.Id).Count();
            var summary = _data.EndMonthSummaries.Where(s => s.UserId == Guest2.Id).First();

            Assert.That(summariesCountBefore,Is.LessThan(summariesCountAfter));
            Assert.That(summary.IsResolved,Is.False);
            Assert.That(summary.Summary, Is.EqualTo("Success"));
            Assert.That(summary.Date,Is.EqualTo(date));
            
        }

        [Test]
        public async Task GetMemberSalaryFormModels_ShouldReturnCorrectMemberCountAndData()
        {
           
            var expected = _data.HouseholdMembers.Where(m => m.UserId == Guest2.Id).ToList();
            var expectedMember = expected.First();

            var result=await budgetSummaryService.GetMemberSalaryFormModelsAsync(Guest2.Id);
            var resultMember = result.First();

            Assert.That(result.Count, Is.EqualTo(expected.Count));
            Assert.That(resultMember.Id, Is.EqualTo(expectedMember.Id));
            Assert.That(resultMember.Name, Is.EqualTo(expectedMember.Name));
            Assert.That(resultMember.Salary, Is.EqualTo(0));

        }

        [Test]
        public async Task ResolveSummary_ShouldResolveCorrectSummary()
        {
            int summaryId=_data.EndMonthSummaries.Where(s=>s.IsResolved==false&&s.UserId==Guest.Id).Select(s=>s.Id).FirstOrDefault();
            Assert.That(summaryId,Is.Not.Zero);
            
            await budgetSummaryService.ResolveSummary(summaryId);
            var summaryAfter = _data.EndMonthSummaries.Find(summaryId);
            Assert.That(summaryAfter, Is.Not.Null);
            Assert.That(summaryAfter.IsResolved,Is.True);
        }

        [Test]
        public async Task AllBudgets_ShouldReturnCorrectCount()
        {
            string dateString = "2024-04-01 00:00:00.0000000";
            DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss.fffffff", System.Globalization.CultureInfo.InvariantCulture);
            var model = new AllArchivedBudgetsQueryModel()
            {
                BudgetMonth =date,
                CurrentPage = 1,
            };

            var expected = _data.HouseholdBudgets.Where(b => b.UserId == Guest.Id && b.Date == date).ToList();
            var result=await budgetSummaryService.AllBudgetsAsync(Guest.Id, model);
            Assert.That(expected, Is.Not.Null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ArchivedBudgetsCount, Is.EqualTo(expected.Count));
            Assert.That(result.ArchivedBudgets.Count(), Is.EqualTo(expected.Count));
            Assert.That(result.ArchivedBudgetsToDownload.Count(), Is.EqualTo(expected.Count));
        }

        [Test]
        public async Task AllBudgets_ShouldReturnCorrectOrder()
        {
            
            var model = new AllArchivedBudgetsQueryModel()
            {
               Sorting=BudgetSorting.MostIncome,
            };

            var expectedFirst = _data.HouseholdBudgets.Where(b => b.UserId == Guest.Id).OrderByDescending(b => b.Income).First();
            var result = await budgetSummaryService.AllBudgetsAsync(Guest.Id, model);
            var resultFirst=result.ArchivedBudgets.First();
           
            Assert.That(resultFirst.Id, Is.EqualTo(expectedFirst.Id));
            Assert.That(resultFirst.Income, Is.EqualTo(expectedFirst.Income));
            Assert.That(resultFirst.Expences, Is.EqualTo(expectedFirst.Expences));
            Assert.That(resultFirst.Date, Is.EqualTo(expectedFirst.Date));

            var model2 = new AllArchivedBudgetsQueryModel()
            {
                Sorting = BudgetSorting.LeastIncome,
            };

            var expectedFirst2 = _data.HouseholdBudgets.Where(b => b.UserId == Guest.Id).OrderBy(b => b.Income).First();
            var result2 = await budgetSummaryService.AllBudgetsAsync(Guest.Id, model2);
            var resultFirst2 = result2.ArchivedBudgets.First();

            Assert.That(resultFirst2.Id, Is.EqualTo(expectedFirst2.Id));
            Assert.That(resultFirst2.Income, Is.EqualTo(expectedFirst2.Income));
            Assert.That(resultFirst2.Expences, Is.EqualTo(expectedFirst2.Expences));
            Assert.That(resultFirst2.Date, Is.EqualTo(expectedFirst2.Date));

            var model3 = new AllArchivedBudgetsQueryModel()
            {
                Sorting = BudgetSorting.MostExpences,
            };

            var expectedFirst3 = _data.HouseholdBudgets.Where(b => b.UserId == Guest.Id).OrderByDescending(b => b.Expences).First();
            var result3 = await budgetSummaryService.AllBudgetsAsync(Guest.Id, model3);
            var resultFirst3 = result3.ArchivedBudgets.First();

            Assert.That(resultFirst3.Id, Is.EqualTo(expectedFirst3.Id));
            Assert.That(resultFirst3.Income, Is.EqualTo(expectedFirst3.Income));
            Assert.That(resultFirst3.Expences, Is.EqualTo(expectedFirst3.Expences));
            Assert.That(resultFirst3.Date, Is.EqualTo(expectedFirst3.Date));

            var model4 = new AllArchivedBudgetsQueryModel()
            {
                Sorting = BudgetSorting.LeastExpences,
            };

            var expectedFirst4 = _data.HouseholdBudgets.Where(b => b.UserId == Guest.Id).OrderBy(b => b.Expences).First();
            var result4 = await budgetSummaryService.AllBudgetsAsync(Guest.Id, model4);
            var resultFirst4 = result4.ArchivedBudgets.First();

            Assert.That(resultFirst4.Id, Is.EqualTo(expectedFirst4.Id));
            Assert.That(resultFirst4.Income, Is.EqualTo(expectedFirst4.Income));
            Assert.That(resultFirst4.Expences, Is.EqualTo(expectedFirst4.Expences));
            Assert.That(resultFirst4.Date, Is.EqualTo(expectedFirst4.Date));

        }

        [Test]
        public async Task HasBills_ShouldReturnCorrectBool()
        {
            bool IsTrue=await budgetSummaryService.HasBillsAsync(Guest.Id);
            bool IsFalse=await budgetSummaryService.HasBillsAsync(Guest2.Id);
            Assert.IsTrue(IsTrue);
            Assert.IsFalse(IsFalse);
        }

        [Test]
        public void  HouseholdIncomeIsZero_ShouldReturnCorrectBool()
        {
            var inputForTrueResult=new List<MemberSalaryFormViewModel>() { new MemberSalaryFormViewModel() { Salary = 0.00M } , new MemberSalaryFormViewModel() { Salary = 0.00M } };
            var inputForFalseResult = new List<MemberSalaryFormViewModel>() { new MemberSalaryFormViewModel() { Salary = 10.00M }, new MemberSalaryFormViewModel() { Salary = 0.00M } };

            bool IsTrue=budgetSummaryService.HouseholdIncomeIsZero(inputForTrueResult);
            bool IsFalse=budgetSummaryService.HouseholdIncomeIsZero(inputForFalseResult);
            Assert.IsTrue(IsTrue);
            Assert.IsFalse(IsFalse);
        }
    }
}
