namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalFrenchNumericsProvider : INumericsProvider
    {
        internal static string[] separator = new string[] { " ", "-", " et ", "" };
        private static string[] generalSingles;
        internal static string[] teens;
        internal static string[] tenths;
        private static string[] hundreds;
        internal static string[] thousands;
        internal static string[] million;
        internal static string[] billion;
        internal static string[] trillion;
        internal static string[] quadrillion;
        internal static string[] quintillion;

        static CardinalFrenchNumericsProvider()
        {
            string[] textArray2 = new string[11];
            textArray2[0] = "un";
            textArray2[1] = "deux";
            textArray2[2] = "trois";
            textArray2[3] = "quatre";
            textArray2[4] = "cinq";
            textArray2[5] = "six";
            textArray2[6] = "sept";
            textArray2[7] = "huit";
            textArray2[8] = "neuf";
            textArray2[9] = "z\x00e9ro";
            textArray2[10] = "s";
            generalSingles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "dix";
            textArray3[1] = "onze";
            textArray3[2] = "douze";
            textArray3[3] = "treize";
            textArray3[4] = "quatorze";
            textArray3[5] = "quinze";
            textArray3[6] = "seize";
            textArray3[7] = "dix-sept";
            textArray3[8] = "dix-huit";
            textArray3[9] = "dix-neuf";
            teens = textArray3;
            tenths = new string[] { "vingt", "trente", "quarante", "cinquante", "soixante", "soixante", "quatre-vingt", "quatre-vingt" };
            string[] textArray5 = new string[9];
            textArray5[0] = "cent";
            textArray5[1] = "deux cent";
            textArray5[2] = "trois cent";
            textArray5[3] = "quatre cent";
            textArray5[4] = "cinq cent";
            textArray5[5] = "six cent";
            textArray5[6] = "sept cent";
            textArray5[7] = "huit cent";
            textArray5[8] = "neuf cent";
            hundreds = textArray5;
            thousands = new string[] { "mille" };
            million = new string[] { "million", "millions" };
            billion = new string[] { "milliard", "milliards" };
            trillion = new string[] { "billion", "billions" };
            quadrillion = new string[] { "billiard", "billiards" };
            quintillion = new string[] { "trillion", "trillions" };
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

