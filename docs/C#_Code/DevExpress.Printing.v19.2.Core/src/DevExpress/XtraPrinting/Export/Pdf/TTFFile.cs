namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.IO;

    internal class TTFFile
    {
        private TTFTableDirectory tableDirectory;
        private TTFCMap cmap;
        private TTFGlyf glyf;
        private TTFHead head;
        private TTFHHea hhea;
        private TTFHMtx hmtx;
        private TTFLoca loca;
        private TTFMaxP maxp;
        private TTFPost post;
        private TTFOS2 os2;
        private TTFName name;
        private TTFBinaryTable prep;
        private TTFBinaryTable cvt;
        private TTFBinaryTable fpgm;
        private bool hasPrep;
        private bool hasCVT;
        private bool hasFPGM;
        private uint offset;
        private int fontCodePage;

        public TTFFile() : this(0, 0)
        {
        }

        public TTFFile(uint offset) : this(offset, 0)
        {
        }

        public TTFFile(uint offset, int fontCodePage)
        {
            this.hasPrep = true;
            this.hasCVT = true;
            this.hasFPGM = true;
            this.offset = offset;
            this.fontCodePage = fontCodePage;
            this.tableDirectory = new TTFTableDirectory(this);
            this.head = new TTFHead(this);
            this.maxp = new TTFMaxP(this);
            this.hhea = new TTFHHea(this);
            this.hmtx = new TTFHMtx(this);
            this.post = new TTFPost(this);
            this.os2 = new TTFOS2(this);
            this.loca = new TTFLoca(this);
            this.glyf = new TTFGlyf(this);
            this.cmap = new TTFCMap(this);
            this.name = new TTFName(this);
            this.prep = new TTFBinaryTable(this, "prep");
            this.cvt = new TTFBinaryTable(this, "cvt ");
            this.fpgm = new TTFBinaryTable(this, "fpgm");
        }

        private void AddGlyphIndex(ushort glyphIndex, TTFGlyphIndexCache cache)
        {
            if (cache.Add(glyphIndex))
            {
                TTFGlyphData data = this.Glyf.Glyphs[glyphIndex];
                if (data != null)
                {
                    TTFCompositeGlyphDescription description = data.Description as TTFCompositeGlyphDescription;
                    if (description != null)
                    {
                        for (int i = 0; i < description.Count; i++)
                        {
                            this.AddGlyphIndex(description[i], cache);
                        }
                    }
                }
            }
        }

        public ushort[] CreateGlyphIndices(PdfCharCache charCache)
        {
            TTFGlyphIndexCache cache = new TTFGlyphIndexCache();
            this.AddGlyphIndex(0, cache);
            foreach (char ch in (IEnumerable) charCache)
            {
                ushort glyphIndex = this.CMap[Convert.ToUInt16(ch)];
                this.AddGlyphIndex(glyphIndex, cache);
                char[] c = new char[] { ch };
                charCache.AddUniqueGlyph(glyphIndex, c);
            }
            foreach (ushort num3 in charCache.GlyphIndices)
            {
                this.AddGlyphIndex(num3, cache);
            }
            return cache.ToArray;
        }

        public ushort GetCharWidth(char ch) => 
            this.GetCharWidth(this.GetGlyphIndex(ch));

        public ushort GetCharWidth(ushort glyphIndex) => 
            this.HMtx[glyphIndex].AdvanceWidth;

        public ushort GetGlyphIndex(char ch) => 
            this.CMap[ch];

        private void Initialize(TTFFile pattern, TTFInitializeParam param)
        {
            this.Glyf.Initialize(pattern.Glyf, param);
            this.Head.Initialize(pattern.Head);
            this.Loca.Initialize(pattern.Loca);
            this.MaxP.Initialize(pattern.MaxP);
            this.HHea.Initialize(pattern.HHea);
            this.HMtx.Initialize(pattern.HMtx);
            this.hasPrep = pattern.hasPrep;
            this.hasCVT = pattern.hasCVT;
            this.hasFPGM = pattern.hasFPGM;
            if (this.hasPrep)
            {
                this.Prep.Initialize(pattern.Prep);
            }
            if (this.hasCVT)
            {
                this.CVT.Initialize(pattern.CVT);
            }
            if (this.hasFPGM)
            {
                this.FPGM.Initialize(pattern.FPGM);
            }
            this.tableDirectory.Initialize(pattern.TableDirectory);
        }

        public static bool IsIdentical(TTFFile ttfFile1, TTFFile ttfFile2)
        {
            if ((ttfFile1 == null) || ((ttfFile2 == null) || (ttfFile1.tableDirectory.Count != ttfFile2.tableDirectory.Count)))
            {
                return false;
            }
            for (int i = 0; i < ttfFile1.tableDirectory.Count; i++)
            {
                if (ttfFile1.tableDirectory[i].CheckSum != ttfFile2.tableDirectory[i].CheckSum)
                {
                    return false;
                }
            }
            return true;
        }

        public void Read(byte[] data)
        {
            TTFStream ttfStream = new TTFStreamAsByteArray(data);
            this.tableDirectory.Read(ttfStream);
            this.head.Read(ttfStream);
            this.hhea.Read(ttfStream);
            this.maxp.Read(ttfStream);
            this.hmtx.Read(ttfStream);
            this.loca.Read(ttfStream);
            this.glyf.Read(ttfStream);
            this.hasPrep = this.TryToRead(this.prep, ttfStream);
            this.hasCVT = this.TryToRead(this.cvt, ttfStream);
            this.hasFPGM = this.TryToRead(this.fpgm, ttfStream);
            this.os2.Read(ttfStream);
            this.post.Read(ttfStream);
            this.cmap.Read(ttfStream);
            this.name.Read(ttfStream);
        }

        internal void ReadNameTable(TTFStream ttfStream)
        {
            this.tableDirectory.Read(ttfStream);
            this.name.Read(ttfStream);
        }

        private bool TryToRead(TTFTable optionalTable, TTFStream ttfStream)
        {
            try
            {
                optionalTable.Read(ttfStream);
                return true;
            }
            catch (TTFFileException)
            {
                return false;
            }
        }

        private void Write(TTFStream ttfStream)
        {
            this.tableDirectory.Write(ttfStream);
            this.Head.Write(ttfStream);
            this.HHea.Write(ttfStream);
            this.MaxP.Write(ttfStream);
            this.HMtx.Write(ttfStream);
            this.Loca.Write(ttfStream);
            this.Glyf.Write(ttfStream);
            if (this.hasPrep)
            {
                this.Prep.Write(ttfStream);
            }
            if (this.hasCVT)
            {
                this.CVT.Write(ttfStream);
            }
            if (this.hasFPGM)
            {
                this.FPGM.Write(ttfStream);
            }
            ttfStream.Pad4();
            this.tableDirectory.WriteOffsets(ttfStream);
            this.tableDirectory.WriteCheckSum(ttfStream);
            this.Head.WriteCheckSumAdjustment(ttfStream);
        }

        public void Write(Stream stream, PdfCharCache charCache, string newFontName)
        {
            TTFFile file = new TTFFile();
            TTFInitializeParam param = new TTFInitializeParam {
                Chars = charCache,
                NewFontName = newFontName
            };
            file.Initialize(this, param);
            file.Write(new TTFStreamAsStream(stream));
        }

        public TTFTableDirectory TableDirectory =>
            this.tableDirectory;

        public TTFHead Head =>
            this.head;

        public TTFMaxP MaxP =>
            this.maxp;

        public TTFHHea HHea =>
            this.hhea;

        public TTFHMtx HMtx =>
            this.hmtx;

        public TTFPost Post =>
            this.post;

        public TTFOS2 OS2 =>
            this.os2;

        public TTFLoca Loca =>
            this.loca;

        public TTFGlyf Glyf =>
            this.glyf;

        public TTFCMap CMap =>
            this.cmap;

        public TTFName Name =>
            this.name;

        public TTFBinaryTable Prep =>
            this.prep;

        public TTFBinaryTable CVT =>
            this.cvt;

        public TTFBinaryTable FPGM =>
            this.fpgm;

        public bool IsEmbeddableFont =>
            this.OS2.FsType != 2;

        public uint Offset =>
            this.offset;

        public int FontCodePage
        {
            get => 
                this.fontCodePage;
            set => 
                this.fontCodePage = value;
        }
    }
}

