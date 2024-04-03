using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Constants.DataConstants.MemberSalary;
using static App.Core.Constants.ErrorMessagesConstants;

namespace App.Core.Models.BudgetSummary
{
    public class MemberSalaryFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Range(typeof(decimal),
            CostMin,
            CostMax,
            ConvertValueInInvariantCulture = true,
            ErrorMessage = CostMessage)]        
        public decimal Salary { get; set; }
    }
}
