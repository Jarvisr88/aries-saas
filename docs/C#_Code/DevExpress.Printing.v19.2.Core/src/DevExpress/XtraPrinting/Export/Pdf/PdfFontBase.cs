namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Drawing;

    public abstract class PdfFontBase : PdfDocumentDictionaryObject, IDisposable
    {
        private PdfFont owner;

        protected PdfFontBase(PdfFont owner, bool compressed) : base(compressed)
        {
            this.owner = owner;
        }

        public abstract void Dispose();
        public override void FillUp()
        {
            base.Dictionary.Add("Type", "Font");
            base.Dictionary.Add("Subtype", this.Subtype);
            if (this.Name != null)
            {
                base.Dictionary.Add("Name", this.Name);
            }
            base.Dictionary.Add("BaseFont", this.BaseFont);
        }

        public virtual string ProcessText(TextRun info) => 
            "(" + PdfStringUtils.EscapeString(info.Text) + ")";

        internal PdfFont Owner =>
            this.owner;

        internal System.Drawing.Font Font =>
            this.Owner.Font;

        internal string Name =>
            this.Owner.Name;

        internal DevExpress.XtraPrinting.Export.Pdf.TTFFile TTFFile =>
            this.Owner.TTFFile;

        public abstract string Subtype { get; }

        public abstract string BaseFont { get; }
    }
}

