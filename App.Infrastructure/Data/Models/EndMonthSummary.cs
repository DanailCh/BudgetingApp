using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Constants.DataConstants.EndMonthSummary;


namespace App.Infrastructure.Data.Models
{
    public class EndMonthSummary
    {
        [Key]
        [Required]
        [Comment("Identifier for Summary")]
        public int Id { get; set; }

        [Required]
        [Comment("For which month and year is the Summary")]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(SummaryMaxLength)]
        [Comment("Summary content")]
        public string Summary { get; set; } = String.Empty;

        [Required]       
        public bool IsResolved { get; set; }

        [Required]
        [Comment("Foreign key for User")]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser? User { get; set; }
    }
}
