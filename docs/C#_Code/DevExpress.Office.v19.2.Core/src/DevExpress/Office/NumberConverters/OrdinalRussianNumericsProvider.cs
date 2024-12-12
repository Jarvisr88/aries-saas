namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalRussianNumericsProvider : INumericsProvider
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

        static OrdinalRussianNumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "первый";
            textArray1[1] = "второй";
            textArray1[2] = "третий";
            textArray1[3] = "четвертый";
            textArray1[4] = "пятый";
            textArray1[5] = "шестой";
            textArray1[6] = "седьмой";
            textArray1[7] = "восьмой";
            textArray1[8] = "девятый";
            textArray1[9] = "нулевой";
            generalSingles = textArray1;
            separator = new string[] { " ", " " };
            string[] textArray3 = new string[10];
            textArray3[0] = "десятый";
            textArray3[1] = "одиннадцатый";
            textArray3[2] = "двенадцатый";
            textArray3[3] = "тринадцатый";
            textArray3[4] = "четырнадцатый";
            textArray3[5] = "пятнадцатый";
            textArray3[6] = "шестнадцатый";
            textArray3[7] = "семнадцатый";
            textArray3[8] = "восемнадцатый";
            textArray3[9] = "девятнадцатый";
            teens = textArray3;
            tenths = new string[] { "двадцатый", "тридцатый", "сороковой", "пятидесятый", "шестидесятый", "семидесятый", "восьмидесятый", "девяностый" };
            string[] textArray5 = new string[9];
            textArray5[0] = "сотый";
            textArray5[1] = "двухсотый";
            textArray5[2] = "трехсотый";
            textArray5[3] = "четырехсотый";
            textArray5[4] = "пятисотый";
            textArray5[5] = "шестисотый";
            textArray5[6] = "семисотый";
            textArray5[7] = "восьмисотый";
            textArray5[8] = "девятисотый";
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

