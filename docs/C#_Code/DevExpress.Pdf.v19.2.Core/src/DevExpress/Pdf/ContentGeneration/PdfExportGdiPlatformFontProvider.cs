namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PdfExportGdiPlatformFontProvider : IPdfExportPlatformFontProvider
    {
        private static HashSet<FontFamily> systemFontFamilies;
        private readonly Font font;

        static PdfExportGdiPlatformFontProvider()
        {
            try
            {
                systemFontFamilies = new HashSet<FontFamily>(FontFamily.Families);
            }
            catch
            {
                systemFontFamilies = new HashSet<FontFamily>();
            }
        }

        public PdfExportGdiPlatformFontProvider(Font font)
        {
            this.<Key>k__BackingField = systemFontFamilies.Contains(font.FontFamily) ? ((object) new SystemCollectionKey(font)) : ((object) new PrivateCollectionKey(font));
            this.font = font;
        }

        public IPdfExportPlatformFont GetPlatformFont() => 
            PdfExportGdiPlatformFont.Create(this.font);

        public object Key { get; }

        [StructLayout(LayoutKind.Sequential)]
        private struct PrivateCollectionKey
        {
            private readonly FontFamily family;
            private readonly bool bold;
            private readonly bool italic;
            public PrivateCollectionKey(Font font)
            {
                this.family = font.FontFamily;
                this.bold = font.Bold;
                this.italic = font.Italic;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is PdfExportGdiPlatformFontProvider.PrivateCollectionKey))
                {
                    return false;
                }
                PdfExportGdiPlatformFontProvider.PrivateCollectionKey key = (PdfExportGdiPlatformFontProvider.PrivateCollectionKey) obj;
                return (EqualityComparer<FontFamily>.Default.Equals(this.family, key.family) && ((this.bold == key.bold) && (this.italic == key.italic)));
            }

            public override int GetHashCode() => 
                (((((0x17523d8c * -1521134295) + EqualityComparer<FontFamily>.Default.GetHashCode(this.family)) * -1521134295) + this.bold.GetHashCode()) * -1521134295) + this.italic.GetHashCode();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SystemCollectionKey
        {
            private readonly string familyName;
            private readonly bool bold;
            private readonly bool italic;
            public SystemCollectionKey(Font font)
            {
                this.familyName = font.Name;
                this.bold = font.Bold;
                this.italic = font.Italic;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is PdfExportGdiPlatformFontProvider.SystemCollectionKey))
                {
                    return false;
                }
                PdfExportGdiPlatformFontProvider.SystemCollectionKey key = (PdfExportGdiPlatformFontProvider.SystemCollectionKey) obj;
                return ((this.familyName == key.familyName) && ((this.bold == key.bold) && (this.italic == key.italic)));
            }

            public override int GetHashCode() => 
                (((((0x1e96a755 * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.familyName)) * -1521134295) + this.bold.GetHashCode()) * -1521134295) + this.italic.GetHashCode();
        }
    }
}

