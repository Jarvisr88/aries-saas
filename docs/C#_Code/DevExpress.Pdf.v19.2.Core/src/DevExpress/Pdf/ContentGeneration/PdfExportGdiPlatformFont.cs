namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Data.Helpers;
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration.Interop;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class PdfExportGdiPlatformFont : IPdfExportPlatformFont
    {
        private readonly Font font;

        private PdfExportGdiPlatformFont(Font font, PdfFontFile fontFile)
        {
            this.font = font;
            this.<FontFile>k__BackingField = fontFile;
            this.<Descriptor>k__BackingField = CreateDescriptor(font);
            this.<Metrics>k__BackingField = new PdfFontMetrics(this.FontFile);
            this.<Simulations>k__BackingField = DXFontSimulations.None;
            if (font.Bold && IsNotBold(this.FontFile))
            {
                this.<Simulations>k__BackingField = this.Simulations | DXFontSimulations.Bold;
            }
            if (font.Italic && IsNotItalic(this.FontFile))
            {
                this.<Simulations>k__BackingField = this.Simulations | DXFontSimulations.Italic;
            }
        }

        public static IPdfExportPlatformFont Create(Font font)
        {
            bool enable = AzureCompatibility.Enable;
            PdfFontFile fontFile = (!SecurityHelper.IsUnmanagedCodeGrantedAndCanUseGetHdc || enable) ? null : CreateFontFileGdi(font);
            return ((fontFile != null) ? ((IPdfExportPlatformFont) new PdfExportGdiPlatformFont(font, fontFile)) : ((IPdfExportPlatformFont) new PdfExportGdiPartialTrustPlatformFont(font)));
        }

        public DXCTLShaper CreateCTLShaper() => 
            new UniscribeShaper(this.font);

        private static DXFontDescriptor CreateDescriptor(Font font) => 
            PdfFontNamesHelper.GetGDICompatibleDescriptor(font.FontFamily.GetName(0x409), (PdfFontStyle) font.Style);

        [SecuritySafeCritical]
        private static PdfFontFile CreateFontFileGdi(Font font)
        {
            PdfFontFile file;
            bool flag = font.FontFamily.Name == "Symbol";
            if (flag)
            {
                System.Version version = Environment.OSVersion.Version;
                if ((flag & ((version.Major < 6) || ((version.Major == 6) && (version.Minor == 0)))) & !(FontSizeHelper.GetSizeInPoints(font) == 9f))
                {
                    font = new Font("Symbol", 9f, font.Style);
                }
            }
            using (Bitmap bitmap = new Bitmap(1, 1))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    IntPtr hdc = graphics.GetHdc();
                    try
                    {
                        IntPtr hgdiobj = Gdi32Interop.SelectObject(hdc, font.ToHfont());
                        try
                        {
                            uint cbData = Gdi32Interop.GetFontData(hdc, 0x66637474, 0, null, 0);
                            if (cbData != uint.MaxValue)
                            {
                                byte[] lpvBuffer = new byte[cbData];
                                file = (Gdi32Interop.GetFontData(hdc, 0x66637474, 0, lpvBuffer, cbData) == lpvBuffer.Length) ? PdfTrueTypeCollectionFontFile.ReadFontFile(lpvBuffer, font.FontFamily.GetName(0x409)) : null;
                            }
                            else
                            {
                                cbData = Gdi32Interop.GetFontData(hdc, 0, 0, null, 0);
                                if (cbData != uint.MaxValue)
                                {
                                    byte[] lpvBuffer = new byte[cbData];
                                    file = (Gdi32Interop.GetFontData(hdc, 0, 0, lpvBuffer, cbData) == lpvBuffer.Length) ? new PdfFontFile(PdfFontFile.TTFVersion, lpvBuffer) : null;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                        finally
                        {
                            IntPtr ptr2;
                            Gdi32Interop.SelectObject(hdc, hgdiobj);
                            Gdi32Interop.DeleteObject(ptr2);
                        }
                    }
                    catch
                    {
                        file = null;
                    }
                    finally
                    {
                        graphics.ReleaseHdc(hdc);
                        if (flag)
                        {
                            font.Dispose();
                        }
                    }
                }
            }
            return file;
        }

        public Font CreateGDIPlusFont(float fontSize) => 
            new Font(this.font.FontFamily, fontSize, this.font.Style);

        private static bool IsNotBold(PdfFontFile fontFile)
        {
            PdfFontOS2TableDirectoryEntry entry = fontFile?.OS2;
            return ((entry == null) || (!entry.IsBold && (entry.WeightClass < 700)));
        }

        private static bool IsNotItalic(PdfFontFile fontFile)
        {
            PdfFontOS2TableDirectoryEntry entry = fontFile?.OS2;
            return (((entry == null) || !entry.IsItalic) ? ((fontFile.Post == null) || (Math.Abs(fontFile.Post.ItalicAngle) <= float.Epsilon)) : false);
        }

        public bool ShouldEmbed(PdfCreationOptions creationOptions) => 
            creationOptions.EmbedFont(this.font.Name, this.font.Bold, this.font.Italic);

        public PdfFontMetrics Metrics { get; }

        public PdfFontFile FontFile { get; }

        public DXFontDescriptor Descriptor { get; }

        public DXFontSimulations Simulations { get; }

        private class PdfExportGdiPartialTrustPlatformFont : IPdfExportPlatformFont
        {
            private readonly Font font;

            public PdfExportGdiPartialTrustPlatformFont(Font font)
            {
                this.font = font;
                this.<Descriptor>k__BackingField = PdfExportGdiPlatformFont.CreateDescriptor(font);
                this.<Metrics>k__BackingField = CreateMetrics(font);
            }

            public DXCTLShaper CreateCTLShaper() => 
                null;

            public Font CreateGDIPlusFont(float fontSize) => 
                new Font(this.font.FontFamily, fontSize, this.font.Style);

            private static PdfFontMetrics CreateMetrics(Font font)
            {
                FontFamily fontFamily = font.FontFamily;
                FontStyle style = font.Style;
                return new PdfFontMetrics((double) fontFamily.GetCellAscent(style), (double) fontFamily.GetCellDescent(style), (double) fontFamily.GetLineSpacing(style), (double) fontFamily.GetEmHeight(style));
            }

            public bool ShouldEmbed(PdfCreationOptions creationOptions) => 
                creationOptions.EmbedFont(this.font.Name, this.font.Bold, this.font.Italic);

            public DXFontDescriptor Descriptor { get; }

            public PdfFontMetrics Metrics { get; }

            public DXFontSimulations Simulations =>
                DXFontSimulations.None;

            public PdfFontFile FontFile =>
                null;
        }
    }
}

