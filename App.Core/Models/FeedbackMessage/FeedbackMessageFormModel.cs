using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Core.Constants.ErrorMessagesConstants;
using static App.Infrastructure.Constants.DataConstants.Messages;

namespace App.Core.Models.FeedbackMessage
{
    public class FeedbackMessageFormModel
    {
        [Required(ErrorMessage =RequiredMessage)]
        [StringLength(TitleMaxLength,
           MinimumLength = TitleMinLength,
           ErrorMessage = LengthMessage)]
        public string Title { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(ContentMaxLength,
           MinimumLength = ContentMinLength,
           ErrorMessage = LengthMessage)]
        public string Content { get; set; }  
    }
}
