using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data.Models
{
    public class HouseholdBudget
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Income { get; set; }

        [Required]
        public decimal Expences { get; set; }

        [Required]
        [Comment("Identifier for User")]
        public string UserId { get; set; } = String.Empty;


        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
