namespace DevExpress.Office.NumberConverters
{
    using System;

    public class OrdinalFrenchOptionalNumericsProvider : INumericsProvider
    {
        private static string[] generalSingles;

        static OrdinalFrenchOptionalNumericsProvider()
        {
            string[] textArray1 = new string[11];
            textArray1[0] = "premier";
            textArray1[1] = "deuxi\x00e8me";
            textArray1[2] = "troisi\x00e8me";
            textArray1[3] = "quatri\x00e8me";
            textArray1[4] = "cinqui\x00e8me";
            textArray1[5] = "sixi\x00e8me";
            textArray1[6] = "septi\x00e8me";
            textArray1[7] = "huiti\x00e8me";
            textArray1[8] = "neuvi\x00e8me";
            textArray1[9] = "z\x00e9ro";
            textArray1[10] = "i\x00e8me";
            generalSingles = textArray1;
        }

        public string[] Separator =>
            OrdinalFrenchNumericsProvider.separator;

        public string[] SinglesNumeral =>
            generalSingles;

        public string[] Singles =>
            generalSingles;

        public string[] Teens =>
            OrdinalFrenchNumericsProvider.teens;

        public string[] Tenths =>
            OrdinalFrenchNumericsProvider.tenths;

        public string[] Hundreds =>
            OrdinalFrenchNumericsProvider.hundreds;

        public string[] Thousands =>
            OrdinalFrenchNumericsProvider.thousands;

        public string[] Million =>
            OrdinalFrenchNumericsProvider.million;

        public string[] Billion =>
            OrdinalFrenchNumericsProvider.billion;

        public string[] Trillion =>
            OrdinalFrenchNumericsProvider.trillion;

        public string[] Quadrillion =>
            OrdinalFrenchNumericsProvider.quadrillion;

        public string[] Quintillion =>
            OrdinalFrenchNumericsProvider.quintillion;
    }
}

