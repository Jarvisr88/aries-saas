namespace DevExpress.Xpf.Editors
{
    using System;
    using System.ComponentModel;
    using System.Windows.Media;

    public static class PredefinedColorCollections
    {
        private static readonly ColorCollection standard;
        private static readonly ColorCollection apex;
        private static readonly ColorCollection aspect;
        private static readonly ColorCollection civic;
        private static readonly ColorCollection concource;
        private static readonly ColorCollection equality;
        private static readonly ColorCollection flow;
        private static readonly ColorCollection foundry;
        private static readonly ColorCollection grayscale;
        private static readonly ColorCollection median;
        private static readonly ColorCollection metro;
        private static readonly ColorCollection module;
        private static readonly ColorCollection office;
        private static readonly ColorCollection opulent;
        private static readonly ColorCollection oriel;
        private static readonly ColorCollection origin;
        private static readonly ColorCollection paper;
        private static readonly ColorCollection solstice;
        private static readonly ColorCollection trek;
        private static readonly ColorCollection urban;
        private static readonly ColorCollection verve;

        static PredefinedColorCollections()
        {
            Color[] source = new Color[10];
            source[0] = ColorEditDefaultColors.DarkRed;
            source[1] = ColorEditDefaultColors.Red;
            source[2] = ColorEditDefaultColors.Orange;
            source[3] = ColorEditDefaultColors.Yellow;
            source[4] = ColorEditDefaultColors.LightGreen;
            source[5] = ColorEditDefaultColors.Green;
            source[6] = ColorEditDefaultColors.LightBlue;
            source[7] = ColorEditDefaultColors.Blue;
            source[8] = ColorEditDefaultColors.DarkBlue;
            source[9] = ColorEditDefaultColors.Purple;
            standard = new ColorCollection(source);
            Color[] colorArray2 = new Color[10];
            colorArray2[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray2[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray2[2] = ColorHelper.ColorFromHex("#FFC9C2D1");
            colorArray2[3] = ColorHelper.ColorFromHex("#FF69676D");
            colorArray2[4] = ColorHelper.ColorFromHex("#FFCEB966");
            colorArray2[5] = ColorHelper.ColorFromHex("#FF9CB084");
            colorArray2[6] = ColorHelper.ColorFromHex("#FF6BB1C9");
            colorArray2[7] = ColorHelper.ColorFromHex("#FF6585CF");
            colorArray2[8] = ColorHelper.ColorFromHex("#FF7E6BC9");
            colorArray2[9] = ColorHelper.ColorFromHex("#FFA379BB");
            apex = new ColorCollection(colorArray2);
            Color[] colorArray3 = new Color[10];
            colorArray3[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray3[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray3[2] = ColorHelper.ColorFromHex("#FFE3DED1");
            colorArray3[3] = ColorHelper.ColorFromHex("#FF323232");
            colorArray3[4] = ColorHelper.ColorFromHex("#FFF07F09");
            colorArray3[5] = ColorHelper.ColorFromHex("#FF9F2936");
            colorArray3[6] = ColorHelper.ColorFromHex("#FF1B587C");
            colorArray3[7] = ColorHelper.ColorFromHex("#FF4E8542");
            colorArray3[8] = ColorHelper.ColorFromHex("#FF604878");
            colorArray3[9] = ColorHelper.ColorFromHex("#FFC19859");
            aspect = new ColorCollection(colorArray3);
            Color[] colorArray4 = new Color[10];
            colorArray4[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray4[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray4[2] = ColorHelper.ColorFromHex("#FFC5D1D7");
            colorArray4[3] = ColorHelper.ColorFromHex("#FF646B86");
            colorArray4[4] = ColorHelper.ColorFromHex("#FFD16349");
            colorArray4[5] = ColorHelper.ColorFromHex("#FFCCB400");
            colorArray4[6] = ColorHelper.ColorFromHex("#FF8CADAE");
            colorArray4[7] = ColorHelper.ColorFromHex("#FF8C7B70");
            colorArray4[8] = ColorHelper.ColorFromHex("#FF8FB08C");
            colorArray4[9] = ColorHelper.ColorFromHex("#FFD19049");
            civic = new ColorCollection(colorArray4);
            Color[] colorArray5 = new Color[10];
            colorArray5[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray5[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray5[2] = ColorHelper.ColorFromHex("#FFDEF5FA");
            colorArray5[3] = ColorHelper.ColorFromHex("#FF464646");
            colorArray5[4] = ColorHelper.ColorFromHex("#FF2DA2BF");
            colorArray5[5] = ColorHelper.ColorFromHex("#FFDA1F28");
            colorArray5[6] = ColorHelper.ColorFromHex("#FFEB641B");
            colorArray5[7] = ColorHelper.ColorFromHex("#FF39639D");
            colorArray5[8] = ColorHelper.ColorFromHex("#FF474B78");
            colorArray5[9] = ColorHelper.ColorFromHex("#FF7D3C4A");
            concource = new ColorCollection(colorArray5);
            Color[] colorArray6 = new Color[10];
            colorArray6[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray6[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray6[2] = ColorHelper.ColorFromHex("#FFDEF5FA");
            colorArray6[3] = ColorHelper.ColorFromHex("#FF464646");
            colorArray6[4] = ColorHelper.ColorFromHex("#FF2DA2BF");
            colorArray6[5] = ColorHelper.ColorFromHex("#FFDA1F28");
            colorArray6[6] = ColorHelper.ColorFromHex("#FFEB641B");
            colorArray6[7] = ColorHelper.ColorFromHex("#FF39639D");
            colorArray6[8] = ColorHelper.ColorFromHex("#FF474B78");
            colorArray6[9] = ColorHelper.ColorFromHex("#FF7D3C4A");
            equality = new ColorCollection(colorArray6);
            Color[] colorArray7 = new Color[10];
            colorArray7[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray7[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray7[2] = ColorHelper.ColorFromHex("#FFDBF5F9");
            colorArray7[3] = ColorHelper.ColorFromHex("#FF04617B");
            colorArray7[4] = ColorHelper.ColorFromHex("#FF0F6FC6");
            colorArray7[5] = ColorHelper.ColorFromHex("#FF009DD9");
            colorArray7[6] = ColorHelper.ColorFromHex("#FF0BD0D9");
            colorArray7[7] = ColorHelper.ColorFromHex("#FF10CF9B");
            colorArray7[8] = ColorHelper.ColorFromHex("#FF7CCA62");
            colorArray7[9] = ColorHelper.ColorFromHex("#FFA5C249");
            flow = new ColorCollection(colorArray7);
            Color[] colorArray8 = new Color[10];
            colorArray8[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray8[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray8[2] = ColorHelper.ColorFromHex("#FFEAEBDE");
            colorArray8[3] = ColorHelper.ColorFromHex("#FF676A55");
            colorArray8[4] = ColorHelper.ColorFromHex("#FF72A376");
            colorArray8[5] = ColorHelper.ColorFromHex("#FFB0CCB0");
            colorArray8[6] = ColorHelper.ColorFromHex("#FFA8CDD7");
            colorArray8[7] = ColorHelper.ColorFromHex("#FFC0BEAF");
            colorArray8[8] = ColorHelper.ColorFromHex("#FFCEC597");
            colorArray8[9] = ColorHelper.ColorFromHex("#FFE8B7B7");
            foundry = new ColorCollection(colorArray8);
            Color[] colorArray9 = new Color[10];
            colorArray9[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray9[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray9[2] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray9[3] = ColorHelper.ColorFromHex("#FF000000");
            colorArray9[4] = ColorHelper.ColorFromHex("#FFDDDDDD");
            colorArray9[5] = ColorHelper.ColorFromHex("#FFB2B2B2");
            colorArray9[6] = ColorHelper.ColorFromHex("#FF969696");
            colorArray9[7] = ColorHelper.ColorFromHex("#FF808080");
            colorArray9[8] = ColorHelper.ColorFromHex("#FF5F5F5F");
            colorArray9[9] = ColorHelper.ColorFromHex("#FF4D4D4D");
            grayscale = new ColorCollection(colorArray9);
            Color[] colorArray10 = new Color[10];
            colorArray10[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray10[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray10[2] = ColorHelper.ColorFromHex("#FFEBDDC3");
            colorArray10[3] = ColorHelper.ColorFromHex("#FF775F55");
            colorArray10[4] = ColorHelper.ColorFromHex("#FF94B6D2");
            colorArray10[5] = ColorHelper.ColorFromHex("#FFDD8047");
            colorArray10[6] = ColorHelper.ColorFromHex("#FFA5AB81");
            colorArray10[7] = ColorHelper.ColorFromHex("#FFD8B25C");
            colorArray10[8] = ColorHelper.ColorFromHex("#FF7BA79D");
            colorArray10[9] = ColorHelper.ColorFromHex("#FF968C8C");
            median = new ColorCollection(colorArray10);
            Color[] colorArray11 = new Color[10];
            colorArray11[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray11[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray11[2] = ColorHelper.ColorFromHex("#FFD6ECFF");
            colorArray11[3] = ColorHelper.ColorFromHex("#FF4E5B6F");
            colorArray11[4] = ColorHelper.ColorFromHex("#FF7FD13B");
            colorArray11[5] = ColorHelper.ColorFromHex("#FFEA157A");
            colorArray11[6] = ColorHelper.ColorFromHex("#FFFEB80A");
            colorArray11[7] = ColorHelper.ColorFromHex("#FF00ADDC");
            colorArray11[8] = ColorHelper.ColorFromHex("#FF738AC8");
            colorArray11[9] = ColorHelper.ColorFromHex("#FF1AB39F");
            metro = new ColorCollection(colorArray11);
            Color[] colorArray12 = new Color[10];
            colorArray12[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray12[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray12[2] = ColorHelper.ColorFromHex("#FFD4D4D6");
            colorArray12[3] = ColorHelper.ColorFromHex("#FF5A6378");
            colorArray12[4] = ColorHelper.ColorFromHex("#FFF0AD00");
            colorArray12[5] = ColorHelper.ColorFromHex("#FF60B5CC");
            colorArray12[6] = ColorHelper.ColorFromHex("#FFE66C7D");
            colorArray12[7] = ColorHelper.ColorFromHex("#FF6BB76D");
            colorArray12[8] = ColorHelper.ColorFromHex("#FFE88651");
            colorArray12[9] = ColorHelper.ColorFromHex("#FFC64847");
            module = new ColorCollection(colorArray12);
            Color[] colorArray13 = new Color[10];
            colorArray13[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray13[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray13[2] = ColorHelper.ColorFromHex("#FFEEECE1");
            colorArray13[3] = ColorHelper.ColorFromHex("#FF1F497D");
            colorArray13[4] = ColorHelper.ColorFromHex("#FF4F81BD");
            colorArray13[5] = ColorHelper.ColorFromHex("#FFC0504D");
            colorArray13[6] = ColorHelper.ColorFromHex("#FF9BBB59");
            colorArray13[7] = ColorHelper.ColorFromHex("#FF8064A2");
            colorArray13[8] = ColorHelper.ColorFromHex("#FF4BACC6");
            colorArray13[9] = ColorHelper.ColorFromHex("#FFF79646");
            office = new ColorCollection(colorArray13);
            Color[] colorArray14 = new Color[10];
            colorArray14[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray14[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray14[2] = ColorHelper.ColorFromHex("#FFF4E7ED");
            colorArray14[3] = ColorHelper.ColorFromHex("#FFB13F9A");
            colorArray14[4] = ColorHelper.ColorFromHex("#FFB83D68");
            colorArray14[5] = ColorHelper.ColorFromHex("#FFAC66BB");
            colorArray14[6] = ColorHelper.ColorFromHex("#FFDE6C36");
            colorArray14[7] = ColorHelper.ColorFromHex("#FFF9B639");
            colorArray14[8] = ColorHelper.ColorFromHex("#FFCF6DA4");
            colorArray14[9] = ColorHelper.ColorFromHex("#FFFA8D3D");
            opulent = new ColorCollection(colorArray14);
            Color[] colorArray15 = new Color[10];
            colorArray15[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray15[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray15[2] = ColorHelper.ColorFromHex("#FFFFF39D");
            colorArray15[3] = ColorHelper.ColorFromHex("#FF575F6D");
            colorArray15[4] = ColorHelper.ColorFromHex("#FFFE8637");
            colorArray15[5] = ColorHelper.ColorFromHex("#FF7598D9");
            colorArray15[6] = ColorHelper.ColorFromHex("#FFB32C16");
            colorArray15[7] = ColorHelper.ColorFromHex("#FFF5CD2D");
            colorArray15[8] = ColorHelper.ColorFromHex("#FFAEBAD5");
            colorArray15[9] = ColorHelper.ColorFromHex("#FF777C84");
            oriel = new ColorCollection(colorArray15);
            Color[] colorArray16 = new Color[10];
            colorArray16[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray16[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray16[2] = ColorHelper.ColorFromHex("#FFDDE9EC");
            colorArray16[3] = ColorHelper.ColorFromHex("#FF464653");
            colorArray16[4] = ColorHelper.ColorFromHex("#FF727CA3");
            colorArray16[5] = ColorHelper.ColorFromHex("#FF9FB8CD");
            colorArray16[6] = ColorHelper.ColorFromHex("#FFD2DA7A");
            colorArray16[7] = ColorHelper.ColorFromHex("#FFFADA7A");
            colorArray16[8] = ColorHelper.ColorFromHex("#FFB88472");
            colorArray16[9] = ColorHelper.ColorFromHex("#FF8E736A");
            origin = new ColorCollection(colorArray16);
            Color[] colorArray17 = new Color[10];
            colorArray17[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray17[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray17[2] = ColorHelper.ColorFromHex("#FFFEFAC9");
            colorArray17[3] = ColorHelper.ColorFromHex("#FF444D26");
            colorArray17[4] = ColorHelper.ColorFromHex("#FFA5B592");
            colorArray17[5] = ColorHelper.ColorFromHex("#FFF3A447");
            colorArray17[6] = ColorHelper.ColorFromHex("#FFE7BC29");
            colorArray17[7] = ColorHelper.ColorFromHex("#FFD092A7");
            colorArray17[8] = ColorHelper.ColorFromHex("#FF9C85C0");
            colorArray17[9] = ColorHelper.ColorFromHex("#FF809EC2");
            paper = new ColorCollection(colorArray17);
            Color[] colorArray18 = new Color[10];
            colorArray18[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray18[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray18[2] = ColorHelper.ColorFromHex("#FFE7DEC9");
            colorArray18[3] = ColorHelper.ColorFromHex("#FF4F271C");
            colorArray18[4] = ColorHelper.ColorFromHex("#FF4F271C");
            colorArray18[5] = ColorHelper.ColorFromHex("#FFFEB80A");
            colorArray18[6] = ColorHelper.ColorFromHex("#FFE7BC29");
            colorArray18[7] = ColorHelper.ColorFromHex("#FF84AA33");
            colorArray18[8] = ColorHelper.ColorFromHex("#FF964305");
            colorArray18[9] = ColorHelper.ColorFromHex("#FF475A8D");
            solstice = new ColorCollection(colorArray18);
            Color[] colorArray19 = new Color[10];
            colorArray19[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray19[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray19[2] = ColorHelper.ColorFromHex("#FFFBEEC9");
            colorArray19[3] = ColorHelper.ColorFromHex("#FF4E3B30");
            colorArray19[4] = ColorHelper.ColorFromHex("#FFF0A22E");
            colorArray19[5] = ColorHelper.ColorFromHex("#FFA5644E");
            colorArray19[6] = ColorHelper.ColorFromHex("#FFB58B80");
            colorArray19[7] = ColorHelper.ColorFromHex("#FFC3986D");
            colorArray19[8] = ColorHelper.ColorFromHex("#FFA19574");
            colorArray19[9] = ColorHelper.ColorFromHex("#FFC17529");
            trek = new ColorCollection(colorArray19);
            Color[] colorArray20 = new Color[10];
            colorArray20[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray20[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray20[2] = ColorHelper.ColorFromHex("#FFDEDEDE");
            colorArray20[3] = ColorHelper.ColorFromHex("#FF424456");
            colorArray20[4] = ColorHelper.ColorFromHex("#FF53548A");
            colorArray20[5] = ColorHelper.ColorFromHex("#FF438086");
            colorArray20[6] = ColorHelper.ColorFromHex("#FFA04DA3");
            colorArray20[7] = ColorHelper.ColorFromHex("#FFC4652D");
            colorArray20[8] = ColorHelper.ColorFromHex("#FF8B5D3D");
            colorArray20[9] = ColorHelper.ColorFromHex("#FF5C92B5");
            urban = new ColorCollection(colorArray20);
            Color[] colorArray21 = new Color[10];
            colorArray21[0] = ColorHelper.ColorFromHex("#FFFFFFFF");
            colorArray21[1] = ColorHelper.ColorFromHex("#FF000000");
            colorArray21[2] = ColorHelper.ColorFromHex("#FFD2D2D2");
            colorArray21[3] = ColorHelper.ColorFromHex("#FF666666");
            colorArray21[4] = ColorHelper.ColorFromHex("#FFFF388C");
            colorArray21[5] = ColorHelper.ColorFromHex("#FFE40059");
            colorArray21[6] = ColorHelper.ColorFromHex("#FF9C007F");
            colorArray21[7] = ColorHelper.ColorFromHex("#FF68007F");
            colorArray21[8] = ColorHelper.ColorFromHex("#FF005BD3");
            colorArray21[9] = ColorHelper.ColorFromHex("#FF00349E");
            verve = new ColorCollection(colorArray21);
        }

        [Description("Gets base colors for the Apex palette collection.")]
        public static ColorCollection Apex =>
            apex;

        [Description("Gets base colors for the Aspect palette collection.")]
        public static ColorCollection Aspect =>
            aspect;

        [Description("Gets base colors for the Civic palette collection.")]
        public static ColorCollection Civic =>
            civic;

        [Description("Gets base colors for the Concourse palette collection.")]
        public static ColorCollection Concourse =>
            concource;

        [Description("Gets base colors for the Equality palette collection.")]
        public static ColorCollection Equality =>
            equality;

        [Description("Gets base colors for the Flow palette collection.")]
        public static ColorCollection Flow =>
            flow;

        [Description("Gets base colors for the Foundry palette collection.")]
        public static ColorCollection Foundry =>
            foundry;

        [Description("Gets base colors for the Grayscale palette collection.")]
        public static ColorCollection Grayscale =>
            grayscale;

        [Description("Gets base colors for the Median palette collection.")]
        public static ColorCollection Median =>
            median;

        [Description("Gets base colors for the Metro palette collection.")]
        public static ColorCollection Metro =>
            metro;

        [Description("Gets base colors for the Module palette collection.")]
        public static ColorCollection Module =>
            module;

        [Description("Gets base colors for the Office palette collection.")]
        public static ColorCollection Office =>
            office;

        [Description("Gets base colors for the Opulent palette collection.")]
        public static ColorCollection Opulent =>
            opulent;

        [Description("Gets base colors for the Oriel palette collection.")]
        public static ColorCollection Oriel =>
            oriel;

        [Description("Gets base colors for the Origin palette collection.")]
        public static ColorCollection Origin =>
            origin;

        [Description("Gets base colors for the Paper palette collection.")]
        public static ColorCollection Paper =>
            paper;

        [Description("Gets base colors for the Solstice palette collection.")]
        public static ColorCollection Solstice =>
            solstice;

        [Description("Gets the collection of standard colors.")]
        public static ColorCollection Standard =>
            standard;

        [Description("Gets base colors for the Trek palette collection.")]
        public static ColorCollection Trek =>
            trek;

        [Description("Gets base colors for the Urban palette collection.")]
        public static ColorCollection Urban =>
            urban;

        [Description("Gets base colors for the Verve palette collection.")]
        public static ColorCollection Verve =>
            verve;
    }
}

