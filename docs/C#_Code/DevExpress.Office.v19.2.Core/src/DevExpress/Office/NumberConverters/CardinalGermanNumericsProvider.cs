namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalGermanNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { "", "", "und", " " };
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

        static CardinalGermanNumericsProvider()
        {
            string[] textArray2 = new string[11];
            textArray2[0] = "ein";
            textArray2[1] = "zwei";
            textArray2[2] = "drei";
            textArray2[3] = "vier";
            textArray2[4] = "f\x00fcnf";
            textArray2[5] = "sechs";
            textArray2[6] = "sieben";
            textArray2[7] = "acht";
            textArray2[8] = "neun";
            textArray2[9] = "null";
            textArray2[10] = "eine";
            generalSingles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "zehn";
            textArray3[1] = "elf";
            textArray3[2] = "zw\x00f6lf";
            textArray3[3] = "dreizehn";
            textArray3[4] = "vierzehn";
            textArray3[5] = "f\x00fcnfzehn";
            textArray3[6] = "sechzehn";
            textArray3[7] = "siebzehn";
            textArray3[8] = "achtzehn";
            textArray3[9] = "neunzehn";
            teens = textArray3;
            tenths = new string[] { "zwanzig", "drei\x00dfig", "vierzig", "f\x00fcnfzig", "sechzig", "siebzig", "achtzig", "neunzig" };
            string[] textArray5 = new string[9];
            textArray5[0] = "hundert";
            textArray5[1] = "zweihundert";
            textArray5[2] = "dreihundert";
            textArray5[3] = "vierhundert";
            textArray5[4] = "f\x00fcnfhundert";
            textArray5[5] = "sechshundert";
            textArray5[6] = "siebenhundert";
            textArray5[7] = "achthundert";
            textArray5[8] = "neunhundert";
            hundreds = textArray5;
            thousands = new string[] { "tausend" };
            million = new string[] { "million", "millionen" };
            billion = new string[] { "milliarde", "milliarden" };
            trillion = new string[] { "billion", "billionen" };
            quadrillion = new string[] { "billiarde", "billiarden" };
            quintillion = new string[] { "trillion", "trillionen" };
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

