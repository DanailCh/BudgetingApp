using App.Core.Contracts;
using App.Core.Models.Archive.Bill;
using App.Core.Models.Archive.HouseholdBudget;
using App.Core.Models.Archive.MemberSalary;
using App.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Test.UnitTests
{
    [TestFixture]
    public class FileGeneratorTests
    {
        private IFileGeneratorService fileGeneratorService;

        [OneTimeSetUp]
        public void SetUp() => fileGeneratorService = new FileGeneratorService();

        [Test]
        public void GenerateFileForArchiveBills_ShouldGenerateText()
        {
            var input = new ArchiveBillViewModel[]{new ArchiveBillViewModel()
            {
                BillTypeName = "Name",
                Date = DateTime.Now,
                Cost = 0,
            } };
            string result = fileGeneratorService.GenerateFileForArchivedBills(input);
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void GenerateFileForArchiveBudgets_ShouldGenerateText()
        {
            var input = new ArchiveHouseholdBudgetViewModel[]{new ArchiveHouseholdBudgetViewModel()
            {
               Date = DateTime.Now,
               Income=1,
               Expences=1,
            } };
            string result = fileGeneratorService.GenerateFileForArchivedBudgets(input);
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void GenerateFileForArchiveSalaries_ShouldGenerateText()
        {
            var input = new ArchiveMemberSalaryViewModel[]{new ArchiveMemberSalaryViewModel()
            {
               Date = DateTime.Now,
               Name="name",
               Salary=1,
            } };
            string result = fileGeneratorService.GenerateFileForArchivedSalaries(input);
            Assert.That(result, Is.Not.Null);
        }
    }
}
