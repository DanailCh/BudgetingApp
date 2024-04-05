using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Constants.DataConstants.HouseholdMember;

namespace App.Infrastructure.Data.Models
{
    public class HouseholdMember
    {
        [Key]
        [Required]
        [Comment("Identifier for household member")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Name of the household member")]
        public string Name { get; set; }=String.Empty;

        [Required]
        [Comment("Foreign key for User")]
        public string UserId { get; set; }= String.Empty;


        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

        public ICollection<MemberSalary> MemberSalaries { get; set; } = new List<MemberSalary>();

        [Comment("Date of deletion")]
        public DateTime? DeletedOn { get; set; }

    }
}
