using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Constants.DataConstants.BilType;

namespace App.Infrastructure.Data.Models
{
    public class BillType
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("BillType name")]
        public string Name { get; set; }=String.Empty;

        [Required]
        [Comment("Identifier for User created BillTypes.Default BillTypes will not have UserId")]
        public string UserId { get; set; } = String.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser? User { get; set; }

    }
}
