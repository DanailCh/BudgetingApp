using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Constants.DataConstants.BillType;

namespace App.Infrastructure.Data.Models
{
    public class BillType
    {
        [Key]
        [Required]
        [Comment("Identifier for BillType")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("BillType name")]
        public string Name { get; set; }=String.Empty;

       
        [Comment("Foreign key for User created BillTypes.Default BillTypes will not have UserId")]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser? User { get; set; }

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

        [Comment("Date of deletion")]
        public DateTime? DeletedOn { get; set; }

    }
}
