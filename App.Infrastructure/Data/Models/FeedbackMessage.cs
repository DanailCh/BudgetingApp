using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Constants.DataConstants.Messages;

namespace App.Infrastructure.Data.Models
{
    public class FeedbackMessage
    {
        [Key]
        [Required]
        [Comment("Identifier for Message")]
        public int Id { get; set; }

        [Required]
        [Comment("Foreign key for User who send the message")]
        public string SenderId { get; set; } = String.Empty;


        [ForeignKey(nameof(SenderId))]
        public IdentityUser SenderUser { get; set; } = null!;

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Title for Message")]
        public string Title { get; set; }= String.Empty;

        [Required]
        [MaxLength(ContentMaxLength)]
        [Comment("Content of Message")]
        public string Content { get; set; } = String.Empty;

        

        [Required]
        [Comment("Timestamp for when the message was send")]
        public DateTime Date { get; set; }

        
        [Comment("Foreign key for SeverityType")]
        public int? SeverityTypeId { get; set; }

        [ForeignKey(nameof(SeverityTypeId))]
        public SeverityType? SeverityType { get; set; } = null!;


        
        [MaxLength(CommentMaxLength)]
        [Comment("Comment from Admin")]
        public string? Comment { get; set; } = String.Empty;

        [Required]
        [Comment("Status for message")]       
        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
    }
}
