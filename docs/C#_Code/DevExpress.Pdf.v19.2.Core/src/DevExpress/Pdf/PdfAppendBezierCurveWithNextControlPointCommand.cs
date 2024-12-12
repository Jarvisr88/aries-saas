namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAppendBezierCurveWithNextControlPointCommand : PdfCommand
    {
        internal const string Name = "y";
        private readonly double x1;
        private readonly double y1;
        private readonly double x3;
        private readonly double y3;

        internal PdfAppendBezierCurveWithNextControlPointCommand(PdfStack operands)
        {
            this.y3 = operands.PopDouble();
            this.x3 = operands.PopDouble();
            this.y1 = operands.PopDouble();
            this.x1 = operands.PopDouble();
        }

        public PdfAppendBezierCurveWithNextControlPointCommand(double x1, double y1, double x3, double y3)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x3 = x3;
            this.y3 = y3;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.AppendPathBezierSegment(new PdfPoint(this.x1, this.y1), new PdfPoint(this.x3, this.y3), new PdfPoint(this.x3, this.y3));
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.x1);
            writer.WriteSpace();
            writer.WriteDouble(this.y1);
            writer.WriteSpace();
            writer.WriteDouble(this.x3);
            writer.WriteSpace();
            writer.WriteDouble(this.y3);
            writer.WriteSpace();
            writer.WriteString("y");
        }

        public double X1 =>
            this.x1;

        public double Y1 =>
            this.y1;

        public double X3 =>
            this.x3;

        public double Y3 =>
            this.y3;
    }
}

