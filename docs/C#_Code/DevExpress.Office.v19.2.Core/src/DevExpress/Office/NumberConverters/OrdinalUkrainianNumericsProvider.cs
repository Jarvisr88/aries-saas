namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalUkrainianNumericsProvider : INumericsProvider
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

        static OrdinalUkrainianNumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "перший";
            textArray1[1] = "другий";
            textArray1[2] = "третій";
            textArray1[3] = "четвертий";
            textArray1[4] = "п'ятий";
            textArray1[5] = "шостий";
            textArray1[6] = "сьомий";
            textArray1[7] = "восьмий";
            textArray1[8] = "дев'ятий";
            textArray1[9] = "нульовий";
            generalSingles = textArray1;
            separator = new string[] { " ", " " };
            string[] textArray3 = new string[10];
            textArray3[0] = "десятий";
            textArray3[1] = "одинадцятий";
            textArray3[2] = "дванадцятий";
            textArray3[3] = "тринадцятий";
            textArray3[4] = "чотирнадцятий";
            textArray3[5] = "п'ятнадцятий";
            textArray3[6] = "шістнадцятий";
            textArray3[7] = "сімнадцятий";
            textArray3[8] = "вісімнадцятий";
            textArray3[9] = "дев'ятнадцятий";
            teens = textArray3;
            tenths = new string[] { "двадцятий", "тридцятий", "сороковий", "п'ятдесятий", "шістдесятий", "сімдесятий", "вісімдесятий", "дев'яностий" };
            string[] textArray5 = new string[9];
            textArray5[0] = "сотий";
            textArray5[1] = "двохсотий";
            textArray5[2] = "трьохсотий";
            textArray5[3] = "чотирьохсотий";
            textArray5[4] = "п'ятисотий";
            textArray5[5] = "шестисотий";
            textArray5[6] = "семисотий";
            textArray5[7] = "восьмисотий";
            textArray5[8] = "дев'ятисотий";
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

