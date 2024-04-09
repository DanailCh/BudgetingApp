using App.Core.Contracts;
using App.Core.Models.BillType;
using App.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Test.UnitTests
{
    public class BillTypeTests:UnitTestsBase
    {
        private IBillTypeService billTypeService;

        [OneTimeSetUp]
        public void SetUp() => billTypeService = new BillTypeService(_data);

        [Test]
        public async Task AllCustomBillTypes_ShouldReturnOnlyCustomTypes()
        {
            BillTypeFormViewModel model = new BillTypeFormViewModel()
            {
                Name = "custom",
            };
            await billTypeService.CreateCustomBillTypeAsync(model, Guest.Id);
            BillTypeFormViewModel model2 = new BillTypeFormViewModel()
            {
                Name = "custom2",
            };
            await billTypeService.CreateCustomBillTypeAsync(model2, Guest.Id);
            int expectedCount=_data.BillTypes.Where(b=>b.UserId==Guest.Id).Count();

            var result = await billTypeService.AllCustomBillTypesAsync(Guest.Id);
            Assert.That(result.Count(),Is.EqualTo(expectedCount));

        }

        [Test]
        public async Task BillTypeWithNameAlreadyExists_ShouldReturnCorrectBool()
        {
            BillTypeFormViewModel model = new BillTypeFormViewModel()
            {
                Name = "custom",
            };
            await billTypeService.CreateCustomBillTypeAsync(model, Guest.Id);

            BillTypeFormViewModel model2 = new BillTypeFormViewModel()
            {
                Name = "Custom",
            };
            BillTypeFormViewModel model3 = new BillTypeFormViewModel()
            {
                Name = "Different",
            };
            bool IsTrue=await billTypeService.BillTypeWhitNameAlreadyExistsAsync(model2,Guest.Id);
            bool IsFalse = await billTypeService.BillTypeWhitNameAlreadyExistsAsync(model3, Guest.Id);
            Assert.That(IsTrue,Is.True);
            Assert.That(IsFalse,Is.False);
        }

        [Test]
        public async Task DeleteCustomBillType_ShouldDeleteCorrectBillType()
        {
            BillTypeFormViewModel model = new BillTypeFormViewModel()
            {
                Name = "custom",
            };
            await billTypeService.CreateCustomBillTypeAsync(model, Guest.Id);
            DateTime? before = _data.BillTypes.Where(b => b.UserId == Guest.Id).Select(b => b.DeletedOn).First();
            Assert.That(before, Is.Null);
            int id = _data.BillTypes.Where(b => b.UserId == Guest.Id).Select(b => b.Id).First();
            await billTypeService.DeleteCustomBillTypeByIdAsync(id);
            DateTime? after = _data.BillTypes.Where(b => b.UserId == Guest.Id).Select(b => b.DeletedOn).First();
            Assert.That(after,Is.Not.Null);
        }
    }
}
