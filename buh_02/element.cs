using System;

namespace buh_02
{
    static class element
    {
        public static Boolean BudgetCheck { get; set; }
        public static String InOut { get; set; }
        public static String Category { get; set; }
        public static DateTime Date { get; set; }
        public static Double Sum { get; set; }
        public static String Comment { get; set; }
    }

    static class DateBeginEnd
    {
        public static DateTime DateBegin { get; set; }
        public static DateTime DateEnd { get; set; }
    }

    static class Backup
    {
        public static Boolean Enable { get; set; }
        public static String Dir { get; set; }
        public static Decimal Counter { get; set; }
    }
}
