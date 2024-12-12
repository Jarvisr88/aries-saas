namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfAppendRectangleCommand : PdfCommand
    {
        internal const string Name = "re";
        private readonly double x;
        private readonly double y;
        private readonly double width;
        private readonly double height;

        internal PdfAppendRectangleCommand(PdfStack operands)
        {
            this.height = operands.PopDouble();
            this.width = operands.PopDouble();
            this.y = operands.PopDouble();
            this.x = operands.PopDouble();
        }

        public PdfAppendRectangleCommand(double x, double y, double width, double height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.AppendRectangle(this.x, this.y, this.width, this.height);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.x);
            writer.WriteSpace();
            writer.WriteDouble(this.y);
            writer.WriteSpace();
            writer.WriteDouble(this.width);
            writer.WriteSpace();
            writer.WriteDouble(this.height);
            writer.WriteSpace();
            writer.WriteString("re");
        }

        public double X =>
            this.x;

        public double Y =>
            this.y;

        public double Width =>
            this.width;

        public double Height =>
            this.height;
    }
}

