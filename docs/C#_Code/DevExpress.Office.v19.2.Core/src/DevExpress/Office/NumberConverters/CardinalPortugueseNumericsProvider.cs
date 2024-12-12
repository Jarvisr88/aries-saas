namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalPortugueseNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", " e ", "" };
        private static string[] singlesNumeral;
        private static string[] generalSingles;
        private static string[] teens;
        private static string[] tenths;
        private static string[] hundreads;
        private static string[] thousands;
        private static string[] million;
        private static string[] billion;
        private static string[] trillion;
        private static string[] quadrillion;
        private static string[] quintillion;

        static CardinalPortugueseNumericsProvider()
        {
            string[] textArray2 = new string[9];
            textArray2[0] = "um";
            textArray2[1] = "dois";
            textArray2[2] = "tr\x00eas";
            textArray2[3] = "quatro";
            textArray2[4] = "cinco";
            textArray2[5] = "seis";
            textArray2[6] = "sete";
            textArray2[7] = "oito";
            textArray2[8] = "nove";
            singlesNumeral = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "um";
            textArray3[1] = "dois";
            textArray3[2] = "tr\x00eas";
            textArray3[3] = "quatro";
            textArray3[4] = "cinco";
            textArray3[5] = "seis";
            textArray3[6] = "sete";
            textArray3[7] = "oito";
            textArray3[8] = "nove";
            textArray3[9] = "zero";
            generalSingles = textArray3;
            string[] textArray4 = new string[10];
            textArray4[0] = "dez";
            textArray4[1] = "onze";
            textArray4[2] = "doze";
            textArray4[3] = "treze";
            textArray4[4] = "quatorze";
            textArray4[5] = "quinze";
            textArray4[6] = "dezesseis";
            textArray4[7] = "dezessete";
            textArray4[8] = "dezoito";
            textArray4[9] = "dezenove";
            teens = textArray4;
            tenths = new string[] { "vinte", "trinta", "quarenta", "cinq\x00fcenta", "sessenta", "setenta", "oitenta", "noventa" };
            string[] textArray6 = new string[10];
            textArray6[0] = "cento";
            textArray6[1] = "duzentos";
            textArray6[2] = "trezentos";
            textArray6[3] = "quatrocentos";
            textArray6[4] = "quinhentos";
            textArray6[5] = "seiscentos";
            textArray6[6] = "setecentos";
            textArray6[7] = "oitocentos";
            textArray6[8] = "novecentos";
            textArray6[9] = "cem";
            hundreads = textArray6;
            thousands = new string[] { "mil" };
            million = new string[] { "milh\x00e3o", "milh\x00f5es" };
            billion = new string[] { "mil milh\x00f5es" };
            trillion = new string[] { "bili\x00e3o", "bili\x00f5es" };
            quadrillion = new string[] { "mil bili\x00f5es" };
            quintillion = new string[] { "trili\x00e3o", "trili\x00f5es" };
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            singlesNumeral;

        public string[] Singles =>
            generalSingles;

        public string[] Teens =>
            teens;

        public string[] Tenths =>
            tenths;

        public string[] Hundreds =>
            hundreads;

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

