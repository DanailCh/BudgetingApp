using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Models.BillType;
using App.Core.Models.HouseholdMember;

namespace App.Core.Models.Bill
{
    public class BillFormModel
    {
       
        public int BillTypeId { get; set; }
        public decimal Cost { get; set; }
        public int PayerId { get; set; }
        public DateTime Date { get; set; }
       

        public IEnumerable<BillTypeViewModel> BillTypes { get; set; } = Enumerable.Empty<BillTypeViewModel>();
        public IEnumerable<HouseholdMemberViewModel> Payers { get; set; } = Enumerable.Empty<HouseholdMemberViewModel>();

    }
}
