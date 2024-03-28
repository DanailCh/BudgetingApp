using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Constants
{
    public static class DataConstants
    {
        public static class BilType
        {
            public const int NameMaxLength = 15;
            public const int NameMinLength = 3;
        }
        public static class HouseholdMember
        {
            public const int MaximumMembers = 5;
            public const int NameMaxLength = 50;
            public const int NameMinLength = 3;
        }
        public static class EndMonthSummary
        {
            public const int SummaryMaxLength = 300;            
        }
    }
}
