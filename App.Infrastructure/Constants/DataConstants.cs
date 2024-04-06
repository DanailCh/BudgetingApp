using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Constants
{
    public static class DataConstants
    {
        public static class Bill
        {
            public const string CostMax = "10000000";
            public const string CostMin = "0";
        }
        public static class BillType
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
            public const int SummaryMaxLength = 3000;            
        }
        public static class Messages
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 50;
            public const int ContentMinLength = 10;
            public const int ContentMaxLength = 1000;
            public const int CommentMaxLength = 300;
        }
        public static class SeverityType
        {
            public const int NameMaxLength = 15;           
        }
        public static class Status
        {
            public const int NameMaxLength = 20;
        }
        public static class MemberSalary
        {
            public const string CostMax = "100000000";
            public const string CostMin = "0";
        }
    }
}
