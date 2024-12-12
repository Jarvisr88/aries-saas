namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Security;

    public class PdfFont : IDisposable
    {
        public const float MaxFontSize = 300f;
        private System.Drawing.Font font;
        protected PdfFontBase innerFont;
        private DevExpress.XtraPrinting.Export.Pdf.TTFFile ttfFile;
        private string name;
        private float scaleFactor;
        private PdfCharCache charCache;
        private bool needToEmbedFont;
        protected bool compressed;
        private bool? emulateBold;

        public PdfFont(System.Drawing.Font font, bool compressed)
        {
            this.needToEmbedFont = true;
            this.font = (System.Drawing.Font) font.Clone();
            this.compressed = compressed;
            if (this.ShouldCreateTTFFile)
            {
                this.InitTTFFile(this.CreateTTFFile());
            }
        }

        internal PdfFont(System.Drawing.Font font, DevExpress.XtraPrinting.Export.Pdf.TTFFile ttfFile, bool compressed)
        {
            this.needToEmbedFont = true;
            this.font = (System.Drawing.Font) font.Clone();
            this.compressed = compressed;
            this.InitTTFFile(ttfFile);
        }

        private System.Drawing.Font CreateFakeFont()
        {
            string familyName = "Symbol";
            return ((this.font.Name == familyName) ? new System.Drawing.Font(familyName, 9f, this.Font.Style) : ((System.Drawing.Font) this.font.Clone()));
        }

        protected internal virtual void CreateInnerFont()
        {
            if (this.innerFont == null)
            {
                if (this.NeedToEmbedFont)
                {
                    this.innerFont = new PdfType0Font(this, this.compressed);
                }
                else
                {
                    this.innerFont = new PdfTrueTypeFont(this, this.compressed);
                }
            }
        }

        protected virtual PdfCharCache CreatePdfFontCache() => 
            new PdfCharCache();

        private DevExpress.XtraPrinting.Export.Pdf.TTFFile CreateTTFFile()
        {
            using (System.Drawing.Font font = this.CreateFakeFont())
            {
                bool flag;
                byte[] data = PdfFontUtils.CreateTTFFile(font, out flag);
                return this.CreateTTFFile(data, flag, GetFontCodePage(font));
            }
        }

        private DevExpress.XtraPrinting.Export.Pdf.TTFFile CreateTTFFile(byte[] data, bool isTtcFile, int fontCodePage)
        {
            if (isTtcFile)
            {
                string name = this.font.FontFamily.GetName(0x409);
                return new TTCFile().Read(data, name, fontCodePage);
            }
            DevExpress.XtraPrinting.Export.Pdf.TTFFile file2 = new DevExpress.XtraPrinting.Export.Pdf.TTFFile(0, fontCodePage);
            file2.Read(data);
            return file2;
        }

        public void Dispose()
        {
            if (this.font != null)
            {
                this.font.Dispose();
                this.font = null;
            }
            if (this.innerFont != null)
            {
                this.innerFont.Dispose();
                this.innerFont = null;
            }
        }

        public bool EmulateBold(PdfFonts fonts)
        {
            if (this.emulateBold == null)
            {
                this.emulateBold = new bool?(this.ShouldEmulateBold(fonts));
            }
            return this.emulateBold.Value;
        }

        public void FillUp()
        {
            if (this.innerFont != null)
            {
                this.CharCache.CalculateGlyphIndeces(this.ttfFile);
                this.innerFont.FillUp();
            }
        }

        public float GetCharWidth(char ch) => 
            this.ttfFile.GetCharWidth(ch) * this.scaleFactor;

        internal float GetCharWidth(ushort glyphIndex) => 
            this.ttfFile.GetCharWidth(glyphIndex) * this.scaleFactor;

        [SecuritySafeCritical]
        private static int GetFontCodePage(System.Drawing.Font font)
        {
            int ciACP;
            IntPtr hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc();
            IntPtr hgdiobj = GDIFunctions.SelectObject(hdc, font.ToHfont());
            try
            {
                int textCharset = GDIFunctions.GetTextCharset(hdc);
                CHARSETINFO lpCs = new CHARSETINFO();
                GDIFunctions.TranslateCharsetInfo((uint) textCharset, ref lpCs, 1);
                ciACP = lpCs.ciACP;
            }
            finally
            {
                Graphics graphics;
                IntPtr ptr2;
                GDIFunctions.SelectObject(hdc, hgdiobj);
                GDIFunctions.DeleteObject(ptr2);
                graphics.ReleaseHdc(hdc);
                graphics.Dispose();
            }
            return ciACP;
        }

        internal ushort GetGlyphByChar(char ch) => 
            this.ttfFile.GetGlyphIndex(ch);

        private void InitTTFFile(DevExpress.XtraPrinting.Export.Pdf.TTFFile ttfFile)
        {
            if (ttfFile != null)
            {
                this.ttfFile = ttfFile;
                this.scaleFactor = 1000f / ((float) ttfFile.Head.UnitsPerEm);
            }
        }

        public string ProcessText(TextRun textRun)
        {
            if (this.innerFont == null)
            {
                throw new PdfException("The inner font doesn't specified yet");
            }
            return this.innerFont.ProcessText(textRun);
        }

        public void Register(PdfXRef xRef)
        {
            if (this.innerFont != null)
            {
                this.innerFont.Register(xRef);
            }
        }

        protected internal void SetName(string name)
        {
            this.name ??= name;
        }

        private bool ShouldEmulateBold(PdfFonts fonts)
        {
            if (!this.font.Bold || !this.font.FontFamily.IsStyleAvailable(FontStyle.Regular))
            {
                return false;
            }
            using (System.Drawing.Font font = new System.Drawing.Font(this.font, FontStyle.Regular))
            {
                PdfFont font2 = fonts.FindFont(font);
                bool flag = false;
                if (font2 == null)
                {
                    font2 = PdfFonts.CreatePdfFont(font, this.compressed);
                    flag = true;
                }
                bool flag2 = DevExpress.XtraPrinting.Export.Pdf.TTFFile.IsIdentical(this.TTFFile, font2.TTFFile);
                if (flag)
                {
                    font2.Dispose();
                }
                return flag2;
            }
        }

        public void Write(StreamWriter writer)
        {
            if (this.innerFont != null)
            {
                this.innerFont.Write(writer);
            }
        }

        protected internal void WriteFontSubset(Stream stream)
        {
            this.ttfFile.Write(stream, this.CharCache, PdfFontUtils.GetTrueTypeFontName0(this.font.Name, true));
        }

        internal DevExpress.XtraPrinting.Export.Pdf.TTFFile TTFFile =>
            this.ttfFile;

        public PdfDictionary Dictionary =>
            this.innerFont?.Dictionary;

        public float ScaleFactor =>
            this.scaleFactor;

        public PdfCharCache CharCache
        {
            get
            {
                this.charCache ??= this.CreatePdfFontCache();
                return this.charCache;
            }
        }

        public System.Drawing.Font Font =>
            this.font;

        public string Name =>
            this.name;

        public bool NeedToEmbedFont
        {
            get => 
                this.needToEmbedFont;
            set => 
                this.needToEmbedFont = value;
        }

        protected virtual bool ShouldCreateTTFFile =>
            true;
    }
}

