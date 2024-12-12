namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfSetCMYKColorSpaceCommand : PdfCommand
    {
        private readonly double c;
        private readonly double m;
        private readonly double y;
        private readonly double k;

        protected PdfSetCMYKColorSpaceCommand(PdfStack operands)
        {
            this.k = PdfColor.ClipColorComponent(operands.PopDouble());
            this.y = PdfColor.ClipColorComponent(operands.PopDouble());
            this.m = PdfColor.ClipColorComponent(operands.PopDouble());
            this.c = PdfColor.ClipColorComponent(operands.PopDouble());
        }

        protected PdfSetCMYKColorSpaceCommand(double c, double m, double y, double k)
        {
            this.c = PdfColor.ClipColorComponent(c);
            this.m = PdfColor.ClipColorComponent(m);
            this.y = PdfColor.ClipColorComponent(y);
            this.k = PdfColor.ClipColorComponent(k);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.c);
            writer.WriteSpace();
            writer.WriteDouble(this.m);
            writer.WriteSpace();
            writer.WriteDouble(this.y);
            writer.WriteSpace();
            writer.WriteDouble(this.k);
            writer.WriteSpace();
        }

        public double C =>
            this.c;

        public double M =>
            this.m;

        public double Y =>
            this.y;

        public double K =>
            this.k;
    }
}

