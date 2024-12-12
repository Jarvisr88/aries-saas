namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Media;

    internal class WpfExportPlatformFont : IPdfExportPlatformFont
    {
        private const string ttcfFont = "ttcf";

        public WpfExportPlatformFont(GlyphTypeface typeface, DXFontDescriptor descriptor)
        {
            this.<Descriptor>k__BackingField = descriptor;
            if (typeface.StyleSimulations.HasFlag(StyleSimulations.BoldSimulation))
            {
                this.<Simulations>k__BackingField = this.Simulations | DXFontSimulations.Bold;
            }
            if (typeface.StyleSimulations.HasFlag(StyleSimulations.ItalicSimulation))
            {
                this.<Simulations>k__BackingField = this.Simulations | DXFontSimulations.Italic;
            }
            this.<FontFile>k__BackingField = this.CreateFontFile(this.ReadStreamData(typeface));
            this.<Metrics>k__BackingField = new PdfFontMetrics(this.FontFile);
        }

        public DXCTLShaper CreateCTLShaper() => 
            null;

        private PdfFontFile CreateFontFile(byte[] data)
        {
            Func<StringBuilder, byte, StringBuilder> func = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<StringBuilder, byte, StringBuilder> local1 = <>c.<>9__17_0;
                func = <>c.<>9__17_0 = (builder, x) => builder.Append((char) x);
            }
            return ((data.Take<byte>(4).Aggregate<byte, StringBuilder>(new StringBuilder(), func).ToString() != "ttcf") ? new PdfFontFile(PdfFontFile.TTFVersion, data) : PdfTrueTypeCollectionFontFile.ReadFontFile(data, this.Descriptor.FamilyName));
        }

        public Font CreateGDIPlusFont(float fontSize) => 
            null;

        private byte[] ReadStreamData(GlyphTypeface typeface)
        {
            using (Stream stream = typeface.GetFontStream())
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int) stream.Length);
                return buffer;
            }
        }

        public bool ShouldEmbed(PdfCreationOptions creationOptions) => 
            creationOptions.EmbedFont(this.Descriptor.FamilyName, this.Descriptor.Weight >= DXFontWeight.Bold, this.Descriptor.Style != DXFontStyle.Regular);

        public DXFontDescriptor Descriptor { get; }

        public DXFontSimulations Simulations { get; }

        public PdfFontMetrics Metrics { get; }

        public PdfFontFile FontFile { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WpfExportPlatformFont.<>c <>9 = new WpfExportPlatformFont.<>c();
            public static Func<StringBuilder, byte, StringBuilder> <>9__17_0;

            internal StringBuilder <CreateFontFile>b__17_0(StringBuilder builder, byte x) => 
                builder.Append((char) x);
        }
    }
}

