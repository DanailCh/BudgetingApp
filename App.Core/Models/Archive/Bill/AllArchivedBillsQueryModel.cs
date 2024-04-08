using App.Core.Enum;
using App.Core.Models.BillType;


namespace App.Core.Models.Archive.Bill
{
    public class AllArchivedBillsQueryModel
    {
        public int BillsPerPage { get; } = Constants.PaginationConstants.BillsPerPage;

        public int? BillTypeId { get; init; }

        public DateTime? BillMonth { get; init; }

        public BillsSorting SortingDate { get; set; } = BillsSorting.None;
        public BillsSorting SortingCost { get; set; } = BillsSorting.None;

        public int CurrentPage { get; init; } = 1;

        public int ArchivedBillsCount { get; set; }

        public IEnumerable<BillTypeFormViewModel> BillTypes { get; set; } = null!;

        public IEnumerable<ArchiveBillViewModel> ArchivedBills { get; set; } = new List<ArchiveBillViewModel>();
    }
}
