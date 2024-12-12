namespace DevExpress.Office.NumberConverters
{
    using System;

    public class CardinalGreekNumericsProvider : INumericsProvider
    {
        private static string[] separator = new string[] { " ", " ", "" };
        private static string[] generalSingles;
        private static string[] singlesNumeral;
        private static string[] teens;
        private static string[] tenths;
        private static string[] hundreds;
        private static string[] thousands;
        private static string[] million;
        private static string[] billion;
        private static string[] trillion;
        private static string[] quadrillion;
        private static string[] quintillion;

        static CardinalGreekNumericsProvider()
        {
            string[] textArray2 = new string[10];
            textArray2[0] = "ένα";
            textArray2[1] = "δύο";
            textArray2[2] = "τρία";
            textArray2[3] = "τέσσερα";
            textArray2[4] = "πέντε";
            textArray2[5] = "έξι";
            textArray2[6] = "επτά";
            textArray2[7] = "οκτώ";
            textArray2[8] = "εννέα";
            textArray2[9] = "μηδέν";
            generalSingles = textArray2;
            string[] textArray3 = new string[10];
            textArray3[0] = "ένα";
            textArray3[1] = "δύο";
            textArray3[2] = "τρεις";
            textArray3[3] = "τέσσερις";
            textArray3[4] = "πέντε";
            textArray3[5] = "έξι";
            textArray3[6] = "επτά";
            textArray3[7] = "οκτώ";
            textArray3[8] = "εννέα";
            textArray3[9] = "μία";
            singlesNumeral = textArray3;
            string[] textArray4 = new string[10];
            textArray4[0] = "δέκα";
            textArray4[1] = "ένδεκα";
            textArray4[2] = "δώδεκα";
            textArray4[3] = "δεκατρία";
            textArray4[4] = "δεκατέσσερα";
            textArray4[5] = "δεκαπέντε";
            textArray4[6] = "δεκαέξι";
            textArray4[7] = "δεκαεπτά";
            textArray4[8] = "δεκαοκτώ";
            textArray4[9] = "δεκαεννέα";
            teens = textArray4;
            tenths = new string[] { "είκοσι", "τριάντα", "σαράντα", "πενήντα", "εξήντα", "εβδομήντα", "ογδόντα", "ενενήντα" };
            string[] textArray6 = new string[10];
            textArray6[0] = "εκατόν";
            textArray6[1] = "διακόσια";
            textArray6[2] = "τριακόσια";
            textArray6[3] = "τετρακόσια";
            textArray6[4] = "πεντακόσια";
            textArray6[5] = "εξακόσια";
            textArray6[6] = "επτακόσια";
            textArray6[7] = "οκτακόσια";
            textArray6[8] = "εννιακόσια";
            textArray6[9] = "εκατό";
            hundreds = textArray6;
            thousands = new string[] { "χιλιάδες", "χίλια" };
            million = new string[] { "εκατομμύρια", "εκατομμύριο" };
            billion = new string[] { "δισεκατομμύρια", "δισεκατομμύριο" };
            trillion = new string[] { "τρισεκατομμύρια", "τρισεκατομμύριο" };
            quadrillion = new string[] { "τετράκις εκατομμύρια", "τετράκις εκατομμύριο" };
            quintillion = new string[] { "πεντάκις εκατομμύρια", "πεντάκις εκατομμύριο" };
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

