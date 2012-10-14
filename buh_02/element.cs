using System;

namespace buh_02
{
    static class element
    {
        public static string InOut { get; set; }
        public static string Category { get; set; }
        public static DateTime Date { get; set; }
        public static double Sum { get; set; }
        public static string Comment { get; set; }
    }

    static class DateBeginEnd
    {
        public static DateTime DateBegin { get; set; }
        public static DateTime DateEnd { get; set; }
    }

    static class Backup
    {
        public static Boolean Enable { get; set; }
        public static string Dir { get; set; }
        public static Decimal Counter { get; set; }
    }
}
