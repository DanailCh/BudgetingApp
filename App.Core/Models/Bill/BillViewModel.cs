using App.Core.Models.HouseholdMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Bill
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public string BillTypeName { get; set; }=String.Empty;
        public decimal Cost { get; set; }
        public bool IsPayed { get; set; }
        public string PayedBy { get; set; }= String.Empty;
        public int PayerId { get; set; }

        public IEnumerable<HouseholdMemberViewModel> Payers { get; set; } = Enumerable.Empty<HouseholdMemberViewModel>();
    }
}
