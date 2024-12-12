namespace DevExpress.Office.NumberConverters
{
    using System;

    public class EnglishUKOptionalNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " and " };

        public string[] Separator =>
            separator;

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
            null;

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

