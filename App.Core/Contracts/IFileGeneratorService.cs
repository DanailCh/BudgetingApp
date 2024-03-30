using App.Core.Models.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Contracts
{
    public interface IFileGeneratorService
    {
        Task<string> GenerateFileForArchivedBills(string userId, IEnumerable<ArchiveBillViewModel> model);
    }
}
