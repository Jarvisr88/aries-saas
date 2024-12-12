namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalEnglishNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", "-" };
        private static string[] generalSingles;
        private static string[] teens;
        private static string[] tenths;
        private static string[] hundreds;
        private static string[] thousands;
        private static string[] million;
        private static string[] billion;
        private static string[] trillion;
        private static string[] quadrillion;
        private static string[] quintillion;

        static CardinalEnglishNumericsProvider()
        {
            string[] textArray2 = new string[10];
            textArray2[0] = "one";
            textArray2[1] = "two";
            textArray2[2] = "three";
            textArray2[3] = "four";
            textArray2[4] = "five";
            textArray2[5] = "six";
            textArray2[6] = "seven";
            textArray2[7] = "eight";
            textArray2[8] = "nine";
            textArray2[9] = "zero";
            generalSingles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "ten";
            textArray3[1] = "eleven";
            textArray3[2] = "twelve";
            textArray3[3] = "thirteen";
            textArray3[4] = "fourteen";
            textArray3[5] = "fifteen";
            textArray3[6] = "sixteen";
            textArray3[7] = "seventeen";
            textArray3[8] = "eighteen";
            textArray3[9] = "nineteen";
            teens = textArray3;
            tenths = new string[] { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string[] textArray5 = new string[9];
            textArray5[0] = "one hundred";
            textArray5[1] = "two hundred";
            textArray5[2] = "three hundred";
            textArray5[3] = "four hundred";
            textArray5[4] = "five hundred";
            textArray5[5] = "six hundred";
            textArray5[6] = "seven hundred";
            textArray5[7] = "eight hundred";
            textArray5[8] = "nine hundred";
            hundreds = textArray5;
            thousands = new string[] { "thousand" };
            million = new string[] { "million" };
            billion = new string[] { "billion" };
            trillion = new string[] { "trillion" };
            quadrillion = new string[] { "quadrillion" };
            quintillion = new string[] { "quintillion" };
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            generalSingles;

        public string[] Singles =>
            generalSingles;

        public string[] Teens =>
            teens;

        public string[] Tenths =>
            tenths;

        public string[] Hundreds =>
            hundreds;

        public string[] Thousands =>
            thousands;

        public string[] Million =>
            million;

        public string[] Billion =>
            billion;

        public string[] Trillion =>
            trillion;

        public string[] Quadrillion =>
            quadrillion;

        public string[] Quintillion =>
            quintillion;
    }
}

