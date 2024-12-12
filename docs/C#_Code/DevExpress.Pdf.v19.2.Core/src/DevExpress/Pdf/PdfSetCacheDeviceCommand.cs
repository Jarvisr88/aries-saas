namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetCacheDeviceCommand : PdfCommand
    {
        internal const string Name = "d1";
        private readonly double charWidth;
        private readonly PdfRectangle boundingBox;

        internal PdfSetCacheDeviceCommand(PdfStack operands)
        {
            double x = operands.PopDouble();
            double num4 = operands.PopDouble();
            this.boundingBox = new PdfRectangle(new PdfPoint(num4, operands.PopDouble()), new PdfPoint(x, operands.PopDouble()));
            if (operands.PopDouble() != 0.0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.charWidth = operands.PopDouble();
        }

        public PdfSetCacheDeviceCommand(double charWidth, PdfRectangle boundingBox)
        {
            this.charWidth = charWidth;
            this.boundingBox = boundingBox;
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.charWidth);
            writer.WriteSpace();
            writer.WriteInt(0);
            writer.WriteSpace();
            writer.WriteDouble(this.boundingBox.Left);
            writer.WriteSpace();
            writer.WriteDouble(this.boundingBox.Bottom);
            writer.WriteSpace();
            writer.WriteDouble(this.boundingBox.Right);
            writer.WriteSpace();
            writer.WriteDouble(this.boundingBox.Top);
            writer.WriteSpace();
            writer.WriteString("d1");
        }

        public double CharWidth =>
            this.charWidth;

        public PdfRectangle BoundingBox =>
            this.boundingBox;
    }
}

