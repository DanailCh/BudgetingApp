using App.Core.Enum;
using App.Core.Models.BillType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App.Core.Models.Bill
{
    public  class AllArchivedBillsQueryModel
    {
        public int BillsPerPage { get; } = App.Core.Constants.ArchiveConstants.BillsPerPage;

        public int BillTypeId { get; init; }

        public DateTime? BillMonth { get; init; }
        
        public BillsSorting SortingDate { get; init; } = BillsSorting.None;
        public BillsSorting SortingCost { get; init; } = BillsSorting.None;

        public int CurrentPage { get; init; } = 1;

        public int ArchivedBillsCount { get; set; }

        public IEnumerable<BillTypeFormViewModel> BillTypes { get; set; } = null!;

        public IEnumerable<ArchiveBillViewModel> ArchivedBills { get; set; } = new List<ArchiveBillViewModel>();
    }
}
