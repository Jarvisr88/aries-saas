namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfType1FontCompactFontProgram : PdfType1FontProgram
    {
        public const PdfType1FontType DefaultFontType = PdfType1FontType.Type2;
        private static readonly PdfTransformationMatrix defaultFontMatrix = new PdfTransformationMatrix(0.001, 0.0, 0.0, 0.001, 0.0, 0.0);
        private static readonly PdfRectangle defaultFontBBox = new PdfRectangle(0.0, 0.0, 0.0, 0.0);
        private readonly byte majorVersion;
        private readonly byte minorVersion;
        private readonly PdfCompactFontFormatStringIndex stringIndex;
        private readonly IList<byte[]> globalSubrs;
        private int[] xuid;
        private PdfType1FontCharset charset;
        private PdfType1FontEncoding encoding;
        private IList<byte[]> charStrings;
        private string postScript;
        private double[] baseFontBlend;
        private string cidFontName;
        private PdfType1FontType fontType = PdfType1FontType.Type2;

        internal PdfType1FontCompactFontProgram(byte majorVersion, byte minorVersion, string fontName, PdfCompactFontFormatStringIndex stringIndex, IList<byte[]> globalSubrs)
        {
            this.majorVersion = majorVersion;
            this.minorVersion = minorVersion;
            this.stringIndex = stringIndex;
            this.globalSubrs = globalSubrs;
            base.FontName = fontName;
            base.FontInfo = new PdfType1FontInfo();
            base.PaintType = PdfType1FontPaintType.Filled;
            base.FontMatrix = defaultFontMatrix;
            base.FontBBox = defaultFontBBox;
            this.Charset = new PdfType1FontPredefinedCharset(PdfType1FontPredefinedCharsetID.ISOAdobe);
            this.Encoding = new PdfType1FontPredefinedEncoding(PdfType1FontPredefinedEncodingID.StandardEncoding);
        }

        public override IPdfCodePointMapping GetSimpleMapping(PdfSimpleFontEncoding fontEncoding)
        {
            short[] codeToGIDMapping;
            IDictionary<short, short> sidToGidMapping = this.charset.SidToGidMapping;
            if (!fontEncoding.ShouldUseEmbeddedFontEncoding)
            {
                codeToGIDMapping = new short[0x100];
                for (short i = 0; i < 0x100; i = (short) (i + 1))
                {
                    string glyphName = fontEncoding.GetGlyphName((byte) i);
                    if (glyphName != ".notdef")
                    {
                        short num5;
                        short key = this.stringIndex.TryGetSID(glyphName);
                        if ((key != 0) && sidToGidMapping.TryGetValue(key, out num5))
                        {
                            codeToGIDMapping[i] = num5;
                        }
                    }
                }
            }
            else
            {
                codeToGIDMapping = this.encoding.GetCodeToGIDMapping(this.charset, this.stringIndex);
                foreach (KeyValuePair<int, string> pair in fontEncoding.Differences)
                {
                    short num;
                    short key = this.stringIndex.TryGetSID(pair.Value);
                    if ((key != 0) && sidToGidMapping.TryGetValue(key, out num))
                    {
                        codeToGIDMapping[pair.Key] = num;
                    }
                }
            }
            return new PdfSimpleFontCodePointMapping(codeToGIDMapping, PdfFontProgramFacade.GetUnicodeMapping(fontEncoding));
        }

        public static PdfType1FontCompactFontProgram Parse(byte[] data)
        {
            using (PdfBinaryStream stream = new PdfBinaryStream(data))
            {
                byte majorVersion = stream.ReadByte();
                byte minorVersion = stream.ReadByte();
                stream.Position = stream.ReadByte();
                string[] strings = new PdfCompactFontFormatNameIndex(stream).Strings;
                if (strings.Length != 1)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return PdfCompactFontFormatTopDictIndexParser.Parse(majorVersion, minorVersion, strings[0], new PdfCompactFontFormatStringIndex(stream), new PdfCompactFontFormatBinaryIndex(stream).Data, stream, new PdfCompactFontFormatTopDictIndex(stream).ObjectData);
            }
        }

        internal void SetFontType(PdfType1FontType fontType)
        {
            this.fontType = fontType;
        }

        public virtual bool Validate()
        {
            PdfType1FontPrivateData @private = base.Private;
            bool flag = (@private != null) && @private.Patch();
            if (this.charStrings != null)
            {
                foreach (byte[] buffer in this.charStrings)
                {
                    if ((buffer.Length == 1) && (buffer[0] == 0))
                    {
                        buffer[0] = 14;
                        flag = true;
                    }
                }
            }
            int count = this.globalSubrs.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.globalSubrs[i].Length == 0)
                {
                    this.globalSubrs[i] = new byte[] { 11 };
                    flag = true;
                }
            }
            return flag;
        }

        public static PdfTransformationMatrix DefaultFontMatrix =>
            defaultFontMatrix;

        public static PdfRectangle DefaultFontBBox =>
            defaultFontBBox;

        public byte MajorVersion =>
            this.majorVersion;

        public byte MinorVersion =>
            this.minorVersion;

        public PdfCompactFontFormatStringIndex StringIndex =>
            this.stringIndex;

        public IList<byte[]> GlobalSubrs =>
            this.globalSubrs;

        public int[] XUID
        {
            get => 
                this.xuid;
            internal set => 
                this.xuid = value;
        }

        public PdfType1FontCharset Charset
        {
            get => 
                this.charset;
            internal set => 
                this.charset = value;
        }

        public PdfType1FontEncoding Encoding
        {
            get => 
                this.encoding;
            internal set => 
                this.encoding = value;
        }

        public IList<byte[]> CharStrings
        {
            get => 
                this.charStrings;
            internal set => 
                this.charStrings = value;
        }

        public string PostScript
        {
            get => 
                this.postScript;
            internal set => 
                this.postScript = value;
        }

        public double[] BaseFontBlend
        {
            get => 
                this.baseFontBlend;
            internal set => 
                this.baseFontBlend = value;
        }

        public string CIDFontName
        {
            get => 
                this.cidFontName;
            internal set => 
                this.cidFontName = value;
        }

        public override PdfType1FontType FontType =>
            this.fontType;
    }
}

