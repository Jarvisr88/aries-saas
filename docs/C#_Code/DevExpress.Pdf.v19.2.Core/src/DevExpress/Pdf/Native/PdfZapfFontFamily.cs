namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfZapfFontFamily : PdfStandardFontFamily
    {
        private const string family = "ZapfDingbats";
        private static readonly PdfStandardFontFamily.PdfVariableGlyphWidthProvider zapfDingbatsWidths;
        private static readonly WidthsProvider widthsProvider;
        private static readonly DescriptorProvider descriptorProvider;

        static PdfZapfFontFamily()
        {
            Dictionary<string, short> dictionary = new Dictionary<string, short>();
            dictionary.Add("space", 0x116);
            dictionary.Add("a1", 0x3ce);
            dictionary.Add("a2", 0x3c1);
            dictionary.Add("a202", 0x3ce);
            dictionary.Add("a3", 980);
            dictionary.Add("a4", 0x2cf);
            dictionary.Add("a5", 0x315);
            dictionary.Add("a119", 790);
            dictionary.Add("a118", 0x317);
            dictionary.Add("a117", 690);
            dictionary.Add("a11", 960);
            dictionary.Add("a12", 0x3ab);
            dictionary.Add("a13", 0x225);
            dictionary.Add("a14", 0x357);
            dictionary.Add("a15", 0x38f);
            dictionary.Add("a16", 0x3a5);
            dictionary.Add("a105", 0x38f);
            dictionary.Add("a17", 0x3b1);
            dictionary.Add("a18", 0x3ce);
            dictionary.Add("a19", 0x2f3);
            dictionary.Add("a20", 0x34e);
            dictionary.Add("a21", 0x2fa);
            dictionary.Add("a22", 0x2f9);
            dictionary.Add("a23", 0x23b);
            dictionary.Add("a24", 0x2a5);
            dictionary.Add("a25", 0x2fb);
            dictionary.Add("a26", 760);
            dictionary.Add("a27", 0x2f7);
            dictionary.Add("a28", 0x2f2);
            dictionary.Add("a6", 0x1ee);
            dictionary.Add("a7", 0x228);
            dictionary.Add("a8", 0x219);
            dictionary.Add("a9", 0x241);
            dictionary.Add("a10", 0x2b4);
            dictionary.Add("a29", 0x312);
            dictionary.Add("a30", 0x314);
            dictionary.Add("a31", 0x314);
            dictionary.Add("a32", 790);
            dictionary.Add("a33", 0x319);
            dictionary.Add("a34", 0x31a);
            dictionary.Add("a35", 0x330);
            dictionary.Add("a36", 0x337);
            dictionary.Add("a37", 0x315);
            dictionary.Add("a38", 0x349);
            dictionary.Add("a39", 0x337);
            dictionary.Add("a40", 0x341);
            dictionary.Add("a41", 0x330);
            dictionary.Add("a42", 0x33f);
            dictionary.Add("a43", 0x39b);
            dictionary.Add("a44", 0x2e8);
            dictionary.Add("a45", 0x2d3);
            dictionary.Add("a46", 0x2ed);
            dictionary.Add("a47", 790);
            dictionary.Add("a48", 0x318);
            dictionary.Add("a49", 0x2b7);
            dictionary.Add("a50", 0x308);
            dictionary.Add("a51", 0x300);
            dictionary.Add("a52", 0x318);
            dictionary.Add("a53", 0x2f7);
            dictionary.Add("a54", 0x2c3);
            dictionary.Add("a55", 0x2c4);
            dictionary.Add("a56", 0x2aa);
            dictionary.Add("a57", 0x2bd);
            dictionary.Add("a58", 0x33a);
            dictionary.Add("a59", 0x32f);
            dictionary.Add("a60", 0x315);
            dictionary.Add("a61", 0x315);
            dictionary.Add("a62", 0x2c3);
            dictionary.Add("a63", 0x2af);
            dictionary.Add("a64", 0x2b8);
            dictionary.Add("a65", 0x2b1);
            dictionary.Add("a66", 0x312);
            dictionary.Add("a67", 0x313);
            dictionary.Add("a68", 0x2c9);
            dictionary.Add("a69", 0x317);
            dictionary.Add("a70", 0x311);
            dictionary.Add("a71", 0x317);
            dictionary.Add("a72", 0x369);
            dictionary.Add("a73", 0x2f9);
            dictionary.Add("a74", 0x2fa);
            dictionary.Add("a203", 0x2fa);
            dictionary.Add("a75", 0x2f7);
            dictionary.Add("a204", 0x2f7);
            dictionary.Add("a76", 0x37c);
            dictionary.Add("a77", 0x37c);
            dictionary.Add("a78", 0x314);
            dictionary.Add("a79", 0x310);
            dictionary.Add("a81", 0x1b6);
            dictionary.Add("a82", 0x8a);
            dictionary.Add("a83", 0x115);
            dictionary.Add("a84", 0x19f);
            dictionary.Add("a97", 0x188);
            dictionary.Add("a98", 0x188);
            dictionary.Add("a99", 0x29c);
            dictionary.Add("a100", 0x29c);
            dictionary.Add("a89", 390);
            dictionary.Add("a90", 390);
            dictionary.Add("a93", 0x13d);
            dictionary.Add("a94", 0x13d);
            dictionary.Add("a91", 0x114);
            dictionary.Add("a92", 0x114);
            dictionary.Add("a205", 0x1fd);
            dictionary.Add("a85", 0x1fd);
            dictionary.Add("a206", 410);
            dictionary.Add("a86", 410);
            dictionary.Add("a87", 0xea);
            dictionary.Add("a88", 0xea);
            dictionary.Add("a95", 0x14e);
            dictionary.Add("a96", 0x14e);
            dictionary.Add("a101", 0x2dc);
            dictionary.Add("a102", 0x220);
            dictionary.Add("a103", 0x220);
            dictionary.Add("a104", 910);
            dictionary.Add("a106", 0x29b);
            dictionary.Add("a107", 760);
            dictionary.Add("a108", 760);
            dictionary.Add("a112", 0x308);
            dictionary.Add("a111", 0x253);
            dictionary.Add("a110", 0x2b6);
            dictionary.Add("a109", 0x272);
            dictionary.Add("a120", 0x314);
            dictionary.Add("a121", 0x314);
            dictionary.Add("a122", 0x314);
            dictionary.Add("a123", 0x314);
            dictionary.Add("a124", 0x314);
            dictionary.Add("a125", 0x314);
            dictionary.Add("a126", 0x314);
            dictionary.Add("a127", 0x314);
            dictionary.Add("a128", 0x314);
            dictionary.Add("a129", 0x314);
            dictionary.Add("a130", 0x314);
            dictionary.Add("a131", 0x314);
            dictionary.Add("a132", 0x314);
            dictionary.Add("a133", 0x314);
            dictionary.Add("a134", 0x314);
            dictionary.Add("a135", 0x314);
            dictionary.Add("a136", 0x314);
            dictionary.Add("a137", 0x314);
            dictionary.Add("a138", 0x314);
            dictionary.Add("a139", 0x314);
            dictionary.Add("a140", 0x314);
            dictionary.Add("a141", 0x314);
            dictionary.Add("a142", 0x314);
            dictionary.Add("a143", 0x314);
            dictionary.Add("a144", 0x314);
            dictionary.Add("a145", 0x314);
            dictionary.Add("a146", 0x314);
            dictionary.Add("a147", 0x314);
            dictionary.Add("a148", 0x314);
            dictionary.Add("a149", 0x314);
            dictionary.Add("a150", 0x314);
            dictionary.Add("a151", 0x314);
            dictionary.Add("a152", 0x314);
            dictionary.Add("a153", 0x314);
            dictionary.Add("a154", 0x314);
            dictionary.Add("a155", 0x314);
            dictionary.Add("a156", 0x314);
            dictionary.Add("a157", 0x314);
            dictionary.Add("a158", 0x314);
            dictionary.Add("a159", 0x314);
            dictionary.Add("a160", 0x37e);
            dictionary.Add("a161", 0x346);
            dictionary.Add("a163", 0x3f8);
            dictionary.Add("a164", 0x1ca);
            dictionary.Add("a196", 0x2ec);
            dictionary.Add("a165", 0x39c);
            dictionary.Add("a192", 0x2ec);
            dictionary.Add("a166", 0x396);
            dictionary.Add("a167", 0x39f);
            dictionary.Add("a168", 0x3a0);
            dictionary.Add("a169", 0x3a0);
            dictionary.Add("a170", 0x342);
            dictionary.Add("a171", 0x369);
            dictionary.Add("a172", 0x33c);
            dictionary.Add("a173", 0x39c);
            dictionary.Add("a162", 0x39c);
            dictionary.Add("a174", 0x395);
            dictionary.Add("a175", 930);
            dictionary.Add("a176", 0x3a3);
            dictionary.Add("a177", 0x1cf);
            dictionary.Add("a178", 0x373);
            dictionary.Add("a179", 0x344);
            dictionary.Add("a193", 0x344);
            dictionary.Add("a180", 0x363);
            dictionary.Add("a199", 0x363);
            dictionary.Add("a181", 0x2b8);
            dictionary.Add("a200", 0x2b8);
            dictionary.Add("a182", 0x36a);
            dictionary.Add("a201", 0x36a);
            dictionary.Add("a183", 760);
            dictionary.Add("a184", 0x3b2);
            dictionary.Add("a197", 0x303);
            dictionary.Add("a185", 0x361);
            dictionary.Add("a194", 0x303);
            dictionary.Add("a198", 0x378);
            dictionary.Add("a186", 0x3c7);
            dictionary.Add("a195", 0x378);
            dictionary.Add("a187", 0x33f);
            dictionary.Add("a188", 0x369);
            dictionary.Add("a189", 0x39f);
            dictionary.Add("a190", 970);
            dictionary.Add("a191", 0x396);
            zapfDingbatsWidths = new PdfStandardFontFamily.PdfVariableGlyphWidthProvider(dictionary);
            widthsProvider = new WidthsProvider();
            descriptorProvider = new DescriptorProvider();
        }

        protected override PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<IPdfGlyphWidthProvider> GlyphWidthProvider =>
            widthsProvider;

        protected override PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<PdfFontDescriptorData> FontDescriptorProvider =>
            descriptorProvider;

        private class DescriptorProvider : PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<PdfFontDescriptorData>
        {
            protected override PdfFontDescriptorData Regular
            {
                get
                {
                    PdfFontDescriptorData data1 = new PdfFontDescriptorData();
                    data1.FontFamily = "ZapfDingbats";
                    data1.Flags = PdfFontFlags.Symbolic;
                    data1.BBox = new PdfRectangle(-1.0, -143.0, 981.0, 820.0);
                    data1.StemV = 90.0;
                    data1.StemH = 28.0;
                    return data1;
                }
            }
        }

        private class WidthsProvider : PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<IPdfGlyphWidthProvider>
        {
            protected override IPdfGlyphWidthProvider Regular =>
                PdfZapfFontFamily.zapfDingbatsWidths;
        }
    }
}

