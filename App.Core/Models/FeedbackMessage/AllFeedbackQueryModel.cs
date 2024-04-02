using App.Core.Enum;
using App.Core.Models.Archive.Bill;
using App.Core.Models.BillType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.FeedbackMessage
{
    public class AllFeedbackQueryModel
    {
        public int MessagesPerPage { get; } = Constants.FeedbackMessageConstants.MessagesPerPage;

        public int SeverityTypeId { get; set; }
        public int StatusId { get; set; }

        public MessageSorting Sorting { get; init; } = MessageSorting.None;
        
        public int CurrentPage { get; init; } = 1;

        public int MessagesCount { get; set; }

        public IEnumerable<SeverityTypeViewModel> SeverityTypes { get; set; } = null!;
        public IEnumerable<StatusViewModel> Statuses { get; set; } = null!;

        public IEnumerable<FeedbackMessageFormViewModel> Messages { get; set; } = new List<FeedbackMessageFormViewModel>();
    }
}
