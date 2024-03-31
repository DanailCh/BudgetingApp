using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Constants.DataConstants.SeverityType;

namespace App.Infrastructure.Data.Models
{
    public class SeverityType
    {
        [Key]
        [Required]
        [Comment("Identifier for SeverityType")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("SeverityType name")]
        public string Name { get; set; } = String.Empty;

        public ICollection<FeedbackMessage> Messages { get; set; } = new List<FeedbackMessage>();
    }
}
