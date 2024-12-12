namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPaintShadingPatternCommand : PdfCommand
    {
        internal const string Name = "sh";
        private readonly string shadingName;
        private readonly PdfShading shading;

        internal PdfPaintShadingPatternCommand(PdfResources resources, PdfStack operands)
        {
            this.shadingName = operands.PopName(true);
            this.shading = resources.GetShading(this.shadingName);
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            PdfShading shading = this.shading ?? interpreter.PageResources.GetShading(this.shadingName);
            if (shading != null)
            {
                interpreter.DrawShading(shading);
            }
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteName(new PdfName(this.shadingName));
            writer.WriteSpace();
            writer.WriteString("sh");
        }

        public PdfShading Shading =>
            this.shading;
    }
}

