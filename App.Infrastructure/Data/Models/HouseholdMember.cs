using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
        [Comment("Identifier for User")]
        public string UserId { get; set; }= String.Empty;


        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

    }
}
