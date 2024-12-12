namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfExportFullTrustFont : PdfExportFont
    {
        private const char space = ' ';
        private const char nonBreakingSpace = '\x00a0';
        private static BitArray complexChars = new BitArray(0xffff);
        private readonly Dictionary<int, int> mappedGlyphsCache;
        private readonly PdfFontFile fontFile;
        private readonly float unitsPerEm;
        private readonly List<PdfFontCmapFormatEntry> cmapTables;
        private readonly Lazy<DXCTLShaper> ctlShaper;

        static PdfExportFullTrustFont()
        {
            AddComplexScriptRange(0x300, 0x36f);
            AddComplexScriptRange(0x1dc0, 0x1dff);
            AddComplexScriptRange(0x590, 0x5ff);
            AddComplexScriptRange(0xfb1d, 0xfb4f);
            AddComplexScriptRange(0xfb50, 0xfdff);
            AddComplexScriptRange(0xfe70, 0xfeff);
            AddComplexScriptRange(0x600, 0x6ff);
            AddComplexScriptRange(0x750, 0x77f);
            AddComplexScriptRange(0x700, 0x74f);
            AddComplexScriptRange(0x780, 0x7bf);
            AddComplexScriptRange(0x900, 0x97f);
            AddComplexScriptRange(0x1cd0, 0x1cff);
            AddComplexScriptRange(0xa8e0, 0xa8ff);
            AddComplexScriptRange(0x980, 0x9ff);
            AddComplexScriptRange(0xa00, 0xa7f);
            AddComplexScriptRange(0xa80, 0xaff);
            AddComplexScriptRange(0xb00, 0xb7f);
            AddComplexScriptRange(0xb80, 0xbff);
            AddComplexScriptRange(0xc00, 0xc7f);
            AddComplexScriptRange(0xc80, 0xcff);
            AddComplexScriptRange(0xd00, 0xd7f);
            AddComplexScriptRange(0xd80, 0xdff);
            AddComplexScriptRange(0xe00, 0xe7f);
            AddComplexScriptRange(0xe80, 0xeff);
            AddComplexScriptRange(0xf00, 0xfff);
            AddComplexScriptRange(0x1000, 0x109f);
            AddComplexScriptRange(0x1100, 0x11ff);
            AddComplexScriptRange(0x1200, 0x139f);
            AddComplexScriptRange(0x2d80, 0x2ddf);
            AddComplexScriptRange(0x13a0, 0x13ff);
            AddComplexScriptRange(0x1400, 0x167f);
            AddComplexScriptRange(0x18b0, 0x18ff);
            AddComplexScriptRange(0x1680, 0x169f);
            AddComplexScriptRange(0x16a0, 0x16f0);
            AddComplexScriptRange(0x1780, 0x17ff);
            AddComplexScriptRange(0x19e0, 0x19ff);
            AddComplexScriptRange(0x1800, 0x18af);
            AddComplexScriptRange(0x3130, 0x318f);
            AddComplexScriptRange(0x3200, 0x321f);
            AddComplexScriptRange(0x3260, 0x327f);
            AddComplexScriptRange(0xa960, 0xa97f);
            AddComplexScriptRange(0xac00, 0xd7a3);
            AddComplexScriptRange(0xd7b0, 0xd7ff);
            AddComplexScriptRange(0xffa0, 0xffdf);
            AddComplexScriptRange(0xa500, 0xa63f);
            AddComplexScriptRange(0xa840, 0xa87f);
            AddComplexScriptRange(0xaa60, 0xaa7f);
            AddComplexScriptRange(0x2800, 0x28ff);
            AddComplexScriptRange(0xd800, 0xdbfe);
            AddComplexScriptRange(0xdc01, 0xdfff);
        }

        public PdfExportFullTrustFont(IPdfExportPlatformFont platformFont, PdfExportModelFont modelFont) : base(platformFont, modelFont)
        {
            this.mappedGlyphsCache = new Dictionary<int, int>();
            this.fontFile = platformFont.FontFile;
            this.unitsPerEm = (this.fontFile.Head == null) ? ((float) 0x800) : ((float) this.fontFile.Head.UnitsPerEm);
            List<PdfFontCmapFormatEntry> cMapTables = this.fontFile.CMap?.CMapTables;
            if (cMapTables != null)
            {
                this.cmapTables = new List<PdfFontCmapFormatEntry>(cMapTables);
                bool isSymbolic = (this.fontFile.OS2 != null) && this.fontFile.OS2.IsSymbolic;
                this.cmapTables.Sort((entry1, entry2) => GetCMapEntryPriority(entry1, isSymbolic) - GetCMapEntryPriority(entry2, isSymbolic));
            }
            this.ctlShaper = new Lazy<DXCTLShaper>(new Func<DXCTLShaper>(platformFont.CreateCTLShaper));
        }

        private static void AddComplexScriptRange(int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                complexChars[i] = true;
            }
        }

        public override void Dispose()
        {
            if (this.ctlShaper.IsValueCreated)
            {
                DXCTLShaper local1 = this.ctlShaper.Value;
                if (local1 == null)
                {
                    DXCTLShaper local2 = local1;
                }
                else
                {
                    local1.Dispose();
                }
            }
        }

        protected override double GetCharacterWidth(char ch) => 
            (double) this.fontFile.GetCharacterWidth(this.GetGlyphIndex(ch));

        private static int GetCMapEntryPriority(PdfFontCmapFormatEntry entry, bool isSymbolic)
        {
            PdfFontPlatformID platformId = entry.PlatformId;
            int num = (platformId == PdfFontPlatformID.ISO) ? 100 : ((platformId != PdfFontPlatformID.Microsoft) ? 200 : 0);
            PdfFontEncodingID encodingId = entry.EncodingId;
            num = (encodingId == PdfFontEncodingID.Symbol) ? (num + (isSymbolic ? 0 : 10)) : ((encodingId != PdfFontEncodingID.UGL) ? (num + 20) : (num + (isSymbolic ? 10 : 0)));
            if (!(entry is PdfFontCmapSegmentMappingFormatEntry))
            {
                num++;
            }
            return num;
        }

        private int GetGlyphIndex(char character)
        {
            int num;
            int glyphIndex;
            if (this.mappedGlyphsCache.TryGetValue(character, out num))
            {
                return num;
            }
            using (List<PdfFontCmapFormatEntry>.Enumerator enumerator = this.cmapTables.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        num = enumerator.Current.MapCode(character);
                        if (num == 0)
                        {
                            if (character == '\x00a0')
                            {
                                glyphIndex = this.GetGlyphIndex(' ');
                                break;
                            }
                            continue;
                        }
                    }
                    this.mappedGlyphsCache.Add(character, num);
                    return num;
                }
            }
            return glyphIndex;
        }

        public override IList<DXCluster> GetTextRuns(string text, bool directionRightToLeft, float fontSizeInPoints, bool useKerning)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new DXCluster[0];
            }
            DXCTLShaper shaper = this.ctlShaper.Value;
            bool hasComplexChars = HasComplexChars(text);
            if (hasComplexChars && (base.ModelFont.UseTwoByteCodePoints && (shaper != null)))
            {
                return shaper.GetTextRuns(text, directionRightToLeft, fontSizeInPoints, useKerning);
            }
            IEnumerable<IDXTextRun> enumerable = this.Itemize(text, directionRightToLeft, hasComplexChars);
            int length = text.Length;
            List<int> list = new List<int>(length);
            if (this.cmapTables != null)
            {
                for (int i = 0; i < length; i++)
                {
                    char c = text[i];
                    list.Add(IsWritingOrderControl(c) ? 0 : this.GetGlyphIndex(c));
                }
            }
            IDictionary<int, string> dictionary = new Dictionary<int, string>(length);
            IList<DXCluster> list2 = new List<DXCluster>(length);
            PdfFontKernTableDirectoryEntry kern = this.fontFile.Kern;
            float num2 = fontSizeInPoints / this.unitsPerEm;
            bool flag2 = useKerning && (kern != null);
            foreach (IDXTextRun run in enumerable)
            {
                for (int i = 0; i < run.Length; i++)
                {
                    int offset = run.Offset + i;
                    char c = text[offset];
                    if (!IsWritingOrderControl(c))
                    {
                        DXGlyph glyph;
                        if (IsZeroWidthSymbol(c))
                        {
                            glyph = new DXGlyph(0, 0f, DXGlyphOffset.Empty);
                        }
                        else
                        {
                            int num6 = list[offset];
                            float num7 = 0f;
                            if (flag2 && (offset < (length - 1)))
                            {
                                num7 = kern.GetKerning(num6, list[offset + 1]) * num2;
                            }
                            float characterWidth = this.fontFile.GetCharacterWidth(num6);
                            glyph = base.ModelFont.CreateGlyph(num6, c, characterWidth, ((characterWidth / 1000f) * fontSizeInPoints) + num7);
                        }
                        DXCluster item = new DXCluster(glyph, new StringView(text, offset, 1), run.GetBreakpoint(i), run.BidiLevel, c == '\t');
                        if (!dictionary.ContainsKey(glyph.Index))
                        {
                            dictionary.Add(glyph.Index, c.ToString());
                        }
                        list2.Add(item);
                    }
                }
            }
            return list2;
        }

        private static bool HasComplexChars(string text)
        {
            foreach (char ch in text)
            {
                if (complexChars[ch])
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsWritingOrderControl(char c) => 
            (c >= '‪') && (c <= '‮');

        private static bool IsZeroWidthSymbol(char ch)
        {
            switch (ch)
            {
                case '​':
                case '‌':
                case '‍':
                    return true;
            }
            return false;
        }

        private IEnumerable<IDXTextRun> Itemize(string text, bool directionRightToLeft, bool hasComplexChars)
        {
            if ((hasComplexChars | directionRightToLeft) && (this.ctlShaper.Value != null))
            {
                return this.ctlShaper.Value.Itemize(text, directionRightToLeft);
            }
            return new IDXTextRun[] { new SimpleTextRun(text) };
        }

        protected override string PostScriptFontName
        {
            get
            {
                PdfFontNameTableDirectoryEntry name = this.fontFile.Name;
                return (((name == null) || (string.IsNullOrEmpty(name.MacFamilyName) || ((name.MacFamilyName == name.FamilyName) || string.IsNullOrEmpty(name.PostScriptName)))) ? base.PostScriptFontName : name.PostScriptName);
            }
        }

        public class SimpleTextRun : IDXTextRun
        {
            private readonly DXLineBreakpoint[] breakpoints;

            public SimpleTextRun(string text)
            {
                this.<Length>k__BackingField = text.Length;
                IList<DXBreakCondition> breakPoints = PdfLineBreakAnalyzer.GetBreakPoints(text);
                this.breakpoints = new DXLineBreakpoint[this.Length];
                for (int i = 0; i < this.Length; i++)
                {
                    this.breakpoints[i] = new DXLineBreakpoint(breakPoints[i], text[i]);
                }
            }

            public DXLineBreakpoint GetBreakpoint(int index) => 
                this.breakpoints[index];

            public int Offset =>
                0;

            public byte BidiLevel =>
                0;

            public int Length { get; }
        }
    }
}

