namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalThaiOptionalNumericsProvider : INumericsProvider
    {
        private static string[] thousands = new string[] { "หมื่น", "แสน", "พัน" };

        public string[] Separator =>
            null;

        public string[] SinglesNumeral =>
            null;

        public string[] Singles =>
            null;

        public string[] Teens =>
            null;

        public string[] Tenths =>
            null;

        public string[] Hundreds =>
            null;

        public string[] Thousands =>
            thousands;

        public string[] Million =>
            null;

        public string[] Billion =>
            null;

        public string[] Trillion =>
            null;

        public string[] Quadrillion =>
            null;

        public string[] Quintillion =>
            null;
    }
}

