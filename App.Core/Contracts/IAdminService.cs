using App.Core.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminViewModel>> AllAdminsAsync();
        Task DeleteAdminAsync(string adminId);
        Task<bool> AdminExistsAsync(string adminId);
    }
}
