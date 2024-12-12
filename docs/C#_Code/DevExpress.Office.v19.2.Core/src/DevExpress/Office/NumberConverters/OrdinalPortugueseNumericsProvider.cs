namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalPortugueseNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", " ", "" };
        private static string[] singlesNumeral;
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

        static OrdinalPortugueseNumericsProvider()
        {
            string[] textArray2 = new string[10];
            textArray2[0] = "";
            textArray2[1] = "dois";
            textArray2[2] = "tr\x00eas";
            textArray2[3] = "quatro";
            textArray2[4] = "cinco";
            textArray2[5] = "seis";
            textArray2[6] = "sete";
            textArray2[7] = "oito";
            textArray2[8] = "nove";
            textArray2[9] = "un";
            singlesNumeral = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "primeiro";
            textArray3[1] = "segundo";
            textArray3[2] = "terceiro";
            textArray3[3] = "quarto";
            textArray3[4] = "quinto";
            textArray3[5] = "sexto";
            textArray3[6] = "s\x00e9timo";
            textArray3[7] = "oitavo";
            textArray3[8] = "nono";
            textArray3[9] = "zero";
            generalSingles = textArray3;
            string[] textArray4 = new string[10];
            textArray4[0] = "d\x00e9cimo";
            textArray4[1] = "d\x00e9cimo primeiro";
            textArray4[2] = "d\x00e9cimo segundo";
            textArray4[3] = "d\x00e9cimo terceiro";
            textArray4[4] = "d\x00e9cimo quarto";
            textArray4[5] = "d\x00e9cimo quinto";
            textArray4[6] = "d\x00e9cimo sexto";
            textArray4[7] = "d\x00e9cimo s\x00e9timo";
            textArray4[8] = "d\x00e9cimo oitavo";
            textArray4[9] = "d\x00e9cimo nono";
            teens = textArray4;
            tenths = new string[] { "vig\x00e9simo", "trig\x00e9simo", "quadrag\x00e9simo", "q\x00fcinquag\x00e9simo", "sexag\x00e9simo", "setuag\x00e9simo", "octog\x00e9simo", "nonag\x00e9simo" };
            string[] textArray6 = new string[9];
            textArray6[0] = "cent\x00e9simo";
            textArray6[1] = "ducent\x00e9simo";
            textArray6[2] = "trecent\x00e9simo";
            textArray6[3] = "quadringent\x00e9simo";
            textArray6[4] = "q\x00fcingent\x00e9simo";
            textArray6[5] = "sexcent\x00e9simo";
            textArray6[6] = "setingent\x00e9simo";
            textArray6[7] = "octingent\x00e9simo";
            textArray6[8] = "nongent\x00e9simo";
            hundreds = textArray6;
            thousands = new string[] { "mil\x00e9simos", "mil\x00e9simo" };
            million = new string[] { "milion\x00e9simos", "milion\x00e9simo" };
            billion = new string[] { "mil milion\x00e9simos", "mil milion\x00e9simo" };
            trillion = new string[] { "bilion\x00e9simos", "bilion\x00e9simo" };
            quadrillion = new string[] { "mil bilion\x00e9simos", "mil bilion\x00e9simo" };
            quintillion = new string[] { "trilion\x00e9simos", "trilion\x00e9simo" };
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

