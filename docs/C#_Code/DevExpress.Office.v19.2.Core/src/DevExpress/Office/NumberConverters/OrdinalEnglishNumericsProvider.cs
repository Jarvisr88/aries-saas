namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalEnglishNumericsProvider : INumericsProvider
    {
        private static string[] generalSingles;
        private static string[] separator;
        private static string[] teens;
        private static string[] tenths;
        private static string[] hundreds;
        private static string[] thousands;
        private static string[] million;
        private static string[] billion;
        private static string[] trillion;
        private static string[] quadrillion;
        private static string[] quintillion;

        static OrdinalEnglishNumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "first";
            textArray1[1] = "second";
            textArray1[2] = "third";
            textArray1[3] = "fourth";
            textArray1[4] = "fifth";
            textArray1[5] = "sixth";
            textArray1[6] = "seventh";
            textArray1[7] = "eighth";
            textArray1[8] = "ninth";
            textArray1[9] = "zeroth";
            generalSingles = textArray1;
            separator = new string[] { " ", "-" };
            string[] textArray3 = new string[10];
            textArray3[0] = "tenth";
            textArray3[1] = "eleventh";
            textArray3[2] = "twelfth";
            textArray3[3] = "thirteenth";
            textArray3[4] = "fourteenth";
            textArray3[5] = "fifteenth";
            textArray3[6] = "sixteenth";
            textArray3[7] = "seventeenth";
            textArray3[8] = "eighteenth";
            textArray3[9] = "nineteenth";
            teens = textArray3;
            tenths = new string[] { "twentieth", "thirtieth", "fortieth", "fiftieth", "sixtieth", "seventieth", "eightieth", "ninetieth" };
            string[] textArray5 = new string[9];
            textArray5[0] = "one hundredth";
            textArray5[1] = "two hundredth";
            textArray5[2] = "three hundredth";
            textArray5[3] = "four hundredth";
            textArray5[4] = "five hundredth";
            textArray5[5] = "six hundredth";
            textArray5[6] = "seven hundredth";
            textArray5[7] = "eight hundredth";
            textArray5[8] = "nine hundredth";
            hundreds = textArray5;
            thousands = new string[] { "thousandth" };
            million = new string[] { "millionth" };
            billion = new string[] { "billionth" };
            trillion = new string[] { "trillionth" };
            quadrillion = new string[] { "quadrillionth" };
            quintillion = new string[] { "quintillionth" };
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

