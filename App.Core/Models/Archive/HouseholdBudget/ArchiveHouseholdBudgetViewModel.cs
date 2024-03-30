using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Archive.HouseholdBudget
{
    public class ArchiveHouseholdBudgetViewModel
    {
        public int Id { get; set; }
        public DateTime Date{ get; set; }
        public decimal Income { get; set; }
        public decimal Expences { get; set; }
    }
}
