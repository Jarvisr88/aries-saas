namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfSetGrayColorSpaceCommand : PdfCommand
    {
        private readonly double gray;

        protected PdfSetGrayColorSpaceCommand(PdfStack operands)
        {
            this.gray = PdfColor.ClipColorComponent(operands.PopDouble());
        }

        protected PdfSetGrayColorSpaceCommand(double gray)
        {
            this.gray = PdfColor.ClipColorComponent(gray);
        }

        public double Gray =>
            this.gray;
    }
}

