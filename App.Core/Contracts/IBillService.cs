using App.Core.Models.Bill;
using App.Core.Models.BillType;
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
        Task<IEnumerable<BillViewModel>> AllCurentMonthBillsAsync(string userId,DateTime date);
        Task<bool> BillExistsAsync(int id);
        Task<bool> BillBelongsToUserAsync(int id,string userId);
        Task<bool> BillIsPayedAsync(int id);

        Task CreateBillAsync(BillFormModel model,string userId);
        Task <IEnumerable<BillTypeFormViewModel>> GetBillTypesAsync(string userId);

        Task EditBillByIdAsync(BillFormModel model, int id);
        Task<BillFormModel> FindBillByIdAsync(int id);
        Task PayBillAsync(BillViewModel model,int id);
        Task DeleteBillByIdAsync(int id);
        DateTime GetDate();
        string GetFormatedDate();
    }
}
