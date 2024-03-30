using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Archive.Bill
{
    public class ArchiveBillQueryModel
    {
        public int ArchivedBillsCount { get; set; }

        public IEnumerable<ArchiveBillViewModel> ArchivedBills { get; set; } = new List<ArchiveBillViewModel>();
    }
}
