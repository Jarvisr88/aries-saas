namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfBeginPathCommand : PdfCommand
    {
        internal const string Name = "m";
        private readonly PdfPoint startPoint;

        internal PdfBeginPathCommand(PdfStack operands)
        {
            this.startPoint = new PdfPoint(operands);
        }

        public PdfBeginPathCommand(PdfPoint startPoint)
        {
            this.startPoint = startPoint;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.BeginPath(this.startPoint);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.startPoint.X);
            writer.WriteSpace();
            writer.WriteDouble(this.startPoint.Y);
            writer.WriteSpace();
            writer.WriteString("m");
        }

        public PdfPoint StartPoint =>
            this.startPoint;
    }
}

