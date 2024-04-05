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
        [Comment("Identifier for household budget")]
        public int Id { get; set; }

        [Required]
        [Comment("Date for Household Budget")]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("Monthly Income of the Household")]
        public decimal Income { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("Monthly Expences of the Household")]
        public decimal Expences { get; set; }

        [Required]
        [Comment("Foreign key for User")]
        public string UserId { get; set; } = String.Empty;


        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
