﻿namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfHelveticaFontFamily : PdfStandardFontFamily
    {
        private const string family = "Helvetica";
        private static readonly PdfStandardFontFamily.PdfVariableGlyphWidthProvider helveticaWidths;
        private static readonly PdfStandardFontFamily.PdfVariableGlyphWidthProvider helveticaBoldWidths;
        private static readonly WidthsProvider widthsProvider;
        private static readonly DescriptorProvider descriptorProvider;

        static PdfHelveticaFontFamily()
        {
            Dictionary<string, short> dictionary = new Dictionary<string, short>();
            dictionary.Add(".notdef", 0x116);
            dictionary.Add("space", 0x116);
            dictionary.Add("exclam", 0x116);
            dictionary.Add("quotedbl", 0x163);
            dictionary.Add("numbersign", 0x22c);
            dictionary.Add("dollar", 0x22c);
            dictionary.Add("percent", 0x379);
            dictionary.Add("ampersand", 0x29b);
            dictionary.Add("quoteright", 0xde);
            dictionary.Add("parenleft", 0x14d);
            dictionary.Add("parenright", 0x14d);
            dictionary.Add("asterisk", 0x185);
            dictionary.Add("plus", 0x248);
            dictionary.Add("comma", 0x116);
            dictionary.Add("hyphen", 0x14d);
            dictionary.Add("period", 0x116);
            dictionary.Add("slash", 0x116);
            dictionary.Add("zero", 0x22c);
            dictionary.Add("one", 0x22c);
            dictionary.Add("two", 0x22c);
            dictionary.Add("three", 0x22c);
            dictionary.Add("four", 0x22c);
            dictionary.Add("five", 0x22c);
            dictionary.Add("six", 0x22c);
            dictionary.Add("seven", 0x22c);
            dictionary.Add("eight", 0x22c);
            dictionary.Add("nine", 0x22c);
            dictionary.Add("colon", 0x116);
            dictionary.Add("semicolon", 0x116);
            dictionary.Add("less", 0x248);
            dictionary.Add("equal", 0x248);
            dictionary.Add("greater", 0x248);
            dictionary.Add("question", 0x22c);
            dictionary.Add("at", 0x3f7);
            dictionary.Add("A", 0x29b);
            dictionary.Add("B", 0x29b);
            dictionary.Add("C", 0x2d2);
            dictionary.Add("D", 0x2d2);
            dictionary.Add("E", 0x29b);
            dictionary.Add("F", 0x263);
            dictionary.Add("G", 0x30a);
            dictionary.Add("H", 0x2d2);
            dictionary.Add("I", 0x116);
            dictionary.Add("J", 500);
            dictionary.Add("K", 0x29b);
            dictionary.Add("L", 0x22c);
            dictionary.Add("M", 0x341);
            dictionary.Add("N", 0x2d2);
            dictionary.Add("O", 0x30a);
            dictionary.Add("P", 0x29b);
            dictionary.Add("Q", 0x30a);
            dictionary.Add("R", 0x2d2);
            dictionary.Add("S", 0x29b);
            dictionary.Add("T", 0x263);
            dictionary.Add("U", 0x2d2);
            dictionary.Add("V", 0x29b);
            dictionary.Add("W", 0x3b0);
            dictionary.Add("X", 0x29b);
            dictionary.Add("Y", 0x29b);
            dictionary.Add("Z", 0x263);
            dictionary.Add("bracketleft", 0x116);
            dictionary.Add("backslash", 0x116);
            dictionary.Add("bracketright", 0x116);
            dictionary.Add("asciicircum", 0x1d5);
            dictionary.Add("underscore", 0x22c);
            dictionary.Add("quoteleft", 0xde);
            dictionary.Add("a", 0x22c);
            dictionary.Add("b", 0x22c);
            dictionary.Add("c", 500);
            dictionary.Add("d", 0x22c);
            dictionary.Add("e", 0x22c);
            dictionary.Add("f", 0x116);
            dictionary.Add("g", 0x22c);
            dictionary.Add("h", 0x22c);
            dictionary.Add("i", 0xde);
            dictionary.Add("j", 0xde);
            dictionary.Add("k", 500);
            dictionary.Add("l", 0xde);
            dictionary.Add("m", 0x341);
            dictionary.Add("n", 0x22c);
            dictionary.Add("o", 0x22c);
            dictionary.Add("p", 0x22c);
            dictionary.Add("q", 0x22c);
            dictionary.Add("r", 0x14d);
            dictionary.Add("s", 500);
            dictionary.Add("t", 0x116);
            dictionary.Add("u", 0x22c);
            dictionary.Add("v", 500);
            dictionary.Add("w", 0x2d2);
            dictionary.Add("x", 500);
            dictionary.Add("y", 500);
            dictionary.Add("z", 500);
            dictionary.Add("braceleft", 0x14e);
            dictionary.Add("bar", 260);
            dictionary.Add("braceright", 0x14e);
            dictionary.Add("asciitilde", 0x248);
            dictionary.Add("exclamdown", 0x14d);
            dictionary.Add("cent", 0x22c);
            dictionary.Add("sterling", 0x22c);
            dictionary.Add("fraction", 0xa7);
            dictionary.Add("yen", 0x22c);
            dictionary.Add("florin", 0x22c);
            dictionary.Add("section", 0x22c);
            dictionary.Add("currency", 0x22c);
            dictionary.Add("quotesingle", 0xbf);
            dictionary.Add("quotedblleft", 0x14d);
            dictionary.Add("guillemotleft", 0x22c);
            dictionary.Add("guilsinglleft", 0x14d);
            dictionary.Add("guilsinglright", 0x14d);
            dictionary.Add("fi", 500);
            dictionary.Add("fl", 500);
            dictionary.Add("endash", 0x22c);
            dictionary.Add("dagger", 0x22c);
            dictionary.Add("daggerdbl", 0x22c);
            dictionary.Add("periodcentered", 0x116);
            dictionary.Add("paragraph", 0x219);
            dictionary.Add("bullet", 350);
            dictionary.Add("quotesinglbase", 0xde);
            dictionary.Add("quotedblbase", 0x14d);
            dictionary.Add("quotedblright", 0x14d);
            dictionary.Add("guillemotright", 0x22c);
            dictionary.Add("ellipsis", 0x3e8);
            dictionary.Add("perthousand", 0x3e8);
            dictionary.Add("questiondown", 0x263);
            dictionary.Add("grave", 0x14d);
            dictionary.Add("acute", 0x14d);
            dictionary.Add("circumflex", 0x14d);
            dictionary.Add("tilde", 0x14d);
            dictionary.Add("macron", 0x14d);
            dictionary.Add("breve", 0x14d);
            dictionary.Add("dotaccent", 0x14d);
            dictionary.Add("dieresis", 0x14d);
            dictionary.Add("ring", 0x14d);
            dictionary.Add("cedilla", 0x14d);
            dictionary.Add("hungarumlaut", 0x14d);
            dictionary.Add("ogonek", 0x14d);
            dictionary.Add("caron", 0x14d);
            dictionary.Add("emdash", 0x3e8);
            dictionary.Add("AE", 0x3e8);
            dictionary.Add("ordfeminine", 370);
            dictionary.Add("Lslash", 0x22c);
            dictionary.Add("Oslash", 0x30a);
            dictionary.Add("OE", 0x3e8);
            dictionary.Add("ordmasculine", 0x16d);
            dictionary.Add("ae", 0x379);
            dictionary.Add("dotlessi", 0x116);
            dictionary.Add("lslash", 0xde);
            dictionary.Add("oslash", 0x263);
            dictionary.Add("oe", 0x3b0);
            dictionary.Add("germandbls", 0x263);
            dictionary.Add("Idieresis", 0x116);
            dictionary.Add("eacute", 0x22c);
            dictionary.Add("abreve", 0x22c);
            dictionary.Add("uhungarumlaut", 0x22c);
            dictionary.Add("ecaron", 0x22c);
            dictionary.Add("Ydieresis", 0x29b);
            dictionary.Add("divide", 0x248);
            dictionary.Add("Yacute", 0x29b);
            dictionary.Add("Acircumflex", 0x29b);
            dictionary.Add("aacute", 0x22c);
            dictionary.Add("Ucircumflex", 0x2d2);
            dictionary.Add("yacute", 500);
            dictionary.Add("scommaaccent", 500);
            dictionary.Add("ecircumflex", 0x22c);
            dictionary.Add("Uring", 0x2d2);
            dictionary.Add("Udieresis", 0x2d2);
            dictionary.Add("aogonek", 0x22c);
            dictionary.Add("Uacute", 0x2d2);
            dictionary.Add("uogonek", 0x22c);
            dictionary.Add("Edieresis", 0x29b);
            dictionary.Add("Dcroat", 0x2d2);
            dictionary.Add("commaaccent", 250);
            dictionary.Add("copyright", 0x2e1);
            dictionary.Add("Emacron", 0x29b);
            dictionary.Add("ccaron", 500);
            dictionary.Add("aring", 0x22c);
            dictionary.Add("Ncommaaccent", 0x2d2);
            dictionary.Add("lacute", 0xde);
            dictionary.Add("agrave", 0x22c);
            dictionary.Add("Tcommaaccent", 0x263);
            dictionary.Add("Cacute", 0x2d2);
            dictionary.Add("atilde", 0x22c);
            dictionary.Add("Edotaccent", 0x29b);
            dictionary.Add("scaron", 500);
            dictionary.Add("scedilla", 500);
            dictionary.Add("iacute", 0x116);
            dictionary.Add("lozenge", 0x1d7);
            dictionary.Add("Rcaron", 0x2d2);
            dictionary.Add("Gcommaaccent", 0x30a);
            dictionary.Add("ucircumflex", 0x22c);
            dictionary.Add("acircumflex", 0x22c);
            dictionary.Add("Amacron", 0x29b);
            dictionary.Add("rcaron", 0x14d);
            dictionary.Add("ccedilla", 500);
            dictionary.Add("Zdotaccent", 0x263);
            dictionary.Add("Thorn", 0x29b);
            dictionary.Add("Omacron", 0x30a);
            dictionary.Add("Racute", 0x2d2);
            dictionary.Add("Sacute", 0x29b);
            dictionary.Add("dcaron", 0x283);
            dictionary.Add("Umacron", 0x2d2);
            dictionary.Add("uring", 0x22c);
            dictionary.Add("threesuperior", 0x14d);
            dictionary.Add("Ograve", 0x30a);
            dictionary.Add("Agrave", 0x29b);
            dictionary.Add("Abreve", 0x29b);
            dictionary.Add("multiply", 0x248);
            dictionary.Add("uacute", 0x22c);
            dictionary.Add("Tcaron", 0x263);
            dictionary.Add("partialdiff", 0x1dc);
            dictionary.Add("ydieresis", 500);
            dictionary.Add("Nacute", 0x2d2);
            dictionary.Add("icircumflex", 0x116);
            dictionary.Add("Ecircumflex", 0x29b);
            dictionary.Add("adieresis", 0x22c);
            dictionary.Add("edieresis", 0x22c);
            dictionary.Add("cacute", 500);
            dictionary.Add("nacute", 0x22c);
            dictionary.Add("umacron", 0x22c);
            dictionary.Add("Ncaron", 0x2d2);
            dictionary.Add("Iacute", 0x116);
            dictionary.Add("plusminus", 0x248);
            dictionary.Add("brokenbar", 260);
            dictionary.Add("registered", 0x2e1);
            dictionary.Add("Gbreve", 0x30a);
            dictionary.Add("Idotaccent", 0x116);
            dictionary.Add("summation", 600);
            dictionary.Add("Egrave", 0x29b);
            dictionary.Add("racute", 0x14d);
            dictionary.Add("omacron", 0x22c);
            dictionary.Add("Zacute", 0x263);
            dictionary.Add("Zcaron", 0x263);
            dictionary.Add("greaterequal", 0x225);
            dictionary.Add("Eth", 0x2d2);
            dictionary.Add("Ccedilla", 0x2d2);
            dictionary.Add("lcommaaccent", 0xde);
            dictionary.Add("tcaron", 0x13d);
            dictionary.Add("eogonek", 0x22c);
            dictionary.Add("Uogonek", 0x2d2);
            dictionary.Add("Aacute", 0x29b);
            dictionary.Add("Adieresis", 0x29b);
            dictionary.Add("egrave", 0x22c);
            dictionary.Add("zacute", 500);
            dictionary.Add("iogonek", 0xde);
            dictionary.Add("Oacute", 0x30a);
            dictionary.Add("oacute", 0x22c);
            dictionary.Add("amacron", 0x22c);
            dictionary.Add("sacute", 500);
            dictionary.Add("idieresis", 0x116);
            dictionary.Add("Ocircumflex", 0x30a);
            dictionary.Add("Ugrave", 0x2d2);
            dictionary.Add("Delta", 0x264);
            dictionary.Add("thorn", 0x22c);
            dictionary.Add("twosuperior", 0x14d);
            dictionary.Add("Odieresis", 0x30a);
            dictionary.Add("mu", 0x22c);
            dictionary.Add("igrave", 0x116);
            dictionary.Add("ohungarumlaut", 0x22c);
            dictionary.Add("Eogonek", 0x29b);
            dictionary.Add("dcroat", 0x22c);
            dictionary.Add("threequarters", 0x342);
            dictionary.Add("Scedilla", 0x29b);
            dictionary.Add("lcaron", 0x12b);
            dictionary.Add("Kcommaaccent", 0x29b);
            dictionary.Add("Lacute", 0x22c);
            dictionary.Add("trademark", 0x3e8);
            dictionary.Add("edotaccent", 0x22c);
            dictionary.Add("Igrave", 0x116);
            dictionary.Add("Imacron", 0x116);
            dictionary.Add("Lcaron", 0x22c);
            dictionary.Add("onehalf", 0x342);
            dictionary.Add("lessequal", 0x225);
            dictionary.Add("ocircumflex", 0x22c);
            dictionary.Add("ntilde", 0x22c);
            dictionary.Add("Uhungarumlaut", 0x2d2);
            dictionary.Add("Eacute", 0x29b);
            dictionary.Add("emacron", 0x22c);
            dictionary.Add("gbreve", 0x22c);
            dictionary.Add("onequarter", 0x342);
            dictionary.Add("Scaron", 0x29b);
            dictionary.Add("Scommaaccent", 0x29b);
            dictionary.Add("Ohungarumlaut", 0x30a);
            dictionary.Add("degree", 400);
            dictionary.Add("ograve", 0x22c);
            dictionary.Add("Ccaron", 0x2d2);
            dictionary.Add("ugrave", 0x22c);
            dictionary.Add("radical", 0x1c5);
            dictionary.Add("Dcaron", 0x2d2);
            dictionary.Add("rcommaaccent", 0x14d);
            dictionary.Add("Ntilde", 0x2d2);
            dictionary.Add("otilde", 0x22c);
            dictionary.Add("Rcommaaccent", 0x2d2);
            dictionary.Add("Lcommaaccent", 0x22c);
            dictionary.Add("Atilde", 0x29b);
            dictionary.Add("Aogonek", 0x29b);
            dictionary.Add("Aring", 0x29b);
            dictionary.Add("Otilde", 0x30a);
            dictionary.Add("zdotaccent", 500);
            dictionary.Add("Ecaron", 0x29b);
            dictionary.Add("Iogonek", 0x116);
            dictionary.Add("kcommaaccent", 500);
            dictionary.Add("minus", 0x248);
            dictionary.Add("Icircumflex", 0x116);
            dictionary.Add("ncaron", 0x22c);
            dictionary.Add("tcommaaccent", 0x116);
            dictionary.Add("logicalnot", 0x248);
            dictionary.Add("odieresis", 0x22c);
            dictionary.Add("udieresis", 0x22c);
            dictionary.Add("notequal", 0x225);
            dictionary.Add("gcommaaccent", 0x22c);
            dictionary.Add("eth", 0x22c);
            dictionary.Add("zcaron", 500);
            dictionary.Add("ncommaaccent", 0x22c);
            dictionary.Add("onesuperior", 0x14d);
            dictionary.Add("imacron", 0x116);
            dictionary.Add("Euro", 0x22c);
            helveticaWidths = new PdfStandardFontFamily.PdfVariableGlyphWidthProvider(dictionary);
            Dictionary<string, short> dictionary2 = new Dictionary<string, short>();
            dictionary2.Add(".notdef", 0x116);
            dictionary2.Add("space", 0x116);
            dictionary2.Add("exclam", 0x14d);
            dictionary2.Add("quotedbl", 0x1da);
            dictionary2.Add("numbersign", 0x22c);
            dictionary2.Add("dollar", 0x22c);
            dictionary2.Add("percent", 0x379);
            dictionary2.Add("ampersand", 0x2d2);
            dictionary2.Add("quoteright", 0x116);
            dictionary2.Add("parenleft", 0x14d);
            dictionary2.Add("parenright", 0x14d);
            dictionary2.Add("asterisk", 0x185);
            dictionary2.Add("plus", 0x248);
            dictionary2.Add("comma", 0x116);
            dictionary2.Add("hyphen", 0x14d);
            dictionary2.Add("period", 0x116);
            dictionary2.Add("slash", 0x116);
            dictionary2.Add("zero", 0x22c);
            dictionary2.Add("one", 0x22c);
            dictionary2.Add("two", 0x22c);
            dictionary2.Add("three", 0x22c);
            dictionary2.Add("four", 0x22c);
            dictionary2.Add("five", 0x22c);
            dictionary2.Add("six", 0x22c);
            dictionary2.Add("seven", 0x22c);
            dictionary2.Add("eight", 0x22c);
            dictionary2.Add("nine", 0x22c);
            dictionary2.Add("colon", 0x14d);
            dictionary2.Add("semicolon", 0x14d);
            dictionary2.Add("less", 0x248);
            dictionary2.Add("equal", 0x248);
            dictionary2.Add("greater", 0x248);
            dictionary2.Add("question", 0x263);
            dictionary2.Add("at", 0x3cf);
            dictionary2.Add("A", 0x2d2);
            dictionary2.Add("B", 0x2d2);
            dictionary2.Add("C", 0x2d2);
            dictionary2.Add("D", 0x2d2);
            dictionary2.Add("E", 0x29b);
            dictionary2.Add("F", 0x263);
            dictionary2.Add("G", 0x30a);
            dictionary2.Add("H", 0x2d2);
            dictionary2.Add("I", 0x116);
            dictionary2.Add("J", 0x22c);
            dictionary2.Add("K", 0x2d2);
            dictionary2.Add("L", 0x263);
            dictionary2.Add("M", 0x341);
            dictionary2.Add("N", 0x2d2);
            dictionary2.Add("O", 0x30a);
            dictionary2.Add("P", 0x29b);
            dictionary2.Add("Q", 0x30a);
            dictionary2.Add("R", 0x2d2);
            dictionary2.Add("S", 0x29b);
            dictionary2.Add("T", 0x263);
            dictionary2.Add("U", 0x2d2);
            dictionary2.Add("V", 0x29b);
            dictionary2.Add("W", 0x3b0);
            dictionary2.Add("X", 0x29b);
            dictionary2.Add("Y", 0x29b);
            dictionary2.Add("Z", 0x263);
            dictionary2.Add("bracketleft", 0x14d);
            dictionary2.Add("backslash", 0x116);
            dictionary2.Add("bracketright", 0x14d);
            dictionary2.Add("asciicircum", 0x248);
            dictionary2.Add("underscore", 0x22c);
            dictionary2.Add("quoteleft", 0x116);
            dictionary2.Add("a", 0x22c);
            dictionary2.Add("b", 0x263);
            dictionary2.Add("c", 0x22c);
            dictionary2.Add("d", 0x263);
            dictionary2.Add("e", 0x22c);
            dictionary2.Add("f", 0x14d);
            dictionary2.Add("g", 0x263);
            dictionary2.Add("h", 0x263);
            dictionary2.Add("i", 0x116);
            dictionary2.Add("j", 0x116);
            dictionary2.Add("k", 0x22c);
            dictionary2.Add("l", 0x116);
            dictionary2.Add("m", 0x379);
            dictionary2.Add("n", 0x263);
            dictionary2.Add("o", 0x263);
            dictionary2.Add("p", 0x263);
            dictionary2.Add("q", 0x263);
            dictionary2.Add("r", 0x185);
            dictionary2.Add("s", 0x22c);
            dictionary2.Add("t", 0x14d);
            dictionary2.Add("u", 0x263);
            dictionary2.Add("v", 0x22c);
            dictionary2.Add("w", 0x30a);
            dictionary2.Add("x", 0x22c);
            dictionary2.Add("y", 0x22c);
            dictionary2.Add("z", 500);
            dictionary2.Add("braceleft", 0x185);
            dictionary2.Add("bar", 280);
            dictionary2.Add("braceright", 0x185);
            dictionary2.Add("asciitilde", 0x248);
            dictionary2.Add("exclamdown", 0x14d);
            dictionary2.Add("cent", 0x22c);
            dictionary2.Add("sterling", 0x22c);
            dictionary2.Add("fraction", 0xa7);
            dictionary2.Add("yen", 0x22c);
            dictionary2.Add("florin", 0x22c);
            dictionary2.Add("section", 0x22c);
            dictionary2.Add("currency", 0x22c);
            dictionary2.Add("quotesingle", 0xee);
            dictionary2.Add("quotedblleft", 500);
            dictionary2.Add("guillemotleft", 0x22c);
            dictionary2.Add("guilsinglleft", 0x14d);
            dictionary2.Add("guilsinglright", 0x14d);
            dictionary2.Add("fi", 0x263);
            dictionary2.Add("fl", 0x263);
            dictionary2.Add("endash", 0x22c);
            dictionary2.Add("dagger", 0x22c);
            dictionary2.Add("daggerdbl", 0x22c);
            dictionary2.Add("periodcentered", 0x116);
            dictionary2.Add("paragraph", 0x22c);
            dictionary2.Add("bullet", 350);
            dictionary2.Add("quotesinglbase", 0x116);
            dictionary2.Add("quotedblbase", 500);
            dictionary2.Add("quotedblright", 500);
            dictionary2.Add("guillemotright", 0x22c);
            dictionary2.Add("ellipsis", 0x3e8);
            dictionary2.Add("perthousand", 0x3e8);
            dictionary2.Add("questiondown", 0x263);
            dictionary2.Add("grave", 0x14d);
            dictionary2.Add("acute", 0x14d);
            dictionary2.Add("circumflex", 0x14d);
            dictionary2.Add("tilde", 0x14d);
            dictionary2.Add("macron", 0x14d);
            dictionary2.Add("breve", 0x14d);
            dictionary2.Add("dotaccent", 0x14d);
            dictionary2.Add("dieresis", 0x14d);
            dictionary2.Add("ring", 0x14d);
            dictionary2.Add("cedilla", 0x14d);
            dictionary2.Add("hungarumlaut", 0x14d);
            dictionary2.Add("ogonek", 0x14d);
            dictionary2.Add("caron", 0x14d);
            dictionary2.Add("emdash", 0x3e8);
            dictionary2.Add("AE", 0x3e8);
            dictionary2.Add("ordfeminine", 370);
            dictionary2.Add("Lslash", 0x263);
            dictionary2.Add("Oslash", 0x30a);
            dictionary2.Add("OE", 0x3e8);
            dictionary2.Add("ordmasculine", 0x16d);
            dictionary2.Add("ae", 0x379);
            dictionary2.Add("dotlessi", 0x116);
            dictionary2.Add("lslash", 0x116);
            dictionary2.Add("oslash", 0x263);
            dictionary2.Add("oe", 0x3b0);
            dictionary2.Add("germandbls", 0x263);
            dictionary2.Add("Idieresis", 0x116);
            dictionary2.Add("eacute", 0x22c);
            dictionary2.Add("abreve", 0x22c);
            dictionary2.Add("uhungarumlaut", 0x263);
            dictionary2.Add("ecaron", 0x22c);
            dictionary2.Add("Ydieresis", 0x29b);
            dictionary2.Add("divide", 0x248);
            dictionary2.Add("Yacute", 0x29b);
            dictionary2.Add("Acircumflex", 0x2d2);
            dictionary2.Add("aacute", 0x22c);
            dictionary2.Add("Ucircumflex", 0x2d2);
            dictionary2.Add("yacute", 0x22c);
            dictionary2.Add("scommaaccent", 0x22c);
            dictionary2.Add("ecircumflex", 0x22c);
            dictionary2.Add("Uring", 0x2d2);
            dictionary2.Add("Udieresis", 0x2d2);
            dictionary2.Add("aogonek", 0x22c);
            dictionary2.Add("Uacute", 0x2d2);
            dictionary2.Add("uogonek", 0x263);
            dictionary2.Add("Edieresis", 0x29b);
            dictionary2.Add("Dcroat", 0x2d2);
            dictionary2.Add("commaaccent", 250);
            dictionary2.Add("copyright", 0x2e1);
            dictionary2.Add("Emacron", 0x29b);
            dictionary2.Add("ccaron", 0x22c);
            dictionary2.Add("aring", 0x22c);
            dictionary2.Add("Ncommaaccent", 0x2d2);
            dictionary2.Add("lacute", 0x116);
            dictionary2.Add("agrave", 0x22c);
            dictionary2.Add("Tcommaaccent", 0x263);
            dictionary2.Add("Cacute", 0x2d2);
            dictionary2.Add("atilde", 0x22c);
            dictionary2.Add("Edotaccent", 0x29b);
            dictionary2.Add("scaron", 0x22c);
            dictionary2.Add("scedilla", 0x22c);
            dictionary2.Add("iacute", 0x116);
            dictionary2.Add("lozenge", 0x1ee);
            dictionary2.Add("Rcaron", 0x2d2);
            dictionary2.Add("Gcommaaccent", 0x30a);
            dictionary2.Add("ucircumflex", 0x263);
            dictionary2.Add("acircumflex", 0x22c);
            dictionary2.Add("Amacron", 0x2d2);
            dictionary2.Add("rcaron", 0x185);
            dictionary2.Add("ccedilla", 0x22c);
            dictionary2.Add("Zdotaccent", 0x263);
            dictionary2.Add("Thorn", 0x29b);
            dictionary2.Add("Omacron", 0x30a);
            dictionary2.Add("Racute", 0x2d2);
            dictionary2.Add("Sacute", 0x29b);
            dictionary2.Add("dcaron", 0x2e7);
            dictionary2.Add("Umacron", 0x2d2);
            dictionary2.Add("uring", 0x263);
            dictionary2.Add("threesuperior", 0x14d);
            dictionary2.Add("Ograve", 0x30a);
            dictionary2.Add("Agrave", 0x2d2);
            dictionary2.Add("Abreve", 0x2d2);
            dictionary2.Add("multiply", 0x248);
            dictionary2.Add("uacute", 0x263);
            dictionary2.Add("Tcaron", 0x263);
            dictionary2.Add("partialdiff", 0x1ee);
            dictionary2.Add("ydieresis", 0x22c);
            dictionary2.Add("Nacute", 0x2d2);
            dictionary2.Add("icircumflex", 0x116);
            dictionary2.Add("Ecircumflex", 0x29b);
            dictionary2.Add("adieresis", 0x22c);
            dictionary2.Add("edieresis", 0x22c);
            dictionary2.Add("cacute", 0x22c);
            dictionary2.Add("nacute", 0x263);
            dictionary2.Add("umacron", 0x263);
            dictionary2.Add("Ncaron", 0x2d2);
            dictionary2.Add("Iacute", 0x116);
            dictionary2.Add("plusminus", 0x248);
            dictionary2.Add("brokenbar", 280);
            dictionary2.Add("registered", 0x2e1);
            dictionary2.Add("Gbreve", 0x30a);
            dictionary2.Add("Idotaccent", 0x116);
            dictionary2.Add("summation", 600);
            dictionary2.Add("Egrave", 0x29b);
            dictionary2.Add("racute", 0x185);
            dictionary2.Add("omacron", 0x263);
            dictionary2.Add("Zacute", 0x263);
            dictionary2.Add("Zcaron", 0x263);
            dictionary2.Add("greaterequal", 0x225);
            dictionary2.Add("Eth", 0x2d2);
            dictionary2.Add("Ccedilla", 0x2d2);
            dictionary2.Add("lcommaaccent", 0x116);
            dictionary2.Add("tcaron", 0x185);
            dictionary2.Add("eogonek", 0x22c);
            dictionary2.Add("Uogonek", 0x2d2);
            dictionary2.Add("Aacute", 0x2d2);
            dictionary2.Add("Adieresis", 0x2d2);
            dictionary2.Add("egrave", 0x22c);
            dictionary2.Add("zacute", 500);
            dictionary2.Add("iogonek", 0x116);
            dictionary2.Add("Oacute", 0x30a);
            dictionary2.Add("oacute", 0x263);
            dictionary2.Add("amacron", 0x22c);
            dictionary2.Add("sacute", 0x22c);
            dictionary2.Add("idieresis", 0x116);
            dictionary2.Add("Ocircumflex", 0x30a);
            dictionary2.Add("Ugrave", 0x2d2);
            dictionary2.Add("Delta", 0x264);
            dictionary2.Add("thorn", 0x263);
            dictionary2.Add("twosuperior", 0x14d);
            dictionary2.Add("Odieresis", 0x30a);
            dictionary2.Add("mu", 0x263);
            dictionary2.Add("igrave", 0x116);
            dictionary2.Add("ohungarumlaut", 0x263);
            dictionary2.Add("Eogonek", 0x29b);
            dictionary2.Add("dcroat", 0x263);
            dictionary2.Add("threequarters", 0x342);
            dictionary2.Add("Scedilla", 0x29b);
            dictionary2.Add("lcaron", 400);
            dictionary2.Add("Kcommaaccent", 0x2d2);
            dictionary2.Add("Lacute", 0x263);
            dictionary2.Add("trademark", 0x3e8);
            dictionary2.Add("edotaccent", 0x22c);
            dictionary2.Add("Igrave", 0x116);
            dictionary2.Add("Imacron", 0x116);
            dictionary2.Add("Lcaron", 0x263);
            dictionary2.Add("onehalf", 0x342);
            dictionary2.Add("lessequal", 0x225);
            dictionary2.Add("ocircumflex", 0x263);
            dictionary2.Add("ntilde", 0x263);
            dictionary2.Add("Uhungarumlaut", 0x2d2);
            dictionary2.Add("Eacute", 0x29b);
            dictionary2.Add("emacron", 0x22c);
            dictionary2.Add("gbreve", 0x263);
            dictionary2.Add("onequarter", 0x342);
            dictionary2.Add("Scaron", 0x29b);
            dictionary2.Add("Scommaaccent", 0x29b);
            dictionary2.Add("Ohungarumlaut", 0x30a);
            dictionary2.Add("degree", 400);
            dictionary2.Add("ograve", 0x263);
            dictionary2.Add("Ccaron", 0x2d2);
            dictionary2.Add("ugrave", 0x263);
            dictionary2.Add("radical", 0x225);
            dictionary2.Add("Dcaron", 0x2d2);
            dictionary2.Add("rcommaaccent", 0x185);
            dictionary2.Add("Ntilde", 0x2d2);
            dictionary2.Add("otilde", 0x263);
            dictionary2.Add("Rcommaaccent", 0x2d2);
            dictionary2.Add("Lcommaaccent", 0x263);
            dictionary2.Add("Atilde", 0x2d2);
            dictionary2.Add("Aogonek", 0x2d2);
            dictionary2.Add("Aring", 0x2d2);
            dictionary2.Add("Otilde", 0x30a);
            dictionary2.Add("zdotaccent", 500);
            dictionary2.Add("Ecaron", 0x29b);
            dictionary2.Add("Iogonek", 0x116);
            dictionary2.Add("kcommaaccent", 0x22c);
            dictionary2.Add("minus", 0x248);
            dictionary2.Add("Icircumflex", 0x116);
            dictionary2.Add("ncaron", 0x263);
            dictionary2.Add("tcommaaccent", 0x14d);
            dictionary2.Add("logicalnot", 0x248);
            dictionary2.Add("odieresis", 0x263);
            dictionary2.Add("udieresis", 0x263);
            dictionary2.Add("notequal", 0x225);
            dictionary2.Add("gcommaaccent", 0x263);
            dictionary2.Add("eth", 0x263);
            dictionary2.Add("zcaron", 500);
            dictionary2.Add("ncommaaccent", 0x263);
            dictionary2.Add("onesuperior", 0x14d);
            dictionary2.Add("imacron", 0x116);
            dictionary2.Add("Euro", 0x22c);
            helveticaBoldWidths = new PdfStandardFontFamily.PdfVariableGlyphWidthProvider(dictionary2);
            widthsProvider = new WidthsProvider();
            descriptorProvider = new DescriptorProvider();
        }

        protected override PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<PdfFontDescriptorData> FontDescriptorProvider =>
            descriptorProvider;

        protected override PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<IPdfGlyphWidthProvider> GlyphWidthProvider =>
            widthsProvider;

        private class DescriptorProvider : PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<PdfFontDescriptorData>
        {
            protected override PdfFontDescriptorData Regular
            {
                get
                {
                    PdfFontDescriptorData data1 = new PdfFontDescriptorData();
                    data1.FontFamily = "Helvetica";
                    data1.Flags = PdfFontFlags.SmallCap | PdfFontFlags.Nonsymbolic;
                    data1.BBox = new PdfRectangle(-166.0, -225.0, 1000.0, 931.0);
                    data1.Ascent = 718.0;
                    data1.Descent = -207.0;
                    data1.CapHeight = 718.0;
                    data1.XHeight = 523.0;
                    data1.StemV = 88.0;
                    data1.StemH = 76.0;
                    return data1;
                }
            }

            protected override PdfFontDescriptorData Italic
            {
                get
                {
                    PdfFontDescriptorData data1 = new PdfFontDescriptorData();
                    data1.FontFamily = "Helvetica";
                    data1.Flags = PdfFontFlags.SmallCap | PdfFontFlags.Italic | PdfFontFlags.Nonsymbolic;
                    data1.BBox = new PdfRectangle(-170.0, -225.0, 1116.0, 931.0);
                    data1.ItalicAngle = -12.0;
                    data1.Ascent = 718.0;
                    data1.Descent = -207.0;
                    data1.CapHeight = 718.0;
                    data1.XHeight = 523.0;
                    data1.StemV = 88.0;
                    data1.StemH = 76.0;
                    return data1;
                }
            }

            protected override PdfFontDescriptorData Bold
            {
                get
                {
                    PdfFontDescriptorData data1 = new PdfFontDescriptorData();
                    data1.FontFamily = "Helvetica";
                    data1.Flags = PdfFontFlags.ForceBold | PdfFontFlags.SmallCap | PdfFontFlags.Nonsymbolic;
                    data1.BBox = new PdfRectangle(-170.0, -228.0, 1003.0, 962.0);
                    data1.Ascent = 718.0;
                    data1.Descent = -207.0;
                    data1.CapHeight = 718.0;
                    data1.XHeight = 532.0;
                    data1.StemV = 140.0;
                    data1.StemH = 118.0;
                    return data1;
                }
            }

            protected override PdfFontDescriptorData BoldItalic
            {
                get
                {
                    PdfFontDescriptorData data1 = new PdfFontDescriptorData();
                    data1.FontFamily = "Helvetica";
                    data1.Flags = PdfFontFlags.ForceBold | PdfFontFlags.SmallCap | PdfFontFlags.Italic | PdfFontFlags.Nonsymbolic;
                    data1.BBox = new PdfRectangle(-174.0, -228.0, 1114.0, 962.0);
                    data1.ItalicAngle = -12.0;
                    data1.Ascent = 718.0;
                    data1.Descent = -207.0;
                    data1.CapHeight = 718.0;
                    data1.XHeight = 532.0;
                    data1.StemV = 140.0;
                    data1.StemH = 118.0;
                    return data1;
                }
            }
        }

        private class WidthsProvider : PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<IPdfGlyphWidthProvider>
        {
            protected override IPdfGlyphWidthProvider Regular =>
                PdfHelveticaFontFamily.helveticaWidths;

            protected override IPdfGlyphWidthProvider Bold =>
                PdfHelveticaFontFamily.helveticaBoldWidths;

            protected override IPdfGlyphWidthProvider BoldItalic =>
                PdfHelveticaFontFamily.helveticaBoldWidths;
        }
    }
}

