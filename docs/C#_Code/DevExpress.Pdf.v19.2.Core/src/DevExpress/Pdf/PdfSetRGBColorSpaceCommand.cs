namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfSetRGBColorSpaceCommand : PdfCommand
    {
        private readonly double r;
        private readonly double g;
        private readonly double b;

        protected PdfSetRGBColorSpaceCommand(PdfStack operands)
        {
            if (operands.Count != 1)
            {
                this.b = PdfColor.ClipColorComponent(operands.PopDouble());
                this.g = PdfColor.ClipColorComponent(operands.PopDouble());
                this.r = PdfColor.ClipColorComponent(operands.PopDouble());
            }
            else
            {
                IList<object> list = operands.Pop(true) as IList<object>;
                if ((list == null) || (list.Count != 3))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.r = PdfColor.ClipColorComponent(PdfDocumentReader.ConvertToDouble(list[0]));
                this.g = PdfColor.ClipColorComponent(PdfDocumentReader.ConvertToDouble(list[1]));
                this.b = PdfColor.ClipColorComponent(PdfDocumentReader.ConvertToDouble(list[2]));
            }
        }

        protected PdfSetRGBColorSpaceCommand(double r, double g, double b)
        {
            this.r = PdfColor.ClipColorComponent(r);
            this.g = PdfColor.ClipColorComponent(g);
            this.b = PdfColor.ClipColorComponent(b);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.r);
            writer.WriteSpace();
            writer.WriteDouble(this.g);
            writer.WriteSpace();
            writer.WriteDouble(this.b);
            writer.WriteSpace();
        }

        public double R =>
            this.r;

        public double G =>
            this.g;

        public double B =>
            this.b;
    }
}

