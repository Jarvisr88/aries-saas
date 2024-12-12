namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalPortugueseOptionalNumericProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", " e ", "" };
        private static string[] singlesNumeral;
        private static string[] teens;
        private static string[] tenths;
        private static string[] hundreds;

        static OrdinalPortugueseOptionalNumericProvider()
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
            textArray3[0] = "dez";
            textArray3[1] = "onze";
            textArray3[2] = "doze";
            textArray3[3] = "treze";
            textArray3[4] = "quatorze";
            textArray3[5] = "quinze";
            textArray3[6] = "dezesseis";
            textArray3[7] = "dezessete";
            textArray3[8] = "dezoito";
            textArray3[9] = "dezenove";
            teens = textArray3;
            tenths = new string[] { "vinte", "trinta", "quarenta", "cinq\x00fcenta", "sessenta", "setenta", "oitenta", "noventa" };
            string[] textArray5 = new string[10];
            textArray5[0] = "cento";
            textArray5[1] = "duzentos";
            textArray5[2] = "trezentos";
            textArray5[3] = "quatrocentos";
            textArray5[4] = "quinhentos";
            textArray5[5] = "seiscentos";
            textArray5[6] = "setecentos";
            textArray5[7] = "oitocentos";
            textArray5[8] = "novecentos";
            textArray5[9] = "cem";
            hundreds = textArray5;
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            singlesNumeral;

        public string[] Singles =>
            singlesNumeral;

        public string[] Teens =>
            teens;

        public string[] Tenths =>
            tenths;

        public string[] Hundreds =>
            hundreds;

        public string[] Thousands =>
            null;

        public string[] Million =>
            null;

        public string[] Billion =>
            null;

        public string[] Trillion =>
            null;

        public string[] Quadrillion =>
            null;

        public string[] Quintillion =>
            null;
    }
}

