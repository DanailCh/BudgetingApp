using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Models.BillType;
using App.Core.Models.HouseholdMember;
using static App.Infrastructure.Constants.DataConstants.Bill;
using static App.Core.Constants.ErrorMessagesConstants;
using System.ComponentModel;

namespace App.Core.Models.Bill
{
    public class BillFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [DisplayName("Bill Type")]
        public int BillTypeId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Range(typeof(decimal),
           CostMin,
           CostMax,
           ConvertValueInInvariantCulture = true,
           ErrorMessage = CostMessage)]        
        public decimal Cost { get; set; }

        [DisplayName("Payed By")]
        public int? PayerId { get; set; }

        public IEnumerable<BillTypeFormViewModel> BillTypes { get; set; } = Enumerable.Empty<BillTypeFormViewModel>();
        public IEnumerable<HouseholdMemberFormViewModel> Payers { get; set; } = Enumerable.Empty<HouseholdMemberFormViewModel>();

    }
}
