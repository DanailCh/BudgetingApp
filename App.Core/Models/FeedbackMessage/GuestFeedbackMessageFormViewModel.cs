using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Models.FeedbackMessage
{
    public class GuestFeedbackMessageFormViewModel
    {
        public IEnumerable<FeedbackMessageViewModel> FeedbackMessages { get; set; }=new List<FeedbackMessageViewModel>();
        public FeedbackMessageFormModel Model { get; set; }=new FeedbackMessageFormModel();
    }
}
