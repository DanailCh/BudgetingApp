using App.Core.Models.Archive.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.FeedbackMessage
{
    public class FeedbackQueryModel
    {
        public int MessagesCount { get; set; }

        public IEnumerable<FeedbackMessageFormViewModel> Messages { get; set; } = new List<FeedbackMessageFormViewModel>();
    }
}
