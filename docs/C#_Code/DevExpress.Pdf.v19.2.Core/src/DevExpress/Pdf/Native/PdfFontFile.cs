namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PdfFontFile : PdfDisposableObject
    {
        private const int tableDirectoryOffset = 12;
        private static readonly byte[] openTypeVersion = new byte[] { 0x4f, 0x54, 0x54, 0x4f };
        private static readonly byte[] ttfVersion;
        private static readonly SortedSet<string> trueTypeSubsetTableTags;
        private static readonly SortedSet<string> cffSubsetTableTags;
        private byte[] version;
        private readonly SortedDictionary<string, PdfFontTableDirectoryEntry> tableDictionary;
        private long initalFontSize;
        private PdfFontHheaTableDirectoryEntry hhea;
        private PdfFontCmapTableDirectoryEntry cMap;
        private PdfFontKernTableDirectoryEntry kern;
        private PdfFontHmtxTableDirectoryEntry hmtx;
        private float ttfToPdfFactor;

        static PdfFontFile()
        {
            byte[] buffer2 = new byte[4];
            buffer2[1] = 1;
            ttfVersion = buffer2;
            SortedSet<string> set1 = new SortedSet<string>();
            set1.Add("head");
            set1.Add("hhea");
            set1.Add("maxp");
            set1.Add("hmtx");
            set1.Add("glyf");
            set1.Add("loca");
            set1.Add("cvt ");
            set1.Add("fpgm");
            set1.Add("prep");
            trueTypeSubsetTableTags = set1;
            SortedSet<string> set2 = new SortedSet<string>();
            set2.Add("head");
            set2.Add("hhea");
            set2.Add("maxp");
            set2.Add("hmtx");
            set2.Add("CFF ");
            set2.Add("cvt ");
            set2.Add("fpgm");
            set2.Add("prep");
            cffSubsetTableTags = set2;
        }

        public PdfFontFile(PdfBinaryStream stream)
        {
            this.tableDictionary = new SortedDictionary<string, PdfFontTableDirectoryEntry>();
            this.ttfToPdfFactor = 0.4882813f;
            this.version = TTFVersion;
            this.ReadTables(stream);
        }

        public PdfFontFile(PdfCFFFontProgramFacade compactFontProgram, PdfFont font)
        {
            this.tableDictionary = new SortedDictionary<string, PdfFontTableDirectoryEntry>();
            this.ttfToPdfFactor = 0.4882813f;
            this.version = openTypeVersion;
            PdfFontCmapTableDirectoryEntry table = new PdfFontCmapTableDirectoryEntry(compactFontProgram.Charset);
            PdfRectangle fontBBox = compactFontProgram.FontBBox;
            IList<PdfPoint> points = new List<PdfPoint>();
            if (fontBBox != null)
            {
                points.Add(fontBBox.TopLeft);
                points.Add(fontBBox.TopRight);
                points.Add(fontBBox.BottomLeft);
                points.Add(fontBBox.BottomRight);
            }
            PdfFontDescriptor fontDescriptor = font.FontDescriptor;
            if (fontDescriptor != null)
            {
                PdfRectangle rectangle3 = fontDescriptor.FontBBox;
                points.Add(rectangle3.TopLeft);
                points.Add(rectangle3.TopRight);
                points.Add(rectangle3.BottomLeft);
                points.Add(rectangle3.BottomRight);
                double y = fontDescriptor.Ascent;
                double num4 = fontDescriptor.Descent;
                points.Add(new PdfPoint(rectangle3.Left, y));
                points.Add(new PdfPoint(rectangle3.Right, num4));
            }
            PdfRectangle objA = PdfRectangle.CreateBoundingBox(points);
            if (objA != null)
            {
                if (objA.Right <= 0.0)
                {
                    objA = new PdfRectangle(0.0, objA.Bottom, 0.0, objA.Top);
                }
                if (objA.Width == 0.0)
                {
                    double num5 = font.GlyphWidths.Max();
                    objA = new PdfRectangle(objA.Left, objA.Bottom, objA.Left + num5, objA.Top);
                }
            }
            if ((objA == null) || (ReferenceEquals(objA, PdfType1FontCompactFontProgram.DefaultFontBBox) || ((objA.Width == 0.0) || (objA.Height == 0.0))))
            {
                objA = new PdfRectangle(-32768.0, -32768.0, 32767.0, 32767.0);
            }
            short descent = (short) Math.Max(-32768.0, Math.Floor(objA.Bottom));
            short ascent = (short) Math.Min(32767.0, Math.Ceiling(objA.Top));
            this.AddTable(new PdfOpenTypeCFFTableDirectoryEntry(compactFontProgram.FontFileData));
            this.AddTable(new PdfFontOS2TableDirectoryEntry(font, compactFontProgram.Charset, ascent, descent));
            this.AddTable(table);
            this.AddTable(new PdfFontHeadTableDirectoryEntry(objA));
            this.AddTable(new PdfFontHheaTableDirectoryEntry(font, compactFontProgram.GlyphCount, ascent, descent));
            this.AddTable(new PdfFontHmtxTableDirectoryEntry(compactFontProgram.GlyphCount));
            this.AddTable(new PdfFontMaxpTableDirectoryEntry(compactFontProgram.GlyphCount));
            this.AddTable(new PdfFontNameTableDirectoryEntry(table, font.RegistrationName));
            this.AddTable(new PdfFontPostTableDirectoryEntry(font));
        }

        public PdfFontFile(byte[] version, byte[] data)
        {
            this.tableDictionary = new SortedDictionary<string, PdfFontTableDirectoryEntry>();
            this.ttfToPdfFactor = 0.4882813f;
            this.version = version;
            using (PdfBinaryStream stream = new PdfBinaryStream(data))
            {
                this.ReadTables(stream);
            }
        }

        public void AddTable(PdfFontTableDirectoryEntry table)
        {
            this.tableDictionary[table.Tag] = table;
        }

        public static bool AreEqual(PdfFontFile fontFile1, PdfFontFile fontFile2) => 
            ((fontFile1 != null) || (fontFile2 != null)) ? (((fontFile1 != null) || (fontFile2 == null)) ? (((fontFile1 == null) || (fontFile2 != null)) && (fontFile1.initalFontSize == fontFile2.initalFontSize)) : false) : false;

        private static bool CheckVersion(byte[] fontFileData, byte[] versionData) => 
            (fontFileData.Length >= versionData.Length) && ((fontFileData[0] == versionData[0]) && ((fontFileData[1] == versionData[1]) && ((fontFileData[2] == versionData[2]) && (fontFileData[3] == versionData[3]))));

        public PdfFontFileSubset CreateSubset(ICollection<int> glyphIndices)
        {
            PdfTrueTypeGlyfTableDirectoryEntry glyf = this.Glyf;
            PdfOpenTypeCFFTableDirectoryEntry cFF = this.CFF;
            byte[] buffer = null;
            PdfFontFileSubsetType trueType = PdfFontFileSubsetType.TrueType;
            if (glyf != null)
            {
                glyf.CreateSubset(this, glyphIndices);
                this.version = ttfVersion;
                buffer = this.GetData(trueTypeSubsetTableTags);
                trueType = PdfFontFileSubsetType.TrueType;
            }
            else if (cFF != null)
            {
                try
                {
                    cFF.CreateSubset(glyphIndices);
                    this.version = openTypeVersion;
                    buffer = this.GetData(cffSubsetTableTags);
                    trueType = PdfFontFileSubsetType.CFF;
                }
                catch
                {
                    buffer = this.GetData(trueTypeSubsetTableTags);
                    trueType = PdfFontFileSubsetType.TrueType;
                }
            }
            byte[] data = buffer;
            if (buffer == null)
            {
                byte[] local2 = buffer;
                data = this.GetData(trueTypeSubsetTableTags);
            }
            return new PdfFontFileSubset(trueType, data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (KeyValuePair<string, PdfFontTableDirectoryEntry> pair in this.tableDictionary)
                {
                    pair.Value.Dispose();
                }
            }
        }

        public static byte[] GetCFFData(byte[] fontFileData)
        {
            if (IsOpenType(fontFileData))
            {
                using (PdfFontFile file = new PdfFontFile(openTypeVersion, fontFileData))
                {
                    PdfFontTableDirectoryEntry table = file.GetTable<PdfOpenTypeCFFTableDirectoryEntry>("CFF ");
                    if (table != null)
                    {
                        return table.TableData;
                    }
                }
            }
            return null;
        }

        public float GetCharacterWidth(int glyphIndex)
        {
            PdfFontHmtxTableDirectoryEntry hmtx = this.Hmtx;
            if (hmtx == null)
            {
                return 0f;
            }
            short[] advanceWidths = hmtx.AdvanceWidths;
            if (advanceWidths == null)
            {
                PdfFontHheaTableDirectoryEntry hhea = this.Hhea;
                if (hhea == null)
                {
                    return 0f;
                }
                PdfFontMaxpTableDirectoryEntry maxp = this.Maxp;
                advanceWidths = hmtx.FillAdvanceWidths(hhea.NumberOfHMetrics, (maxp == null) ? 0 : maxp.NumGlyphs);
            }
            return ((glyphIndex < advanceWidths.Length) ? (advanceWidths[glyphIndex] * this.ttfToPdfFactor) : 0f);
        }

        public byte[] GetData() => 
            this.GetData(this.tableDictionary.Keys);

        private byte[] GetData(ICollection<string> tablesToWrite)
        {
            short num = 0;
            foreach (KeyValuePair<string, PdfFontTableDirectoryEntry> pair in this.tableDictionary)
            {
                if (tablesToWrite.Contains(pair.Key))
                {
                    num = (short) (num + 1);
                }
            }
            int offset = 12 + (num * 0x10);
            using (PdfBinaryStream stream = new PdfBinaryStream())
            {
                stream.WriteArray(this.version);
                stream.WriteShort(num);
                short num3 = Convert.ToInt16(Math.Pow(2.0, Math.Floor(Math.Log((double) num, 2.0))));
                short num4 = (short) (num3 * 0x10);
                stream.WriteShort(num4);
                stream.WriteShort(Convert.ToInt16(Math.Log((double) num3, 2.0)));
                stream.WriteShort((short) ((num * 0x10) - num4));
                foreach (KeyValuePair<string, PdfFontTableDirectoryEntry> pair2 in this.tableDictionary)
                {
                    if (tablesToWrite.Contains(pair2.Key))
                    {
                        offset += pair2.Value.Write(stream, offset);
                    }
                }
                foreach (KeyValuePair<string, PdfFontTableDirectoryEntry> pair3 in this.tableDictionary)
                {
                    if (tablesToWrite.Contains(pair3.Key))
                    {
                        stream.WriteArray(pair3.Value.AlignedTableData);
                    }
                }
                stream.Position = 0L;
                return stream.ReadArray((int) stream.Length);
            }
        }

        public IPdfCodePointMapping GetSimpleMapping(PdfSimpleFontEncoding encoding, bool isSymbolic)
        {
            PdfFontCmapTableDirectoryEntry cMap = this.CMap;
            PdfFontPostTableDirectoryEntry post = this.Post;
            PdfFontCmapFormatEntry entry3 = null;
            PdfFontCmapFormatEntry item = null;
            PdfFontCmapFormatEntry entry5 = null;
            PdfFontCmapFormatEntry entry6 = null;
            IList<string> glyphNames = post?.GlyphNames;
            if ((cMap != null) && (cMap.CMapTables != null))
            {
                foreach (PdfFontCmapFormatEntry entry7 in cMap.CMapTables)
                {
                    entry6 ??= entry7;
                    if (entry7.PlatformId != PdfFontPlatformID.Microsoft)
                    {
                        if ((entry7.PlatformId != PdfFontPlatformID.Macintosh) || (entry7.EncodingId != PdfFontEncodingID.Symbol))
                        {
                            continue;
                        }
                        entry5 = entry7;
                        continue;
                    }
                    if (entry7.EncodingId == PdfFontEncodingID.Symbol)
                    {
                        item = entry7;
                        continue;
                    }
                    if (entry7.EncodingId == PdfFontEncodingID.UGL)
                    {
                        entry3 = entry7;
                    }
                }
            }
            short[] unicodeMapping = new short[0x100];
            short[] glyphIndicesMapping = new short[0x100];
            IDictionary<string, ushort> glyphCodes = PdfUnicodeConverter.GlyphCodes;
            bool flag = false;
            for (int i = 0; i < 0x100; i++)
            {
                short num2 = 0;
                string glyphName = encoding.GetGlyphName((byte) i);
                if (!isSymbolic)
                {
                    short glyphCode = 0;
                    bool flag2 = PdfUnicodeConverter.TryGetGlyphCode(glyphName, out glyphCode);
                    if (!flag2 && (glyphNames == null))
                    {
                        glyphCode = (short) i;
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        unicodeMapping[i] = glyphCode;
                        if (entry3 != null)
                        {
                            num2 = (short) entry3.MapCode((char) ((ushort) glyphCode));
                        }
                        else if (entry5 != null)
                        {
                            byte num4;
                            num2 = !PdfSimpleFontEncoding.MacReversedEncoding.TryGetValue(glyphName, out num4) ? ((short) entry5.MapCode((char) ((ushort) glyphCode))) : ((short) entry5.MapCode((char) num4));
                        }
                        else if (entry6 != null)
                        {
                            num2 = (short) entry6.MapCode((char) i);
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }
                else
                {
                    List<PdfFontCmapFormatEntry> list2 = new List<PdfFontCmapFormatEntry>();
                    if (item != null)
                    {
                        list2.Add(item);
                    }
                    if (entry5 != null)
                    {
                        list2.Add(entry5);
                    }
                    if (entry6 != null)
                    {
                        list2.Add(entry6);
                    }
                    if (list2.Count == 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        foreach (PdfFontCmapFormatEntry entry8 in list2)
                        {
                            num2 = (short) entry8.MapCode((char) i);
                            if (num2 != 0)
                            {
                                break;
                            }
                        }
                    }
                    unicodeMapping[i] = (short) i;
                }
                if ((num2 == 0) && (isSymbolic || (glyphName != ".notdef")))
                {
                    int index = -1;
                    if (glyphNames != null)
                    {
                        index = glyphNames.IndexOf(glyphName);
                    }
                    num2 = (index != -1) ? ((short) index) : (!flag ? 0 : ((short) i));
                }
                glyphIndicesMapping[i] = num2;
            }
            return new PdfSimpleFontCodePointMapping(glyphIndicesMapping, unicodeMapping);
        }

        private T GetTable<T>(string key) where T: PdfFontTableDirectoryEntry
        {
            PdfFontTableDirectoryEntry entry;
            if (this.tableDictionary.TryGetValue(key, out entry))
            {
                return (entry as T);
            }
            return default(T);
        }

        public static byte[] GetValidCFFData(byte[] fontFileData)
        {
            if ((fontFileData.Length == 0) || (fontFileData[0] == 1))
            {
                return fontFileData;
            }
            byte[] cFFData = GetCFFData(fontFileData);
            if (cFFData == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return cFFData;
        }

        public static bool IsOpenType(byte[] fontFileData) => 
            CheckVersion(fontFileData, openTypeVersion);

        public static bool IsTrueType(byte[] fontFileData) => 
            CheckVersion(fontFileData, ttfVersion);

        private void ReadTables(PdfBinaryStream stream)
        {
            this.initalFontSize = stream.Length;
            long position = stream.Position;
            stream.ReadInt();
            short num2 = stream.ReadShort();
            stream.ReadShort();
            stream.ReadShort();
            stream.Position = position + 12;
            long length = stream.Length;
            for (short i = 0; i < num2; i = (short) (i + 1))
            {
                string tag = stream.ReadString(4);
                stream.ReadInt();
                long num5 = stream.ReadInt();
                int num6 = stream.ReadInt();
                if (tag == "glyf")
                {
                    num6 ??= ((int) (length - num5));
                }
                if ((tag != "EBLC") && ((num5 > 0L) && ((num5 < length) && (num6 > 0))))
                {
                    long num7 = stream.Position;
                    stream.Position = num5;
                    this.AddTable(PdfFontTableDirectoryEntry.Create(tag, stream.ReadArray(Math.Min(num6, (int) (length - num5)))));
                    stream.Position = num7;
                }
            }
            PdfFontHeadTableDirectoryEntry head = this.Head;
            if (head != null)
            {
                this.ttfToPdfFactor = 1000f / ((float) head.UnitsPerEm);
            }
            PdfTrueTypeLocaTableDirectoryEntry loca = this.Loca;
            if (loca != null)
            {
                loca.ReadOffsets(this);
            }
            PdfTrueTypeGlyfTableDirectoryEntry glyf = this.Glyf;
            if (glyf != null)
            {
                glyf.ReadGlyphs(this);
            }
        }

        public static byte[] TTFVersion =>
            ttfVersion;

        internal SortedDictionary<string, PdfFontTableDirectoryEntry> TableDictionary =>
            this.tableDictionary;

        public bool IsTrueTypeFont =>
            this.Glyf != null;

        public PdfFontHeadTableDirectoryEntry Head =>
            this.GetTable<PdfFontHeadTableDirectoryEntry>("head");

        public PdfFontMaxpTableDirectoryEntry Maxp =>
            this.GetTable<PdfFontMaxpTableDirectoryEntry>("maxp");

        public PdfFontOS2TableDirectoryEntry OS2 =>
            this.GetTable<PdfFontOS2TableDirectoryEntry>("OS/2");

        public PdfFontPostTableDirectoryEntry Post =>
            this.GetTable<PdfFontPostTableDirectoryEntry>("post");

        public PdfFontNameTableDirectoryEntry Name =>
            this.GetTable<PdfFontNameTableDirectoryEntry>("name");

        public PdfTrueTypeLocaTableDirectoryEntry Loca =>
            this.GetTable<PdfTrueTypeLocaTableDirectoryEntry>("loca");

        public PdfTrueTypeGlyfTableDirectoryEntry Glyf =>
            this.GetTable<PdfTrueTypeGlyfTableDirectoryEntry>("glyf");

        public PdfOpenTypeCFFTableDirectoryEntry CFF =>
            this.GetTable<PdfOpenTypeCFFTableDirectoryEntry>("CFF ");

        public PdfFontHheaTableDirectoryEntry Hhea
        {
            get
            {
                this.hhea ??= this.GetTable<PdfFontHheaTableDirectoryEntry>("hhea");
                return this.hhea;
            }
        }

        public PdfFontCmapTableDirectoryEntry CMap
        {
            get
            {
                this.cMap ??= this.GetTable<PdfFontCmapTableDirectoryEntry>("cmap");
                return this.cMap;
            }
        }

        public PdfFontKernTableDirectoryEntry Kern
        {
            get
            {
                this.kern ??= this.GetTable<PdfFontKernTableDirectoryEntry>("kern");
                return this.kern;
            }
        }

        public PdfFontHmtxTableDirectoryEntry Hmtx
        {
            get
            {
                this.hmtx ??= this.GetTable<PdfFontHmtxTableDirectoryEntry>("hmtx");
                return this.hmtx;
            }
        }
    }
}

