using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.Bill
{
    public class BillFormModel
    {
        public int Id { get; set; }
        public int BillTypeId { get; set; }
        public decimal Cost { get; set; }
        public int PayerId { get; set; }

    }
}
