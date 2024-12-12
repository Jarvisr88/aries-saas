namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalItalianOptionalNumericsProvider : INumericsProvider
    {
        private static string[] generalSingles;
        private static string[] separator;
        private static string[] tenths;
        private static string[] hundreds;

        static CardinalItalianOptionalNumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "uno";
            textArray1[1] = "due";
            textArray1[2] = "tr\x00e9";
            textArray1[3] = "quattro";
            textArray1[4] = "cinque";
            textArray1[5] = "sei";
            textArray1[6] = "sette";
            textArray1[7] = "otto";
            textArray1[8] = "nove";
            textArray1[9] = "zero";
            generalSingles = textArray1;
            separator = new string[] { "", "", " " };
            tenths = new string[] { "vent", "trent", "quarant", "cinquant", "sessant", "settant", "ottant", "novant" };
            string[] textArray4 = new string[9];
            textArray4[0] = "cent";
            textArray4[1] = "duecent";
            textArray4[2] = "trecent";
            textArray4[3] = "quattrocent";
            textArray4[4] = "cinquecent";
            textArray4[5] = "seicent";
            textArray4[6] = "settecent";
            textArray4[7] = "ottocent";
            textArray4[8] = "novecent";
            hundreds = textArray4;
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            generalSingles;

        public string[] Singles =>
            generalSingles;

        public string[] Teens =>
            null;

        public string[] Tenths =>
            tenths;

        public string[] Hundreds =>
            hundreds;

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

