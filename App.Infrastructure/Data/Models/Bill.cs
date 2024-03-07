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
    public class Bill
    {
        [Key]
        [Required]
        [Comment("Identifier for Bill")]
        public int Id { get; set; }

        [Required]
        [Comment("Foreign key for BillType")]
        public int BillTypeId { get; set; }

        [ForeignKey(nameof(BillTypeId))]
        public BillType BillType { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("Cost for Bill")]
        public decimal Cost { get; set; }

        [Required]
        [Comment("For which month and year is the Bill")]
        public DateTime Date { get; set; }

        [Comment("Foreign key for which household member payed the Bill")]
        public int? PayerId { get; set; }

        [ForeignKey(nameof(PayerId))]
        public HouseholdMember? Payer { get; set; }

        [Required]
        [Comment("Foreign key for User")]
        public string UserId { get; set; } = String.Empty;


        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
