namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalItalianNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { "", "", " ", " e " };
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

        static CardinalItalianNumericsProvider()
        {
            string[] textArray2 = new string[11];
            textArray2[0] = "uno";
            textArray2[1] = "due";
            textArray2[2] = "tre";
            textArray2[3] = "quattro";
            textArray2[4] = "cinque";
            textArray2[5] = "sei";
            textArray2[6] = "sette";
            textArray2[7] = "otto";
            textArray2[8] = "nove";
            textArray2[9] = "zero";
            textArray2[10] = "un";
            generalSingles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "dieci";
            textArray3[1] = "undici";
            textArray3[2] = "dodici";
            textArray3[3] = "tredici";
            textArray3[4] = "quattordici";
            textArray3[5] = "quindici";
            textArray3[6] = "sedici";
            textArray3[7] = "diciassette";
            textArray3[8] = "diciotto";
            textArray3[9] = "diciannove";
            teens = textArray3;
            tenths = new string[] { "venti", "trenta", "quaranta", "cinquanta", "sessanta", "settanta", "ottanta", "novanta" };
            string[] textArray5 = new string[9];
            textArray5[0] = "cento";
            textArray5[1] = "duecento";
            textArray5[2] = "trecento";
            textArray5[3] = "quattrocento";
            textArray5[4] = "cinquecento";
            textArray5[5] = "seicento";
            textArray5[6] = "settecento";
            textArray5[7] = "ottocento";
            textArray5[8] = "novecento";
            hundreds = textArray5;
            thousands = new string[] { "mila", "mille" };
            million = new string[] { "milione", "milioni" };
            billion = new string[] { "miliardo", "miliardi" };
            trillion = new string[] { "bilione", "bilioni" };
            quadrillion = new string[] { "biliardo", "biliardi" };
            quintillion = new string[] { "trilione", "trilioni" };
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

