namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalRussianNumericsProvider : INumericsProvider
    {
        private static string[] singleNumeral;
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

        static CardinalRussianNumericsProvider()
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "одна";
            textArray1[1] = "две";
            textArray1[2] = "три";
            textArray1[3] = "четыре";
            textArray1[4] = "пять";
            textArray1[5] = "шесть";
            textArray1[6] = "семь";
            textArray1[7] = "восемь";
            textArray1[8] = "девять";
            singleNumeral = textArray1;
            string[] textArray2 = new string[10];
            textArray2[0] = "один";
            textArray2[1] = "два";
            textArray2[2] = "три";
            textArray2[3] = "четыре";
            textArray2[4] = "пять";
            textArray2[5] = "шесть";
            textArray2[6] = "семь";
            textArray2[7] = "восемь";
            textArray2[8] = "девять";
            textArray2[9] = "ноль";
            singles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "десять";
            textArray3[1] = "одиннадцать";
            textArray3[2] = "двенадцать";
            textArray3[3] = "тринадцать";
            textArray3[4] = "четырнадцать";
            textArray3[5] = "пятнадцать";
            textArray3[6] = "шестнадцать";
            textArray3[7] = "семнадцать";
            textArray3[8] = "восемнадцать";
            textArray3[9] = "девятнадцать";
            teens = textArray3;
            tenths = new string[] { "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
            string[] textArray5 = new string[9];
            textArray5[0] = "сто";
            textArray5[1] = "двести";
            textArray5[2] = "триста";
            textArray5[3] = "четыреста";
            textArray5[4] = "пятьсот";
            textArray5[5] = "шестьсот";
            textArray5[6] = "семьсот";
            textArray5[7] = "восемьсот";
            textArray5[8] = "девятьсот";
            hundreds = textArray5;
            thousands = new string[] { "тысяча", "тысячи", "тысяч" };
            million = new string[] { "миллион", "миллиона", "миллионов" };
            billion = new string[] { "миллиард", "миллиарда", "миллиардов" };
            trillion = new string[] { "триллион", "триллиона", "триллионов" };
            quadrillion = new string[] { "квадриллион", "квадриллиона", "квадриллионов" };
            quintillion = new string[] { "квинтиллион", "квинтиллиона", "квинтиллионов" };
        }

        private static string[] separator =>
            new string[] { " ", " " };

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            singleNumeral;

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

