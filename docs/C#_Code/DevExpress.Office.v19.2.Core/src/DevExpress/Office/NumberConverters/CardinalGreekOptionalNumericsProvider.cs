namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalGreekOptionalNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", " ", "" };
        private static string[] teens;
        private static string[] tenths;
        private static string[] hundreds;

        static CardinalGreekOptionalNumericsProvider()
        {
            string[] textArray2 = new string[10];
            textArray2[0] = "δέκα";
            textArray2[1] = "ένδεκα";
            textArray2[2] = "δώδεκα";
            textArray2[3] = "δεκατρείς";
            textArray2[4] = "δεκατέσσερις";
            textArray2[5] = "δεκαπέντε";
            textArray2[6] = "δεκαέξι";
            textArray2[7] = "δεκαεπτά";
            textArray2[8] = "δεκαοκτώ";
            textArray2[9] = "δεκαεννέα";
            teens = textArray2;
            tenths = new string[] { "είκοσι", "τριάντα", "σαράντα", "πενήντα", "εξήντα", "εβδομήντα", "ογδόντα", "ενενήντα" };
            string[] textArray4 = new string[10];
            textArray4[0] = "εκατόν";
            textArray4[1] = "διακόσιες";
            textArray4[2] = "τριακόσιες";
            textArray4[3] = "τετρακόσιες";
            textArray4[4] = "πεντακόσιες";
            textArray4[5] = "εξακόσιες";
            textArray4[6] = "επτακόσιες";
            textArray4[7] = "οκτακόσιες";
            textArray4[8] = "εννιακόσιες";
            textArray4[9] = "εκατό";
            hundreds = textArray4;
        }

        public string[] Separator =>
            separator;

        public string[] SinglesNumeral =>
            null;

        public string[] Singles =>
            null;

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

