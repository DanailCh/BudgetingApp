using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Constants
{
    public class TempDataErrorMessagesConstants
    {
        public const string BillEditNotAlowedMessage = "Cannot edit details of a bill that has already been paid.";

        public const string OverMemberLimitMessage = "Household Member Limit reached";

        public const string NotEnoughMembersMessage = "You need to have at least two members";

        public const string NoBillsMessage = "No Bills to Summarize";

        public const string BillsNotPayedMessage = "All Bills must be payed in order to Summarize month!";

        public const string IncomeCantBeZeroMessage = "Houshold Income must not be 0!";
    }
}
