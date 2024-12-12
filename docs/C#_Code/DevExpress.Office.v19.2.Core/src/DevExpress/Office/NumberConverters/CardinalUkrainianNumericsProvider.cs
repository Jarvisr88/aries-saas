namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalUkrainianNumericsProvider : INumericsProvider
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

        static CardinalUkrainianNumericsProvider()
        {
            string[] textArray1 = new string[9];
            textArray1[0] = "одна";
            textArray1[1] = "дві";
            textArray1[2] = "три";
            textArray1[3] = "чотири";
            textArray1[4] = "п'ять";
            textArray1[5] = "шість";
            textArray1[6] = "сім";
            textArray1[7] = "вісім";
            textArray1[8] = "дев'ять";
            singleNumeral = textArray1;
            string[] textArray2 = new string[10];
            textArray2[0] = "один";
            textArray2[1] = "два";
            textArray2[2] = "три";
            textArray2[3] = "чотири";
            textArray2[4] = "п'ять";
            textArray2[5] = "шість";
            textArray2[6] = "сім";
            textArray2[7] = "вісім";
            textArray2[8] = "дев'ять";
            textArray2[9] = "нуль";
            singles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "десять";
            textArray3[1] = "одинадцять";
            textArray3[2] = "дванадцять";
            textArray3[3] = "тринадцять";
            textArray3[4] = "чотирнадцять";
            textArray3[5] = "п'ятнадцять";
            textArray3[6] = "шістнадцять";
            textArray3[7] = "сімнадцять";
            textArray3[8] = "вісімнадцять";
            textArray3[9] = "дев'ятнадцять";
            teens = textArray3;
            tenths = new string[] { "двадцять", "тридцять", "сорок", "п'ятдесят", "шістдесят", "сімдесят", "вісімдесят", "дев'яносто" };
            string[] textArray5 = new string[9];
            textArray5[0] = "сто";
            textArray5[1] = "двісті";
            textArray5[2] = "триста";
            textArray5[3] = "чотириста";
            textArray5[4] = "п'ятсот";
            textArray5[5] = "шістсот";
            textArray5[6] = "сімсот";
            textArray5[7] = "вісімсот";
            textArray5[8] = "дев'ятсот";
            hundreds = textArray5;
            thousands = new string[] { "тисяча", "тисячі", "тисяч" };
            million = new string[] { "мільйон", "мільйона", "мільйонов" };
            billion = new string[] { "мільярд", "мільярда", "мільярдов" };
            trillion = new string[] { "трильйон", "трильйона", "трильйонов" };
            quadrillion = new string[] { "квадрильйон", "квадрильйона", "квадрильйонов" };
            quintillion = new string[] { "квінтильйон", "квінтильйона", "квінтильйонов" };
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

