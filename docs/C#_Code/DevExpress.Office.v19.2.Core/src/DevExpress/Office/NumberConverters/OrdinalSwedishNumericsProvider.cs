namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalSwedishNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { "", "" };
        private static string[] singlesNumeral;
        private static string[] singles;
        private static string[] teens;
        private static string[] tenths;
        private static string[] hundreds;
        private static string[] thousands;
        private static string[] million;
        private static string[] billion;
        private static string[] trillion;
        private static string[] quadrillion;
        private static string[] quintillion;

        static OrdinalSwedishNumericsProvider()
        {
            string[] textArray2 = new string[9];
            textArray2[0] = "f\x00f6rsta";
            textArray2[1] = "andra";
            textArray2[2] = "tredje";
            textArray2[3] = "fj\x00e4rde";
            textArray2[4] = "femte";
            textArray2[5] = "sj\x00e4tte";
            textArray2[6] = "sjunde";
            textArray2[7] = "\x00e5ttonde";
            textArray2[8] = "nionde";
            singlesNumeral = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "f\x00f6rsta";
            textArray3[1] = "andra";
            textArray3[2] = "tredje";
            textArray3[3] = "fj\x00e4rde";
            textArray3[4] = "femte";
            textArray3[5] = "sj\x00e4tte";
            textArray3[6] = "sjunde";
            textArray3[7] = "\x00e5ttonde";
            textArray3[8] = "nionde";
            textArray3[9] = "nollte";
            singles = textArray3;
            string[] textArray4 = new string[10];
            textArray4[0] = "tionde";
            textArray4[1] = "elfte";
            textArray4[2] = "tolfte";
            textArray4[3] = "trettonde";
            textArray4[4] = "fjortonde";
            textArray4[5] = "femtonde";
            textArray4[6] = "sextonde";
            textArray4[7] = "sjuttonde";
            textArray4[8] = "artonde";
            textArray4[9] = "nittonde";
            teens = textArray4;
            tenths = new string[] { "tjugonde", "trettionde", "fyrtionde", "femtionde", "sextionde", "sjuttionde", "\x00e5ttionde", "nittionde" };
            string[] textArray6 = new string[9];
            textArray6[0] = "etthundrade";
            textArray6[1] = "tv\x00e5hundrade";
            textArray6[2] = "trehundrade";
            textArray6[3] = "fyrahundrade";
            textArray6[4] = "femhundrade";
            textArray6[5] = "sexhundrade";
            textArray6[6] = "sjuhundrade";
            textArray6[7] = "\x00e5ttahundrade";
            textArray6[8] = "niohundrade";
            hundreds = textArray6;
            thousands = new string[] { "tusende", "tusende" };
            million = new string[] { "miljonte", "miljonte" };
            billion = new string[] { "miljardte", "miljardte" };
            trillion = new string[] { "biljonte", "biljonte" };
            quadrillion = new string[] { "biljardte", "biljardte" };
            quintillion = new string[] { "triljonte", "triljonte" };
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            singlesNumeral;

        public string[] Singles =>
            singles;

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

