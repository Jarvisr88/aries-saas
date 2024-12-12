﻿namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalItalianOptionalNumericsProvider : INumericsProvider
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

        static OrdinalItalianOptionalNumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "primo";
            textArray1[1] = "secondo";
            textArray1[2] = "terzo";
            textArray1[3] = "quarto";
            textArray1[4] = "quinto";
            textArray1[5] = "sesto";
            textArray1[6] = "settimo";
            textArray1[7] = "ottavo";
            textArray1[8] = "nono";
            textArray1[9] = "zero";
            generalSingles = textArray1;
            separator = new string[] { "", "", " " };
            string[] textArray3 = new string[10];
            textArray3[0] = "decimo";
            textArray3[1] = "undicesimo";
            textArray3[2] = "dodicesimo";
            textArray3[3] = "tredicesimo";
            textArray3[4] = "quattordicesimo";
            textArray3[5] = "quindicesimo";
            textArray3[6] = "sedicesimo";
            textArray3[7] = "diciassettesimo";
            textArray3[8] = "diciottesimo";
            textArray3[9] = "diciannovesimo";
            teens = textArray3;
            tenths = new string[] { "ventesimo", "trentesimo", "quarantesimo", "cinquantesimo", "sessantesimo", "settantesimo", "ottantesimo", "novantesimo" };
            string[] textArray5 = new string[9];
            textArray5[0] = "centesimo";
            textArray5[1] = "duecentesimo";
            textArray5[2] = "trecentesimo";
            textArray5[3] = "quattrocentesimo";
            textArray5[4] = "cinquecentesimo";
            textArray5[5] = "seicentesimo";
            textArray5[6] = "settecentesimo";
            textArray5[7] = "ottocentesimo";
            textArray5[8] = "novecentesimo";
            hundreds = textArray5;
            thousands = new string[] { "millesimo", "millesimo" };
            million = new string[] { "milionesimo", "milionesimo" };
            billion = new string[] { "miliardesimo", "miliardesimo" };
            trillion = new string[] { "bilionesimo", "bilionesimo" };
            quadrillion = new string[] { "biliardesimo", "biliardesimo" };
            quintillion = new string[] { "trilionesimo", "trilionesimo" };
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
