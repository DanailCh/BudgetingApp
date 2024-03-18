using App.Core.Models.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface IBillService
    {
        Task<IEnumerable<BillViewModel>> AllBillsAsync(string userId);

        Task CreateBillAsync(BillFormModel model);

        Task EditBillByIdAsync(BillFormModel model, int id);
        Task DeleteBillByIdAsync(int id);
    }
}
