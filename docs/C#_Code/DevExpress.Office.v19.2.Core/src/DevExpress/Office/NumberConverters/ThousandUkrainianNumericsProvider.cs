namespace DevExpress.Office.NumberConverters
{
    using System;

    public class ThousandUkrainianNumericsProvider : INumericsProvider
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

        static ThousandUkrainianNumericsProvider()
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "одно";
            textArray1[1] = "двох";
            textArray1[2] = "трьох";
            textArray1[3] = "чотирьох";
            textArray1[4] = "п'яти";
            textArray1[5] = "шести";
            textArray1[6] = "семи";
            textArray1[7] = "восьми";
            textArray1[8] = "дев'яти";
            generalSingles = textArray1;
            separator = new string[] { "", "" };
            string[] textArray3 = new string[10];
            textArray3[0] = "десяти";
            textArray3[1] = "одинадцяти";
            textArray3[2] = "двенадцяти";
            textArray3[3] = "тринадцяти";
            textArray3[4] = "чотирнадцяти";
            textArray3[5] = "п'ятнадцяти";
            textArray3[6] = "шістнадцяти";
            textArray3[7] = "сімнадцяти";
            textArray3[8] = "вісімнадцяти";
            textArray3[9] = "дев'ятнадцяти";
            teens = textArray3;
            tenths = new string[] { "двадцяти", "тридцяти", "сорока", "п'ятдесяти", "шістдесяти", "сімдесяти", "вісімдесяти", "дев'яносто" };
            string[] textArray5 = new string[9];
            textArray5[0] = "сто";
            textArray5[1] = "двохсот";
            textArray5[2] = "трьохсот";
            textArray5[3] = "чотирьохсот";
            textArray5[4] = "п'ятисот";
            textArray5[5] = "шестисот";
            textArray5[6] = "семисот";
            textArray5[7] = "восьмисот";
            textArray5[8] = "дев'ятисот";
            hundreds = textArray5;
            string[] textArray6 = new string[10];
            textArray6[0] = "тисячний";
            textArray6[1] = "тисячний";
            textArray6[2] = "тисячний";
            textArray6[3] = "тисячний";
            textArray6[4] = "тисячний";
            textArray6[5] = "тисячний";
            textArray6[6] = "тисячний";
            textArray6[7] = "тисячний";
            textArray6[8] = "тисячний";
            textArray6[9] = "тисячний";
            thousands = textArray6;
            million = new string[] { "мільйонний" };
            billion = new string[] { "мільярдний" };
            trillion = new string[] { "трильйонний" };
            quadrillion = new string[] { "квадрильйонний" };
            quintillion = new string[] { "квінтильйонний" };
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

