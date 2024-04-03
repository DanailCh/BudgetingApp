using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Constants.DataConstants.Status;

namespace App.Infrastructure.Data.Models
{
    public class Status
    {
        [Key]
        [Required]
        [Comment("Identifier for Status")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Status name")]
        public string Name { get; set; } = String.Empty;

        public ICollection<FeedbackMessage> Messages { get; set; } = new List<FeedbackMessage>();
    }
}
