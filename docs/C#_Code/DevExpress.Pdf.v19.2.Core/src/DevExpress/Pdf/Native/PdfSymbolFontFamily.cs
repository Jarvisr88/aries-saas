namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfSymbolFontFamily : PdfStandardFontFamily
    {
        private const string family = "Symbol";
        private static readonly PdfStandardFontFamily.PdfVariableGlyphWidthProvider symbolWidths;
        private static readonly WidthsProvider widthsProvider;
        private static readonly DescriptorProvider descriptorProvider;

        static PdfSymbolFontFamily()
        {
            Dictionary<string, short> dictionary = new Dictionary<string, short>();
            dictionary.Add("space", 250);
            dictionary.Add("exclam", 0x14d);
            dictionary.Add("universal", 0x2c9);
            dictionary.Add("numbersign", 500);
            dictionary.Add("existential", 0x225);
            dictionary.Add("percent", 0x341);
            dictionary.Add("ampersand", 0x30a);
            dictionary.Add("suchthat", 0x1b7);
            dictionary.Add("parenleft", 0x14d);
            dictionary.Add("parenright", 0x14d);
            dictionary.Add("asteriskmath", 500);
            dictionary.Add("plus", 0x225);
            dictionary.Add("comma", 250);
            dictionary.Add("minus", 0x225);
            dictionary.Add("period", 250);
            dictionary.Add("slash", 0x116);
            dictionary.Add("zero", 500);
            dictionary.Add("one", 500);
            dictionary.Add("two", 500);
            dictionary.Add("three", 500);
            dictionary.Add("four", 500);
            dictionary.Add("five", 500);
            dictionary.Add("six", 500);
            dictionary.Add("seven", 500);
            dictionary.Add("eight", 500);
            dictionary.Add("nine", 500);
            dictionary.Add("colon", 0x116);
            dictionary.Add("semicolon", 0x116);
            dictionary.Add("less", 0x225);
            dictionary.Add("equal", 0x225);
            dictionary.Add("greater", 0x225);
            dictionary.Add("question", 0x1bc);
            dictionary.Add("congruent", 0x225);
            dictionary.Add("Alpha", 0x2d2);
            dictionary.Add("Beta", 0x29b);
            dictionary.Add("Chi", 0x2d2);
            dictionary.Add("Delta", 0x264);
            dictionary.Add("Epsilon", 0x263);
            dictionary.Add("Phi", 0x2fb);
            dictionary.Add("Gamma", 0x25b);
            dictionary.Add("Eta", 0x2d2);
            dictionary.Add("Iota", 0x14d);
            dictionary.Add("theta1", 0x277);
            dictionary.Add("Kappa", 0x2d2);
            dictionary.Add("Lambda", 0x2ae);
            dictionary.Add("Mu", 0x379);
            dictionary.Add("Nu", 0x2d2);
            dictionary.Add("Omicron", 0x2d2);
            dictionary.Add("Pi", 0x300);
            dictionary.Add("Theta", 0x2e5);
            dictionary.Add("Rho", 0x22c);
            dictionary.Add("Sigma", 0x250);
            dictionary.Add("Tau", 0x263);
            dictionary.Add("Upsilon", 690);
            dictionary.Add("sigma1", 0x1b7);
            dictionary.Add("Omega", 0x300);
            dictionary.Add("Xi", 0x285);
            dictionary.Add("Psi", 0x31b);
            dictionary.Add("Zeta", 0x263);
            dictionary.Add("bracketleft", 0x14d);
            dictionary.Add("therefore", 0x35f);
            dictionary.Add("bracketright", 0x14d);
            dictionary.Add("perpendicular", 0x292);
            dictionary.Add("underscore", 500);
            dictionary.Add("radicalex", 500);
            dictionary.Add("alpha", 0x277);
            dictionary.Add("beta", 0x225);
            dictionary.Add("chi", 0x225);
            dictionary.Add("delta", 0x1ee);
            dictionary.Add("epsilon", 0x1b7);
            dictionary.Add("phi", 0x209);
            dictionary.Add("gamma", 0x19b);
            dictionary.Add("eta", 0x25b);
            dictionary.Add("iota", 0x149);
            dictionary.Add("phi1", 0x25b);
            dictionary.Add("kappa", 0x225);
            dictionary.Add("lambda", 0x225);
            dictionary.Add("mu", 0x240);
            dictionary.Add("nu", 0x209);
            dictionary.Add("omicron", 0x225);
            dictionary.Add("pi", 0x225);
            dictionary.Add("theta", 0x209);
            dictionary.Add("rho", 0x225);
            dictionary.Add("sigma", 0x25b);
            dictionary.Add("tau", 0x1b7);
            dictionary.Add("upsilon", 0x240);
            dictionary.Add("omega1", 0x2c9);
            dictionary.Add("omega", 0x2ae);
            dictionary.Add("xi", 0x1ed);
            dictionary.Add("psi", 0x2ae);
            dictionary.Add("zeta", 0x1ee);
            dictionary.Add("braceleft", 480);
            dictionary.Add("bar", 200);
            dictionary.Add("braceright", 480);
            dictionary.Add("similar", 0x225);
            dictionary.Add("Euro", 750);
            dictionary.Add("Upsilon1", 620);
            dictionary.Add("minute", 0xf7);
            dictionary.Add("lessequal", 0x225);
            dictionary.Add("fraction", 0xa7);
            dictionary.Add("infinity", 0x2c9);
            dictionary.Add("florin", 500);
            dictionary.Add("club", 0x2f1);
            dictionary.Add("diamond", 0x2f1);
            dictionary.Add("heart", 0x2f1);
            dictionary.Add("spade", 0x2f1);
            dictionary.Add("arrowboth", 0x412);
            dictionary.Add("arrowleft", 0x3db);
            dictionary.Add("arrowup", 0x25b);
            dictionary.Add("arrowright", 0x3db);
            dictionary.Add("arrowdown", 0x25b);
            dictionary.Add("degree", 400);
            dictionary.Add("plusminus", 0x225);
            dictionary.Add("second", 0x19b);
            dictionary.Add("greaterequal", 0x225);
            dictionary.Add("multiply", 0x225);
            dictionary.Add("proportional", 0x2c9);
            dictionary.Add("partialdiff", 0x1ee);
            dictionary.Add("bullet", 460);
            dictionary.Add("divide", 0x225);
            dictionary.Add("notequal", 0x225);
            dictionary.Add("equivalence", 0x225);
            dictionary.Add("approxequal", 0x225);
            dictionary.Add("ellipsis", 0x3e8);
            dictionary.Add("arrowvertex", 0x25b);
            dictionary.Add("arrowhorizex", 0x3e8);
            dictionary.Add("carriagereturn", 0x292);
            dictionary.Add("aleph", 0x337);
            dictionary.Add("Ifraktur", 0x2ae);
            dictionary.Add("Rfraktur", 0x31b);
            dictionary.Add("weierstrass", 0x3db);
            dictionary.Add("circlemultiply", 0x300);
            dictionary.Add("circleplus", 0x300);
            dictionary.Add("emptyset", 0x337);
            dictionary.Add("intersection", 0x300);
            dictionary.Add("union", 0x300);
            dictionary.Add("propersuperset", 0x2c9);
            dictionary.Add("reflexsuperset", 0x2c9);
            dictionary.Add("notsubset", 0x2c9);
            dictionary.Add("propersubset", 0x2c9);
            dictionary.Add("reflexsubset", 0x2c9);
            dictionary.Add("element", 0x2c9);
            dictionary.Add("notelement", 0x2c9);
            dictionary.Add("angle", 0x300);
            dictionary.Add("gradient", 0x2c9);
            dictionary.Add("registerserif", 790);
            dictionary.Add("copyrightserif", 790);
            dictionary.Add("trademarkserif", 890);
            dictionary.Add("product", 0x337);
            dictionary.Add("radical", 0x225);
            dictionary.Add("dotmath", 250);
            dictionary.Add("logicalnot", 0x2c9);
            dictionary.Add("logicaland", 0x25b);
            dictionary.Add("logicalor", 0x25b);
            dictionary.Add("arrowdblboth", 0x412);
            dictionary.Add("arrowdblleft", 0x3db);
            dictionary.Add("arrowdblup", 0x25b);
            dictionary.Add("arrowdblright", 0x3db);
            dictionary.Add("arrowdbldown", 0x25b);
            dictionary.Add("lozenge", 0x1ee);
            dictionary.Add("angleleft", 0x149);
            dictionary.Add("registersans", 790);
            dictionary.Add("copyrightsans", 790);
            dictionary.Add("trademarksans", 0x312);
            dictionary.Add("summation", 0x2c9);
            dictionary.Add("parenlefttp", 0x180);
            dictionary.Add("parenleftex", 0x180);
            dictionary.Add("parenleftbt", 0x180);
            dictionary.Add("bracketlefttp", 0x180);
            dictionary.Add("bracketleftex", 0x180);
            dictionary.Add("bracketleftbt", 0x180);
            dictionary.Add("bracelefttp", 0x1ee);
            dictionary.Add("braceleftmid", 0x1ee);
            dictionary.Add("braceleftbt", 0x1ee);
            dictionary.Add("braceex", 0x1ee);
            dictionary.Add("angleright", 0x149);
            dictionary.Add("integral", 0x112);
            dictionary.Add("integraltp", 0x2ae);
            dictionary.Add("integralex", 0x2ae);
            dictionary.Add("integralbt", 0x2ae);
            dictionary.Add("parenrighttp", 0x180);
            dictionary.Add("parenrightex", 0x180);
            dictionary.Add("parenrightbt", 0x180);
            dictionary.Add("bracketrighttp", 0x180);
            dictionary.Add("bracketrightex", 0x180);
            dictionary.Add("bracketrightbt", 0x180);
            dictionary.Add("bracerighttp", 0x1ee);
            dictionary.Add("bracerightmid", 0x1ee);
            dictionary.Add("bracerightbt", 0x1ee);
            dictionary.Add("apple", 790);
            symbolWidths = new PdfStandardFontFamily.PdfVariableGlyphWidthProvider(dictionary);
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
                    data1.FontFamily = "Symbol";
                    data1.Flags = PdfFontFlags.Symbolic;
                    data1.BBox = new PdfRectangle(-180.0, -293.0, 1090.0, 1010.0);
                    data1.StemH = 92.0;
                    data1.StemV = 85.0;
                    return data1;
                }
            }
        }

        private class WidthsProvider : PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<IPdfGlyphWidthProvider>
        {
            protected override IPdfGlyphWidthProvider Regular =>
                PdfSymbolFontFamily.symbolWidths;
        }
    }
}

