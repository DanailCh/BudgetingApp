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
    public class MemberSalary
    {
        [Key]
        [Required]
        [Comment("Identifier for Member Salary")]
        public int Id { get; set; }

        [Required]
        [Comment("Foreign key for Household member")]
        public int HouseholdMemberId { get; set; }

        [ForeignKey(nameof(HouseholdMemberId))]
        public HouseholdMember HouseholdMember { get; set; } = null!;

        [Required]
        [Comment("Date for Member salary")]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Comment("Salary for Member Salary")]
        public decimal Salary { get; set; }

        [Required]
        [Comment("Foreign key for User")]
        public string UserId { get; set; } = String.Empty;


        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
