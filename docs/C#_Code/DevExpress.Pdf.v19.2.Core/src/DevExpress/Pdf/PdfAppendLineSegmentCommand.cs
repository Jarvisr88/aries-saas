namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAppendLineSegmentCommand : PdfCommand
    {
        internal const string Name = "l";
        private readonly PdfPoint endPoint;

        internal PdfAppendLineSegmentCommand(PdfStack operands)
        {
            this.endPoint = new PdfPoint(operands);
        }

        public PdfAppendLineSegmentCommand(PdfPoint endPoint)
        {
            this.endPoint = endPoint;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.AppendPathLineSegment(this.endPoint);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.endPoint.X);
            writer.WriteSpace();
            writer.WriteDouble(this.endPoint.Y);
            writer.WriteSpace();
            writer.WriteString("l");
        }

        public PdfPoint EndPoint =>
            this.endPoint;
    }
}

