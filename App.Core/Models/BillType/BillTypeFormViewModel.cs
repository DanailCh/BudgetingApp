using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Core.Constants.ErrorMessagesConstants;
using static App.Infrastructure.Constants.DataConstants.BillType;

namespace App.Core.Models.BillType
{
    public class BillTypeFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage =RequiredMessage)]
        [StringLength(NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = LengthMessage)]
        public string Name { get; set; } = String.Empty;

        public IEnumerable<BillTypeViewModel> CustomBillTypes { get; set; } = new List<BillTypeViewModel>();
    }
}
