using System;

namespace ArxBuh
{
    static class Class_element
    {
        public static Boolean BudgetCheck { get; set; }
        public static String InOut { get; set; }
        public static String Category { get; set; }
        public static DateTime Date { get; set; }
        public static Double Sum { get; set; }
        public static String Comment { get; set; }
    }

    static class arxDs
    {
        public static DataSet1 ds { get; set; }
    }

    static class DateBeginEnd
   {
        public static DateTime DateBegin { get; set; }
        public static DateTime DateEnd { get; set; }
    }

    static class Goal
    {
        public static Int32 History { get; set; }
    }
}
