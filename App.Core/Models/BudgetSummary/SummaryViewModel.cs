using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.BudgetSummary
{
    public class SummaryViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; }=string.Empty;
       public bool IsResolved { get; set; }
    }
}
