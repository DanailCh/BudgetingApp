using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Archive.Bill
{
    public class ArchiveBillViewModel
    {
        public int Id { get; set; }
        public string BillTypeName { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
    }
}
