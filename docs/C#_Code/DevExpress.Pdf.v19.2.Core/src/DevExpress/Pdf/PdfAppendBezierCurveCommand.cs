namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAppendBezierCurveCommand : PdfCommand
    {
        internal const string Name = "c";
        private readonly double x1;
        private readonly double y1;
        private readonly double x2;
        private readonly double y2;
        private readonly double x3;
        private readonly double y3;

        internal PdfAppendBezierCurveCommand(PdfStack operands)
        {
            this.y3 = operands.PopDouble();
            this.x3 = operands.PopDouble();
            this.y2 = operands.PopDouble();
            this.x2 = operands.PopDouble();
            this.y1 = operands.PopDouble();
            this.x1 = operands.PopDouble();
        }

        public PdfAppendBezierCurveCommand(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.x3 = x3;
            this.y3 = y3;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.AppendPathBezierSegment(new PdfPoint(this.x1, this.y1), new PdfPoint(this.x2, this.y2), new PdfPoint(this.x3, this.y3));
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.x1);
            writer.WriteSpace();
            writer.WriteDouble(this.y1);
            writer.WriteSpace();
            writer.WriteDouble(this.x2);
            writer.WriteSpace();
            writer.WriteDouble(this.y2);
            writer.WriteSpace();
            writer.WriteDouble(this.x3);
            writer.WriteSpace();
            writer.WriteDouble(this.y3);
            writer.WriteSpace();
            writer.WriteString("c");
        }

        public double X1 =>
            this.x1;

        public double Y1 =>
            this.y1;

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

