namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAppendBezierCurveWithPreviousControlPointCommand : PdfCommand
    {
        internal const string Name = "v";
        private readonly double x2;
        private readonly double y2;
        private readonly double x3;
        private readonly double y3;

        internal PdfAppendBezierCurveWithPreviousControlPointCommand(PdfStack operands)
        {
            this.y3 = operands.PopDouble();
            this.x3 = operands.PopDouble();
            this.y2 = operands.PopDouble();
            this.x2 = operands.PopDouble();
        }

        public PdfAppendBezierCurveWithPreviousControlPointCommand(double x2, double y2, double x3, double y3)
        {
            this.x2 = x2;
            this.y2 = y2;
            this.x3 = x3;
            this.y3 = y3;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.AppendPathBezierSegment(new PdfPoint(this.x2, this.y2), new PdfPoint(this.x3, this.y3));
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.x2);
            writer.WriteSpace();
            writer.WriteDouble(this.y2);
            writer.WriteSpace();
            writer.WriteDouble(this.x3);
            writer.WriteSpace();
            writer.WriteDouble(this.y3);
            writer.WriteSpace();
            writer.WriteString("v");
        }

        public double X2 =>
            this.x2;

        public double Y2 =>
            this.y2;

        public double X3 =>
            this.x3;

        public double Y3 =>
            this.y3;
    }
}

