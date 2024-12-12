namespace DevExpress.Office.NumberConverters
{
    using System;

    public class ThousandRussianNumericsProvider : INumericsProvider
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

        static ThousandRussianNumericsProvider()
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "одно";
            textArray1[1] = "двух";
            textArray1[2] = "трех";
            textArray1[3] = "четырех";
            textArray1[4] = "пяти";
            textArray1[5] = "шести";
            textArray1[6] = "семи";
            textArray1[7] = "восьми";
            textArray1[8] = "девяти";
            generalSingles = textArray1;
            separator = new string[] { "", "" };
            string[] textArray3 = new string[10];
            textArray3[0] = "десяти";
            textArray3[1] = "одиннадцати";
            textArray3[2] = "двенадцати";
            textArray3[3] = "тринадцати";
            textArray3[4] = "четырнадцати";
            textArray3[5] = "пятнадцати";
            textArray3[6] = "шестнадцати";
            textArray3[7] = "семнадцати";
            textArray3[8] = "восемнадцати";
            textArray3[9] = "девятнадцати";
            teens = textArray3;
            tenths = new string[] { "двадцати", "тридцати", "сороко", "пятидесяти", "шестидесяти", "семидесяти", "восьмидесяти", "девяносто" };
            string[] textArray5 = new string[9];
            textArray5[0] = "сто";
            textArray5[1] = "двухсот";
            textArray5[2] = "трехсот";
            textArray5[3] = "четырехсот";
            textArray5[4] = "пятисот";
            textArray5[5] = "шестисот";
            textArray5[6] = "семисот";
            textArray5[7] = "восьмисот";
            textArray5[8] = "девятисот";
            hundreds = textArray5;
            thousands = new string[] { "тысячный" };
            million = new string[] { "миллионный" };
            billion = new string[] { "миллиардный" };
            trillion = new string[] { "триллионный" };
            quadrillion = new string[] { "квадриллионный" };
            quintillion = new string[] { "квинтиллионный" };
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

