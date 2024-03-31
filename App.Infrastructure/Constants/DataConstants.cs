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
            public const int NameMaxLength = 50;
            public const int NameMinLength = 3;
        }
        public static class EndMonthSummary
        {
            public const int SummaryMaxLength = 300;            
        }
        public static class Messages
        {
            public const int TitleMaxLength = 50;
            public const int ContentMaxLength = 300;
            public const int CommentMaxLength = 300;
        }
        public static class SeverityType
        {
            public const int NameMaxLength = 15;           
        }
    }
}
