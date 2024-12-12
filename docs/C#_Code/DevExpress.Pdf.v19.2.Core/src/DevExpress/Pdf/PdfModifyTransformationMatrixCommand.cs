namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfModifyTransformationMatrixCommand : PdfCommand
    {
        internal const string Name = "cm";
        private readonly PdfTransformationMatrix matrix;

        public PdfModifyTransformationMatrixCommand(PdfTransformationMatrix matrix)
        {
            this.matrix = matrix;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.UpdateTransformationMatrix(this.matrix);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            this.matrix.Write(writer);
            writer.WriteSpace();
            writer.WriteString("cm");
        }

        public PdfTransformationMatrix Matrix =>
            this.matrix;
    }
}

