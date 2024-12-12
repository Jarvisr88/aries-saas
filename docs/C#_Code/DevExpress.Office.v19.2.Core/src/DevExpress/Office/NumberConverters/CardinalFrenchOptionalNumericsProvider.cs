namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalFrenchOptionalNumericsProvider : INumericsProvider
    {
        private static string[] generalSingles;
        private static string[] hundreds;

        static CardinalFrenchOptionalNumericsProvider()
        {
            string[] textArray1 = new string[10];
            textArray1[0] = "un";
            textArray1[1] = "deux";
            textArray1[2] = "trois";
            textArray1[3] = "quatre";
            textArray1[4] = "cinq";
            textArray1[5] = "six";
            textArray1[6] = "sept";
            textArray1[7] = "huit";
            textArray1[8] = "neuf";
            textArray1[9] = "z\x00e9ro";
            generalSingles = textArray1;
            string[] textArray2 = new string[9];
            textArray2[0] = "cent";
            textArray2[1] = "deux cents";
            textArray2[2] = "trois cents";
            textArray2[3] = "quatre cents";
            textArray2[4] = "cinq cents";
            textArray2[5] = "six cents";
            textArray2[6] = "sept cents";
            textArray2[7] = "huit cents";
            textArray2[8] = "neuf cents";
            hundreds = textArray2;
        }

        public string[] Separator =>
            CardinalFrenchNumericsProvider.separator;

        public string[] SinglesNumeral =>
            generalSingles;

        public string[] Singles =>
            generalSingles;

        public string[] Teens =>
            CardinalFrenchNumericsProvider.teens;

        public string[] Tenths =>
            CardinalFrenchNumericsProvider.tenths;

        public string[] Hundreds =>
            hundreds;

        public string[] Thousands =>
            CardinalFrenchNumericsProvider.thousands;

        public string[] Million =>
            CardinalFrenchNumericsProvider.million;

        public string[] Billion =>
            CardinalFrenchNumericsProvider.billion;

        public string[] Trillion =>
            CardinalFrenchNumericsProvider.trillion;

        public string[] Quadrillion =>
            CardinalFrenchNumericsProvider.quadrillion;

        public string[] Quintillion =>
            CardinalFrenchNumericsProvider.quintillion;
    }
}

