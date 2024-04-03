using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Core.Constants.ErrorMessagesConstants;
using static App.Infrastructure.Constants.DataConstants.HouseholdMember;

namespace App.Core.Models.HouseholdMember
{
    public class HouseholdMemberFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(NameMaxLength,
           MinimumLength = NameMinLength,
           ErrorMessage = LengthMessage)]
        public string Name { get; set; } = String.Empty;
    }
}
