using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.HouseholdMember
{
    public class HouseholdMemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
    }
}
