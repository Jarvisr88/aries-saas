namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfSetColorSpaceCommand : PdfCommand
    {
        private readonly string colorSpaceName;
        private readonly PdfColorSpace colorSpace;

        protected PdfSetColorSpaceCommand(PdfColorSpace colorSpace)
        {
            if (colorSpace == null)
            {
                throw new ArgumentNullException("colorSpace");
            }
            this.colorSpace = colorSpace;
        }

        protected PdfSetColorSpaceCommand(PdfResources resources, PdfStack operands)
        {
            this.colorSpaceName = operands.PopName(true);
            this.colorSpace = resources.GetColorSpace(this.colorSpaceName);
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            PdfColorSpace colorSpace = this.ColorSpace ?? interpreter.PageResources.GetColorSpace(this.colorSpaceName);
            if (colorSpace != null)
            {
                this.Execute(interpreter, colorSpace);
            }
        }

        protected abstract void Execute(PdfCommandInterpreter interpreter, PdfColorSpace colorSpace);
        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            if (string.IsNullOrEmpty(this.colorSpaceName))
            {
                writer.WriteObject(this.colorSpace.Write(null), -1);
            }
            else
            {
                writer.WriteName(new PdfName(this.colorSpaceName));
            }
            writer.WriteSpace();
        }

        public PdfColorSpace ColorSpace =>
            this.colorSpace;
    }
}

