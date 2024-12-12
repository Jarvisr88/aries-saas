namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetTextFontCommand : PdfCommand
    {
        internal const string Name = "Tf";
        private readonly PdfFont font;
        private readonly double fontSize;
        private readonly string fontName;

        internal PdfSetTextFontCommand(PdfResources resources, PdfStack operands)
        {
            this.fontSize = operands.PopDouble();
            string fontName = operands.PopName(false);
            if (fontName != null)
            {
                this.font = resources.GetFont(fontName);
                if (this.font == null)
                {
                    this.fontName = fontName;
                }
            }
        }

        internal PdfSetTextFontCommand(PdfFont font, double fontSize)
        {
            this.font = font;
            this.fontSize = fontSize;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            PdfFont font;
            try
            {
                font = this.font ?? interpreter.PageResources.GetFont(this.fontName);
            }
            catch
            {
                font = null;
            }
            interpreter.SetFont(font, this.fontSize);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            PdfName name1 = resources.FindFontName(this.font);
            PdfName name = name1;
            if (name1 == null)
            {
                PdfName local1 = name1;
                name = new PdfName(this.fontName);
            }
            writer.WriteName(name);
            writer.WriteSpace();
            writer.WriteDouble(this.fontSize);
            writer.WriteSpace();
            writer.WriteString("Tf");
        }

        public PdfFont Font =>
            this.font;

        public double FontSize =>
            this.fontSize;

        internal string FontName =>
            this.fontName;
    }
}

