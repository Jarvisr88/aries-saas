namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalSwedishNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { "", "", " " };
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

        static CardinalSwedishNumericsProvider()
        {
            string[] textArray2 = new string[10];
            textArray2[0] = "et";
            textArray2[1] = "tv\x00e5";
            textArray2[2] = "tre";
            textArray2[3] = "fyra";
            textArray2[4] = "fem";
            textArray2[5] = "sex";
            textArray2[6] = "sju";
            textArray2[7] = "\x00e5tta";
            textArray2[8] = "nio";
            textArray2[9] = "noll";
            singlesNumeral = textArray2;
            string[] textArray3 = new string[11];
            textArray3[0] = "ett";
            textArray3[1] = "tv\x00e5";
            textArray3[2] = "tre";
            textArray3[3] = "fyra";
            textArray3[4] = "fem";
            textArray3[5] = "sex";
            textArray3[6] = "sju";
            textArray3[7] = "\x00e5tta";
            textArray3[8] = "nio";
            textArray3[9] = "noll";
            textArray3[10] = "en";
            singles = textArray3;
            string[] textArray4 = new string[10];
            textArray4[0] = "tio";
            textArray4[1] = "elva";
            textArray4[2] = "tolv";
            textArray4[3] = "tretton";
            textArray4[4] = "fjorton";
            textArray4[5] = "femton";
            textArray4[6] = "sexton";
            textArray4[7] = "sjutton";
            textArray4[8] = "arton";
            textArray4[9] = "nitton";
            teens = textArray4;
            tenths = new string[] { "tjugo", "trettio", "fyrtio", "femtio", "sextio", "sjuttio", "\x00e5ttio", "nittio" };
            string[] textArray6 = new string[9];
            textArray6[0] = "etthundra";
            textArray6[1] = "tv\x00e5hundra";
            textArray6[2] = "trehundra";
            textArray6[3] = "fyrahundra";
            textArray6[4] = "femhundra";
            textArray6[5] = "sexhundra";
            textArray6[6] = "sjuhundra";
            textArray6[7] = "\x00e5ttahundra";
            textArray6[8] = "niohundra";
            hundreds = textArray6;
            thousands = new string[] { "tusen", "tusen" };
            million = new string[] { "miljon", "miljoner" };
            billion = new string[] { "miljard", "miljarder" };
            trillion = new string[] { "biljon", "biljoner" };
            quadrillion = new string[] { "biljard", "biljarder" };
            quintillion = new string[] { "triljon", "triljoner" };
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

