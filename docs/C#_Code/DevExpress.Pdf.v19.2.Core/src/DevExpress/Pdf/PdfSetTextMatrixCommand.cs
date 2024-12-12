namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetTextMatrixCommand : PdfCommand
    {
        internal const string Name = "Tm";
        private readonly PdfTransformationMatrix textMatrix;

        public PdfSetTextMatrixCommand(PdfTransformationMatrix textMatrix)
        {
            this.textMatrix = textMatrix;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetTextMatrix(this.textMatrix);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            this.textMatrix.Write(writer);
            writer.WriteSpace();
            writer.WriteString("Tm");
        }

        public PdfTransformationMatrix TextMatrix =>
            this.textMatrix;
    }
}

