namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PdfExportFontManager : IDisposable, IPdfExportFontProvider
    {
        private const string timesNewRomanFontName = "Times New Roman";
        private const string courierNewFontName = "Courier New";
        private const float symbolFontSize = 9f;
        private readonly IPdfDocumentContext documentContext;
        private readonly IDictionary<object, PdfExportFont> fontStorage = new Dictionary<object, PdfExportFont>();
        private readonly Lazy<GDIPlusMeasuringContext> gdiMeasuringContext = new Lazy<GDIPlusMeasuringContext>();
        private readonly IFontProviderBuilder builder;

        public PdfExportFontManager(IPdfDocumentContext documentContext)
        {
            this.documentContext = documentContext;
            try
            {
                this.builder = ((Environment.OSVersion.Platform == PlatformID.Win32NT) || AzureCompatibility.Enable) ? ((IFontProviderBuilder) new GDIExportFontProviderBuilder()) : ((IFontProviderBuilder) new CrossPlatformFontProviderBuilder());
            }
            catch
            {
                this.builder = new GDIExportFontProviderBuilder();
            }
        }

        private PdfExportFont CreateExportFont(IPdfExportPlatformFont platformFont)
        {
            if (AzureCompatibility.Enable || (platformFont.FontFile == null))
            {
                return new PdfExportPartialTrustFont(platformFont, this.gdiMeasuringContext.Value, PdfExportNotEmbeddedModelFont.Create(platformFont));
            }
            return new PdfExportFullTrustFont(platformFont, !this.EmbedFont(platformFont) ? ((PdfExportModelFont) PdfExportNotEmbeddedModelFont.Create(platformFont)) : ((PdfExportModelFont) new PdfExportEmbeddedModelFont(this.documentContext, platformFont)));
        }

        public void Dispose()
        {
            foreach (PdfExportFont font in this.fontStorage.Values)
            {
                font.Dispose();
            }
            if (this.gdiMeasuringContext.IsValueCreated)
            {
                this.gdiMeasuringContext.Value.Dispose();
            }
        }

        private bool EmbedFont(IPdfExportPlatformFont platformFont)
        {
            PdfCreationOptions creationOptions = this.documentContext.DocumentCatalog.CreationOptions;
            return ((creationOptions == null) || platformFont.ShouldEmbed(creationOptions));
        }

        public PdfExportFont GetExportFont(IPdfExportPlatformFontProvider provider)
        {
            PdfExportFont font;
            if (!this.fontStorage.TryGetValue(provider.Key, out font))
            {
                font = this.CreateExportFont(provider.GetPlatformFont());
                this.fontStorage.Add(provider.Key, font);
            }
            return font;
        }

        private PdfExportFont GetFontData(string fontFamily, PdfFontStyle fontStyle)
        {
            using (Font font = new Font(fontFamily, 9f, (FontStyle) fontStyle))
            {
                return this.GetExportFont(this.builder.CreateProvider(font));
            }
        }

        public PdfExportFontInfo GetFontInfo(Font nativeFont)
        {
            DXFontDecorations none = DXFontDecorations.None;
            if (nativeFont.Strikeout)
            {
                none |= DXFontDecorations.Strikeout;
            }
            if (nativeFont.Underline)
            {
                none |= DXFontDecorations.Underline;
            }
            return new PdfExportFontInfo(this.GetExportFont(this.builder.CreateProvider(nativeFont)), nativeFont.SizeInPoints, none);
        }

        public PdfExportFont GetMatchingFont(PdfSetTextFontCommand setTextFontCommand)
        {
            string fontName;
            if (setTextFontCommand == null)
            {
                return this.GetFontData("Times New Roman", PdfFontStyle.Regular);
            }
            PdfFontStyle regular = PdfFontStyle.Regular;
            PdfFontFlags nonsymbolic = PdfFontFlags.Nonsymbolic;
            PdfFont font = setTextFontCommand.Font;
            if (font == null)
            {
                fontName = setTextFontCommand.FontName;
            }
            else
            {
                PdfFontParameters substituteFontParameters = PdfGDIFontSubstitutionHelper.GetSubstituteFontParameters(font);
                if (substituteFontParameters.Weight > 400)
                {
                    regular |= PdfFontStyle.Bold;
                }
                if (substituteFontParameters.IsItalic)
                {
                    regular |= PdfFontStyle.Italic;
                }
                fontName = substituteFontParameters.Name;
                if (font.FontDescriptor != null)
                {
                    nonsymbolic = font.FontDescriptor.Flags;
                }
            }
            return this.GetMatchingFont(fontName, regular, nonsymbolic);
        }

        public PdfExportFont GetMatchingFont(string fontFamily, PdfFontStyle style) => 
            this.GetMatchingFont(fontFamily, style, PdfFontFlags.Nonsymbolic);

        private PdfExportFont GetMatchingFont(string fontName, PdfFontStyle style, PdfFontFlags flags)
        {
            if (string.IsNullOrEmpty(fontName))
            {
                fontName = "Times New Roman";
            }
            string systemFontFamily = PdfGDIFontSubstitutionHelper.GetSystemFontFamily(fontName);
            return ((systemFontFamily == null) ? ((fontName.Contains("Times New Roman") || fontName.Contains("TimesNewRoman")) ? this.GetFontData("Times New Roman", style) : (!fontName.Contains("Courier") ? (!fontName.Contains("Arial") ? (!flags.HasFlag(PdfFontFlags.Serif) ? this.GetFontData(flags.HasFlag(PdfFontFlags.FixedPitch) ? "Courier New" : "Arial", style) : this.GetFontData("Times New Roman", style)) : this.GetFontData("Arial", style)) : this.GetFontData("Courier New", style))) : this.GetFontData(systemFontFamily, style));
        }

        public void UpdateFonts()
        {
            foreach (PdfExportFont font in this.fontStorage.Values)
            {
                font.UpdateFont();
            }
        }

        internal Guid ObjectsId =>
            this.documentContext.DocumentCatalog.Objects.Id;
    }
}

