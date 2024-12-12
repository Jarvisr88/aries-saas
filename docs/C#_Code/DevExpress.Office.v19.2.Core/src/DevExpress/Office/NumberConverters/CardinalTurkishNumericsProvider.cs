namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalTurkishNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { "", "", " " };
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

        static CardinalTurkishNumericsProvider()
        {
            string[] textArray2 = new string[10];
            textArray2[0] = "bir";
            textArray2[1] = "iki";
            textArray2[2] = "\x00fc\x00e7";
            textArray2[3] = "d\x00f6rt";
            textArray2[4] = "beş";
            textArray2[5] = "altı";
            textArray2[6] = "yedi";
            textArray2[7] = "sekiz";
            textArray2[8] = "dokuz";
            textArray2[9] = "sıfır";
            generalSingles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "on";
            textArray3[1] = "onbir";
            textArray3[2] = "oniki";
            textArray3[3] = "on\x00fc\x00e7";
            textArray3[4] = "ond\x00f6rt";
            textArray3[5] = "onbeş";
            textArray3[6] = "onaltı";
            textArray3[7] = "onyedi";
            textArray3[8] = "onsekiz";
            textArray3[9] = "ondokuz";
            teens = textArray3;
            tenths = new string[] { "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };
            string[] textArray5 = new string[9];
            textArray5[0] = "y\x00fcz";
            textArray5[1] = "ikiy\x00fcz";
            textArray5[2] = "\x00fc\x00e7y\x00fcz";
            textArray5[3] = "d\x00f6rty\x00fcz";
            textArray5[4] = "beşy\x00fcz";
            textArray5[5] = "altıy\x00fcz";
            textArray5[6] = "yediy\x00fcz";
            textArray5[7] = "sekizy\x00fcz";
            textArray5[8] = "dokuzy\x00fcz";
            hundreds = textArray5;
            thousands = new string[] { "bin", "bin" };
            million = new string[] { "milyon", "milyon" };
            billion = new string[] { "milyar", "milyar" };
            trillion = new string[] { "trilyon", "trilyon" };
            quadrillion = new string[] { "katrilyon", "katrilyon" };
            quintillion = new string[] { "kentilyon", "kentilyon" };
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

