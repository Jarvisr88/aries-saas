namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalGermanNumericsProvider : INumericsProvider
    {
        internal static string[] generalSingles;
        internal static string[] separator;
        internal static string[] teens;
        internal static string[] tenths;
        private static string[] hundreds;
        internal static string[] thousands;
        internal static string[] million;
        internal static string[] billion;
        internal static string[] trillion;
        internal static string[] quadrillion;
        internal static string[] quintillion;

        static OrdinalGermanNumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "erste";
            textArray1[1] = "zweite";
            textArray1[2] = "dritte";
            textArray1[3] = "vierte";
            textArray1[4] = "f\x00fcnfte";
            textArray1[5] = "sechste";
            textArray1[6] = "siebente";
            textArray1[7] = "achte";
            textArray1[8] = "neunte";
            textArray1[9] = "nullte";
            generalSingles = textArray1;
            separator = new string[] { "", "", "und" };
            string[] textArray3 = new string[10];
            textArray3[0] = "zehnte";
            textArray3[1] = "elfte";
            textArray3[2] = "zw\x00f6lfte";
            textArray3[3] = "dreizehnte";
            textArray3[4] = "vierzehnte";
            textArray3[5] = "f\x00fcnfzehnte";
            textArray3[6] = "sechzehnte";
            textArray3[7] = "siebzehnte";
            textArray3[8] = "achtzehnte";
            textArray3[9] = "neunzehnte";
            teens = textArray3;
            tenths = new string[] { "zwanzigste", "drei\x00dfigste", "vierzigste", "f\x00fcnfzigste", "sechzigste", "siebzigste", "achtzigste", "neunzigste" };
            string[] textArray5 = new string[9];
            textArray5[0] = "hundertste";
            textArray5[1] = "zweihundertste";
            textArray5[2] = "dreihundertste";
            textArray5[3] = "vierhundertste";
            textArray5[4] = "f\x00fcnfhundertste";
            textArray5[5] = "sechshundertste";
            textArray5[6] = "siebenhundertste";
            textArray5[7] = "achthundertste";
            textArray5[8] = "neunhundertste";
            hundreds = textArray5;
            thousands = new string[] { "tausendste" };
            million = new string[] { "millionste", "millionste" };
            billion = new string[] { "milliardeste", "milliardeste" };
            trillion = new string[] { "billionste", "milliardeste" };
            quadrillion = new string[] { "billiardeste", "billiardeste" };
            quintillion = new string[] { "trillionste", "trillionste" };
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

