using App.Core.Contracts;
using App.Core.Enum;
using App.Core.Models.Archive.MemberSalary;
using App.Core.Models.FeedbackMessage;
using App.Core.Models.HouseholdMember;
using App.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Test.UnitTests
{
    public class HouseholdTests:UnitTestsBase
    {
        private IHouseholdService householdService;

        [OneTimeSetUp]
        public void SetUp() => householdService = new HouseholdService(_data);

        [Test]
        public async Task AllHouseholdMembers_ShouldReturnAllMembersForCorrectUser()
        {
            var expectedCount = _data.HouseholdMembers.Where(x => x.UserId == Guest.Id).Count();            
            var result = await householdService.AllHouseholdMembersAsync(Guest.Id);
            Assert.That(result.Count(),Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task AllMembersSalaries_ShouldReturnCorrectCount()
        {
            AllArchivedMembersSalariesQueryModel model = new AllArchivedMembersSalariesQueryModel();
            var result = await householdService.AllMembersSalariesAsync(Guest.Id,model);
            int expected=_data.MemberSalaries.Where(x=> x.UserId == Guest.Id).Count();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ArchivedMembersSalariesCount, Is.EqualTo(expected));
            Assert.That(result.ArchivedSalaries.Count, Is.EqualTo(expected));
            Assert.That(result.ArchivedSalariesToDownload.Count, Is.EqualTo(expected));

            var date =DateTime.Now;
            AllArchivedMembersSalariesQueryModel model2 = new AllArchivedMembersSalariesQueryModel()
            {
                SalariesMonth = date,
                MemberId = "2",
                CurrentPage = 1,
                
            };

            var result2 = await householdService.AllMembersSalariesAsync(Guest.Id,model2);
            var expected2=_data.MemberSalaries.Where(x=> x.UserId == Guest.Id&&x.Date==date && x.HouseholdMemberId == 2).Count();
            Assert.That(result2, Is.Not.Null);
            Assert.That(result2.ArchivedMembersSalariesCount, Is.EqualTo(expected2));
            Assert.That(result2.ArchivedSalaries.Count, Is.EqualTo(expected2));

          

            AllArchivedMembersSalariesQueryModel model3 = new AllArchivedMembersSalariesQueryModel()
            {
                MemberId = "AllInactiveMembers",               
            };

            var result3 = await householdService.AllMembersSalariesAsync(Guest.Id, model3);
            var expected3 = _data.MemberSalaries.Join(_data.HouseholdMembers,
                           s => s.HouseholdMemberId,
                           m => m.Id,
                           (s, m) => new { Salary = s, Deleted = m.DeletedOn })
                           .Where(salary => salary.Deleted != null).Select(sa => sa.Salary).Count();
            Assert.That(result3, Is.Not.Null);
            Assert.That(result3.ArchivedMembersSalariesCount, Is.EqualTo(expected3));
            Assert.That(result3.ArchivedSalaries.Count, Is.EqualTo(expected3));

            AllArchivedMembersSalariesQueryModel model4 = new AllArchivedMembersSalariesQueryModel()
            {
                MemberId = "OnlyActiveMembers",
            };

            var result4 = await householdService.AllMembersSalariesAsync(Guest.Id, model4);
            var expected4 = _data.MemberSalaries.Join(_data.HouseholdMembers,
                           s => s.HouseholdMemberId,
                           m => m.Id,
                           (s, m) => new { Salary = s, Deleted = m.DeletedOn })
                           .Where(salary => salary.Deleted == null).Select(sa => sa.Salary).Count();
            Assert.That(result4, Is.Not.Null);
            Assert.That(result4.ArchivedMembersSalariesCount, Is.EqualTo(expected4));
            Assert.That(result4.ArchivedSalaries.Count, Is.EqualTo(expected4));
        }
        [Test]
        public async Task AllMembersSalaries_ShouldReturnInCorrectOrder()
        {
            AllArchivedMembersSalariesQueryModel model = new AllArchivedMembersSalariesQueryModel()
            {
                MemberId = "OnlyActiveMembers",
                Sorting= SalariesSorting.HighestFirst
            };

            var result = await householdService.AllMembersSalariesAsync(Guest.Id, model);
            var resultFirst = result.ArchivedSalaries.First();
            var expectedFirst = _data.MemberSalaries.Join(_data.HouseholdMembers,
                           s => s.HouseholdMemberId,
                           m => m.Id,
                           (s, m) => new { Salary = s, Deleted = m.DeletedOn })
                           .Where(salary => salary.Deleted == null).Select(sa => sa.Salary).OrderByDescending(s=>s.Salary).First();
            Assert.That(resultFirst, Is.Not.Null);
            Assert.That(resultFirst.Salary, Is.EqualTo(expectedFirst.Salary));

            AllArchivedMembersSalariesQueryModel model2 = new AllArchivedMembersSalariesQueryModel()
            {
                MemberId = "OnlyActiveMembers",
                Sorting = SalariesSorting.LowestFirst
            };

            var result2 = await householdService.AllMembersSalariesAsync(Guest.Id, model2);
            var resultFirst2 = result2.ArchivedSalaries.First();
            var expectedFirst2 = _data.MemberSalaries.Join(_data.HouseholdMembers,
                           s => s.HouseholdMemberId,
                           m => m.Id,
                           (s, m) => new { Salary = s, Deleted = m.DeletedOn })
                           .Where(salary => salary.Deleted == null).Select(sa => sa.Salary).OrderBy(s => s.Salary).First();
            Assert.That(resultFirst2, Is.Not.Null);
            Assert.That(resultFirst2.Salary, Is.EqualTo(expectedFirst2.Salary));
        }

        [Test]
        public async Task CreateHouseholdMember_ShouldCreateMemberWithName()
        {
            var expectedName = "TestName";
            var input = new HouseholdMemberFormViewModel()
            {
                Name = expectedName,
            };
            int memberCountBefore = _data.HouseholdMembers.Where(m => m.UserId == Guest.Id).Count();
            await householdService.CreateHouseholdMemberAsync(input, Guest.Id);
            int memberCountAfter = _data.HouseholdMembers.Where(m => m.UserId == Guest.Id).Count();
            var membersAfter = _data.HouseholdMembers.Where(m => m.UserId == Guest.Id).ToList();
            bool isCreated=membersAfter.Any(m=>m.Name==expectedName);
            Assert.That(memberCountBefore,Is.LessThan(memberCountAfter));
            Assert.IsTrue(isCreated);
        }
        [Test]
        public async Task DeleteHouseholdMemberById_ShouldSetDeletedOnAndChangeName()
        {
            string nameBefore=_data.HouseholdMembers.Where(m=>m.UserId==Guest.Id&&m.Id==1).Select(m=>m.Name).First();
            DateTime? deletedOnBefore = _data.HouseholdMembers.Where(m => m.UserId == Guest.Id && m.Id == 1).Select(m => m.DeletedOn).First();
            Assert.That(deletedOnBefore, Is.Null);
            await householdService.DeleteHouseholdMemberByIdAsync(1);
            string nameAfter = _data.HouseholdMembers.Where(m => m.UserId == Guest.Id && m.Id == 1).Select(m => m.Name).First();
            DateTime? deletedOnAfter = _data.HouseholdMembers.Where(m => m.UserId == Guest.Id && m.Id == 1).Select(m => m.DeletedOn).First();
            Assert.That(nameBefore,Is.Not.EqualTo(nameAfter));
            Assert.That(deletedOnAfter,Is.Not.Null);
        }

        [Test]
        public async Task MemberBelongsToUser_ShouldReturnCorrectBool()
        {
            bool IsTrue = await householdService.MemberBelongsToUserAsync(1, Guest.Id);
            bool IsFalse=await householdService.MemberBelongsToUserAsync(1, Guest2.Id);
            Assert.That(IsTrue, Is.True);
            Assert.That(IsFalse, Is.False);
        }

        [Test]
        public async Task MemberExists_ShouldReturnCorrectBool()
        {
            bool IsTrue = await householdService.MemberExistsAsync(1);
            bool IsFalse = await householdService.MemberExistsAsync(10);
            Assert.That(IsTrue, Is.True);
            Assert.That(IsFalse, Is.False);
        }

        [Test]
        public async Task UnderMinimumMembers_ShouldReturnCorrectBool()
        {
            bool isFalse = await householdService.UnderMinimumMembersAsync(Guest2.Id);
            Assert.That(isFalse, Is.False);
        }


        [Test]
        public async Task OverMembersLimit_ShouldReturnCorrectBool()
        {
            bool isFalse = await householdService.OverMembersLimitAsync(Guest.Id);
            Assert.That(isFalse, Is.False);

            var input = new HouseholdMemberFormViewModel()
            {
                Name = "name",
            };
            await householdService.CreateHouseholdMemberAsync(input,Guest.Id);
            bool isTrue = await householdService.OverMembersLimitAsync(Guest.Id);
            Assert.That(isTrue, Is.True);

        }
    }
}
