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
        public int Id { get; set; }

        [Required]
        public int BillTypeId { get; set; }

        [ForeignKey(nameof(BillTypeId))]
        public BillType BillType { get; set; } = null!;

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int PayerId { get; set; }

        [ForeignKey(nameof(PayerId))]
        public HouseholdMember? Payer { get; set; }

        [Required]
        [Comment("Identifier for User")]
        public string UserId { get; set; } = String.Empty;


        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
