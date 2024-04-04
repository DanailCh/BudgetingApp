using App.Core.Models.Bill;
using App.Core.Models.BillType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface IBillTypeService
    {
        Task<IEnumerable<BillTypeViewModel>> AllCustomBillTypesAsync(string userId);

        Task CreateCustomBillTypeAsync(BillTypeFormViewModel model, string userId);

        Task<bool> BillTypeExistsAsync(BillTypeFormViewModel model, string userId);



        Task DeleteCustomBillTypeByIdAsync(int id);
    }
}
