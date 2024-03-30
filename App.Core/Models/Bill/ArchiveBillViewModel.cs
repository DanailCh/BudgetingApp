using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Bill
{
    public class ArchiveBillViewModel
    {
        public int Id { get; set; }
        public string BillTypeName { get; set; } = String.Empty;
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
    }
}
