namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using DevExpress.XtraPrinting.Export.Pdf;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [CLSCompliant(false)]
    public class EmfPlusFont
    {
        private IPdfContentsOwner owner;
        private DevExpress.XtraPrinting.Export.Pdf.PdfFont pdfFont;

        public EmfPlusFont(MetaReader reader, IPdfContentsOwner owner)
        {
            EmfPlusGraphicsVersion version1 = new EmfPlusGraphicsVersion(reader);
            this.EmSize = reader.ReadSingle();
            this.SizeUnit = (UnitType) reader.ReadUInt32();
            this.FontStyleFlags = (FontStyle) reader.ReadInt32();
            reader.ReadUInt32();
            this.Length = reader.ReadUInt32();
            this.FamilyName = MetaImage.GetUnicodeStringData(reader, (int) this.Length);
            this.owner = owner;
        }

        public float EmSize { get; set; }

        public UnitType SizeUnit { get; set; }

        public FontStyle FontStyleFlags { get; set; }

        public uint Length { get; set; }

        public string FamilyName { get; set; }

        public DevExpress.XtraPrinting.Export.Pdf.PdfFont PdfFont
        {
            get
            {
                if (this.pdfFont == null)
                {
                    this.pdfFont = PdfFonts.CreatePdfFont(new Font(this.FamilyName, this.EmSize, this.FontStyleFlags), false);
                    this.owner.Fonts.AddUnique(this.pdfFont);
                }
                return this.pdfFont;
            }
        }
    }
}

