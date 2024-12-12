namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class PdfType1FontClassicFontProgram : PdfType1FontProgram
    {
        internal const string SerializationPattern = "/{0} {1} def\n";
        internal const string EncodingDictionaryKey = "Encoding";
        internal const string CharStringsDictionaryKey = "CharStrings";
        private const string fontInfoDictionaryKey = "FontInfo";
        private const string fontNameDictionaryKey = "FontName";
        private const string paintTypeDictionaryKey = "PaintType";
        private const string fontTypeDictionaryKey = "FontType";
        private const string fontMatrixDictionaryKey = "FontMatrix";
        private const string fontBBoxDictionaryKey = "FontBBox";
        private const string uniqueIDDictionaryKey = "UniqueID";
        private const string metricsDictionaryKey = "Metrics";
        private const string strokeWidthDictionaryKey = "StrokeWidth";
        private const string privateDictionaryKey = "Private";
        private const string wModeDictionaryKey = "WMode";
        private readonly List<string> encoding;
        private readonly PdfPostScriptDictionary metrics;
        private readonly PdfPostScriptDictionary charStrings;
        private readonly PdfType1FontWMode wMode;

        public PdfType1FontClassicFontProgram(PdfPostScriptDictionary dictionary)
        {
            PdfType1FontType invalid = PdfType1FontType.Invalid;
            PdfType1FontPaintType paintType = base.PaintType;
            PdfTransformationMatrix matrix = null;
            PdfRectangle rectangle = null;
            foreach (PdfPostScriptDictionaryEntry entry in dictionary)
            {
                string key = entry.Key;
                uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
                if (num > 0x7a026a16)
                {
                    if (num <= 0x9e9c08a9)
                    {
                        if (num == 0x853ef12c)
                        {
                            if (key != "FontInfo")
                            {
                                continue;
                            }
                            PdfPostScriptDictionary dictionary2 = entry.Value as PdfPostScriptDictionary;
                            if (dictionary2 == null)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            base.FontInfo = new PdfType1FontInfo(dictionary2);
                            continue;
                        }
                        if (num == 0x8a1e5e7d)
                        {
                            if (key != "WMode")
                            {
                                continue;
                            }
                            this.wMode = (PdfType1FontWMode) ToInt32(entry.Value);
                            continue;
                        }
                        if (num != 0x9e9c08a9)
                        {
                            continue;
                        }
                        if (key != "FontMatrix")
                        {
                            continue;
                        }
                        matrix = new PdfTransformationMatrix(ToList(entry.Value));
                        base.FontMatrix = matrix;
                        continue;
                    }
                    if (num > 0xc81e129b)
                    {
                        if (num == 0xc835e223)
                        {
                            if (key != "FontBBox")
                            {
                                continue;
                            }
                            rectangle = PdfRectangle.Parse(ToList(entry.Value), null);
                            base.FontBBox = rectangle;
                            continue;
                        }
                        if (num != 0xd01f2cac)
                        {
                            continue;
                        }
                        if (key != "Private")
                        {
                            continue;
                        }
                        base.Private = new PdfType1FontClassicFontPrivateData(ToDictionary(entry.Value));
                        continue;
                    }
                    if (num != 0xa7586998)
                    {
                        if (num != 0xc81e129b)
                        {
                            continue;
                        }
                        if (key != "UniqueID")
                        {
                            continue;
                        }
                        base.UniqueID = ToInt32(entry.Value);
                        continue;
                    }
                    if (key != "FontType")
                    {
                        continue;
                    }
                    if (ToInt32(entry.Value) == 1)
                    {
                        continue;
                    }
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    continue;
                }
                if (num > 0x5065da13)
                {
                    if (num == 0x544182f9)
                    {
                        if (key != "CharStrings")
                        {
                            continue;
                        }
                        this.charStrings = ToDictionary(entry.Value);
                        continue;
                    }
                    if (num == 0x66eeeeef)
                    {
                        if (key != "StrokeWidth")
                        {
                            continue;
                        }
                        base.StrokeWidth = PdfDocumentReader.ConvertToDouble(entry.Value);
                        continue;
                    }
                    if (num != 0x7a026a16)
                    {
                        continue;
                    }
                    if (key != "Metrics")
                    {
                        continue;
                    }
                    this.metrics = ToDictionary(entry.Value);
                    if (this.metrics.Count == 0)
                    {
                        continue;
                    }
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    continue;
                }
                if (num != 0xe2a2510)
                {
                    if (num == 0x32eafdcb)
                    {
                        if (key != "PaintType")
                        {
                            continue;
                        }
                        paintType = (PdfType1FontPaintType) ToInt32(entry.Value);
                        if ((paintType != PdfType1FontPaintType.Filled) && (paintType != PdfType1FontPaintType.Stroked))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        base.PaintType = paintType;
                        continue;
                    }
                    if (num != 0x5065da13)
                    {
                        continue;
                    }
                    if (key != "FontName")
                    {
                        continue;
                    }
                    object obj2 = entry.Value;
                    PdfName name = obj2 as PdfName;
                    if (name != null)
                    {
                        base.FontName = name.Name;
                        continue;
                    }
                    byte[] bytes = obj2 as byte[];
                    if (bytes == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    base.FontName = System.Text.Encoding.UTF8.GetString(bytes);
                    continue;
                }
                if (key == "Encoding")
                {
                    IList<object> list = ToList(entry.Value);
                    if (list == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    int count = list.Count;
                    this.encoding = new List<string>(count);
                    foreach (object obj3 in list)
                    {
                        if (obj3 == null)
                        {
                            this.encoding.Add(".notdef");
                            continue;
                        }
                        PdfName name2 = obj3 as PdfName;
                        if (name2 == null)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        this.encoding.Add(name2.Name);
                    }
                }
            }
            if ((this.encoding == null) || ((invalid == PdfType1FontType.Invalid) || ((paintType == PdfType1FontPaintType.Invalid) || ((matrix == null) || ((rectangle == null) || ((this.wMode != PdfType1FontWMode.Horizontal) && (this.wMode != PdfType1FontWMode.Vertical)))))))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        public static byte[] ConvertToCFF(string fontName, PdfType1FontFileData fontFileData)
        {
            PdfType1FontClassicFontProgram program = Create(fontName, fontFileData);
            string[] strings = new string[] { fontName };
            PdfCompactFontFormatStringIndex stringIndex = new PdfCompactFontFormatStringIndex(strings);
            PdfType1FontCompactFontProgram fontProgram = new PdfType1FontCompactFontProgram(1, 0, fontName, stringIndex, new List<byte[]>());
            PdfType1FontCompactFontPrivateData data = new PdfType1FontCompactFontPrivateData();
            PdfType1FontPrivateData @private = program.Private;
            data.BlueFuzz = @private.BlueFuzz;
            data.BlueScale = @private.BlueScale;
            data.BlueShift = @private.BlueShift;
            data.BlueValues = @private.BlueValues;
            data.DefaultWidthX = 1000.0;
            data.ExpansionFactor = @private.ExpansionFactor;
            data.FamilyBlues = @private.FamilyBlues;
            data.FamilyOtherBlues = @private.FamilyOtherBlues;
            data.ForceBold = @private.ForceBold;
            data.ForceBoldThreshold = @private.ForceBoldThreshold;
            data.LanguageGroup = @private.LanguageGroup;
            data.NominalWidthX = 1000.0;
            data.OtherBlues = @private.OtherBlues;
            data.StdHW = @private.StdHW;
            data.StdVW = @private.StdVW;
            data.StemSnapH = @private.StemSnapH;
            data.StemSnapV = @private.StemSnapV;
            fontProgram.Private = program.Private;
            fontProgram.FontBBox = program.FontBBox;
            fontProgram.FontMatrix = program.FontMatrix;
            fontProgram.StrokeWidth = program.StrokeWidth;
            fontProgram.UniqueID = program.UniqueID;
            fontProgram.Private = data;
            fontProgram.FontInfo = new PdfType1FontInfo();
            IList<byte[]> list = new List<byte[]>();
            PdfType1FontClassicFontPrivateData data3 = program.Private as PdfType1FontClassicFontPrivateData;
            int lenIV = (data3 == null) ? 4 : data3.LenIV;
            IDictionary<string, PdfType1GlyphDescription> fontGlyphDescriptions = new Dictionary<string, PdfType1GlyphDescription>();
            IList<PdfType1GlyphDescription> list2 = new List<PdfType1GlyphDescription>();
            foreach (PdfPostScriptDictionaryEntry entry in program.CharStrings)
            {
                PdfType1GlyphDescription description = PdfType1GlyphDescriptionParser.ParseDescription(program, PdfType1FontEexecCipher.DecryptCharstring((byte[]) entry.Value, lenIV));
                if (!fontGlyphDescriptions.ContainsKey(entry.Key))
                {
                    fontGlyphDescriptions.Add(entry.Key, description);
                }
                if (entry.Key != ".notdef")
                {
                    list2.Add(description);
                }
            }
            list.Add(PdfType1FontProgram.EmptyCharstring);
            foreach (PdfType1GlyphDescription description2 in list2)
            {
                list.Add(description2.ConvertToType2Charstring(fontGlyphDescriptions));
            }
            fontProgram.CharStrings = list;
            fontProgram.Validate();
            short[] array = new short[list.Count - 1];
            byte[] buffer = new byte[list.Count - 1];
            int num2 = 1;
            int index = 0;
            while (num2 < array.Length)
            {
                array[index] = (short) num2;
                buffer[index++] = (byte) num2;
                num2++;
            }
            using (PdfBinaryStream stream = new PdfBinaryStream())
            {
                stream.WriteShortArray(array);
                stream.Position = 0L;
                fontProgram.Charset = new PdfType1FontArrayCharset(stream, array.Length);
            }
            using (PdfBinaryStream stream2 = new PdfBinaryStream())
            {
                stream2.WriteByte((byte) array.Length);
                stream2.WriteArray(buffer);
                stream2.Position = 0L;
                fontProgram.Encoding = new PdfType1FontArrayEncoding(stream2);
            }
            return PdfCompactFontFormatTopDictIndexWriter.Write(fontProgram);
        }

        public static PdfType1FontClassicFontProgram Create(string fontName, PdfType1FontFileData fontFileData)
        {
            PdfType1FontClassicFontProgram program = Create(fontName, fontFileData, false);
            return ((program == null) ? Create(fontName, fontFileData, true) : program);
        }

        private static PdfType1FontClassicFontProgram Create(string fontName, PdfType1FontFileData fontFileData, bool forceCharstringTermination)
        {
            try
            {
                byte[] data = fontFileData.Data;
                int plainTextLength = fontFileData.PlainTextLength;
                PdfPostScriptInterpreter interpreter = new PdfPostScriptInterpreter(forceCharstringTermination);
                interpreter.Execute((IEnumerable<object>) PdfPostScriptFileParser.Parse(data, plainTextLength));
                PdfStack stack = interpreter.Stack;
                if ((stack.Count > 0) && (stack.Peek() is PdfPostScriptDictionary))
                {
                    interpreter.Execute(PdfType1FontEexecCipher.Create(data, plainTextLength, fontFileData.CipherTextLength).Decrypt());
                    PdfPostScriptDictionary fontDirectory = interpreter.FontDirectory;
                    PdfType1FontClassicFontProgram program = fontDirectory[fontName] as PdfType1FontClassicFontProgram;
                    if (program == null)
                    {
                        IEnumerator<PdfPostScriptDictionaryEntry> enumerator = fontDirectory.GetEnumerator();
                        if (enumerator.MoveNext())
                        {
                            return (enumerator.Current.Value as PdfType1FontClassicFontProgram);
                        }
                    }
                    else
                    {
                        return program;
                    }
                }
            }
            catch
            {
            }
            return null;
        }

        public override IPdfCodePointMapping GetCompositeMapping(short[] cidToGidMap) => 
            new PdfCompositeFontCodePointMapping(cidToGidMap, null);

        public override IPdfCodePointMapping GetSimpleMapping(PdfSimpleFontEncoding fontEncoding)
        {
            Dictionary<string, short> dictionary = new Dictionary<string, short> {
                { 
                    ".notdef",
                    0
                }
            };
            short num = 1;
            foreach (PdfPostScriptDictionaryEntry entry in this.charStrings)
            {
                string key = entry.Key;
                if (key != ".notdef")
                {
                    if (!dictionary.ContainsKey(key))
                    {
                        dictionary.Add(key, num);
                    }
                    num = (short) (num + 1);
                }
            }
            short[] glyphIndicesMapping = new short[0x100];
            if (!fontEncoding.ShouldUseEmbeddedFontEncoding)
            {
                for (short i = 0; i < 0x100; i = (short) (i + 1))
                {
                    string glyphName = fontEncoding.GetGlyphName((byte) i);
                    if ((glyphName != ".notdef") && dictionary.TryGetValue(glyphName, out num))
                    {
                        glyphIndicesMapping[(byte) i] = num;
                    }
                }
            }
            else
            {
                int count = this.encoding.Count;
                Dictionary<byte, string> dictionary2 = new Dictionary<byte, string>(count);
                short num3 = 0;
                while (true)
                {
                    if (num3 >= count)
                    {
                        foreach (KeyValuePair<int, string> pair in fontEncoding.Differences)
                        {
                            if (dictionary.TryGetValue(pair.Value, out num))
                            {
                                glyphIndicesMapping[(byte) pair.Key] = num;
                            }
                        }
                        break;
                    }
                    string key = this.encoding[num3];
                    if (key != ".notdef")
                    {
                        byte index = (byte) num3;
                        dictionary2[index] = key;
                        if (dictionary.TryGetValue(key, out num))
                        {
                            glyphIndicesMapping[index] = num;
                        }
                    }
                    num3 = (short) (num3 + 1);
                }
            }
            return new PdfSimpleFontCodePointMapping(glyphIndicesMapping, PdfFontProgramFacade.GetUnicodeMapping(fontEncoding));
        }

        public IList<PdfType1CharstringSubroutine> GetSubroutineArray()
        {
            IList<PdfType1CharstringSubroutine> list = new List<PdfType1CharstringSubroutine>();
            PdfType1FontClassicFontPrivateData @private = (PdfType1FontClassicFontPrivateData) base.Private;
            if (@private.Subrs != null)
            {
                foreach (object obj2 in @private.Subrs)
                {
                    list.Add(new PdfType1CharstringSubroutine((byte[]) obj2, @private.LenIV));
                }
            }
            return list;
        }

        private static PdfPostScriptDictionary ToDictionary(object value)
        {
            PdfPostScriptDictionary dictionary = value as PdfPostScriptDictionary;
            if (dictionary == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return dictionary;
        }

        internal static int ToInt32(object value)
        {
            if (!(value is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (int) value;
        }

        internal static IList<object> ToList(object value)
        {
            IList<object> list = value as IList<object>;
            if (list == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return list;
        }

        public string ToPostScript()
        {
            StringBuilder builder = new StringBuilder();
            string fontName = base.FontName;
            PdfType1FontInfo fontInfo = base.FontInfo;
            builder.Append($"%!FontType1-1.0: {fontName} {(fontInfo == null) ? "001.001" : fontInfo.Version}
");
            builder.Append("11 dict begin\n");
            if (fontInfo != null)
            {
                builder.Append($"/{"FontInfo"} {fontInfo.Serialize()} readonly def
");
            }
            builder.Append($"/{"FontName"} /{fontName} def
");
            builder.Append($"/{"Encoding"} 256 array 0 1 255 {{1 index exch /.notdef put}} for
");
            int count = this.encoding.Count;
            for (int i = 0; i < count; i++)
            {
                string str2 = this.encoding[i];
                if (str2 != ".notdef")
                {
                    builder.Append($"dup {i} /{str2} put
");
                }
            }
            builder.Append("readonly def\n");
            builder.Append($"/{"PaintType"} {(int) base.PaintType} def
");
            builder.Append($"/{"FontType"} {(int) this.FontType} def
");
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            PdfTransformationMatrix fontMatrix = base.FontMatrix;
            object[] args = new object[] { "FontMatrix", fontMatrix.A, fontMatrix.B, fontMatrix.C, fontMatrix.D, fontMatrix.E, fontMatrix.F };
            builder.Append(string.Format(invariantCulture, "/{0} [{1} {2} {3} {4} {5} {6}] readonly def\n", args));
            PdfRectangle fontBBox = base.FontBBox;
            object[] objArray2 = new object[] { "FontBBox", fontBBox.Left, fontBBox.Bottom, fontBBox.Right, fontBBox.Top };
            builder.Append(string.Format(invariantCulture, "/{0} {{{1} {2} {3} {4}}} readonly def\n", objArray2));
            int uniqueID = base.UniqueID;
            if (uniqueID != 0)
            {
                builder.Append($"/{"UniqueID"} {uniqueID} def
");
            }
            if (this.metrics != null)
            {
                builder.Append($"/{"Metrics"} 1 dict dup begin
");
                builder.Append("end def\n");
            }
            double strokeWidth = base.StrokeWidth;
            if (strokeWidth != 0.0)
            {
                builder.Append($"/{"StrokeWidth"} {strokeWidth} def
");
            }
            if (this.wMode != PdfType1FontWMode.Horizontal)
            {
                builder.Append($"/{"WMode"} {(int) this.wMode} def
");
            }
            builder.Append("currentdict end\n");
            builder.Append("currentfile eexec\n");
            return builder.ToString();
        }

        public void Validate(PdfFont font)
        {
            PdfRectangle fontBBox = base.FontBBox;
            if ((fontBBox.Left == 0.0) && ((fontBBox.Right == 0.0) && ((fontBBox.Bottom == 0.0) && (fontBBox.Top == 0.0))))
            {
                base.FontBBox = font.FontDescriptor.FontBBox;
            }
            base.FontName = font.RegistrationName;
        }

        public List<string> Encoding =>
            this.encoding;

        public PdfPostScriptDictionary Metrics =>
            this.metrics;

        public PdfPostScriptDictionary CharStrings =>
            this.charStrings;

        public PdfType1FontWMode WMode =>
            this.wMode;

        public override PdfType1FontType FontType =>
            PdfType1FontType.Type1;
    }
}

