namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetGraphicsStateParametersCommand : PdfCommand
    {
        internal const string Name = "gs";
        private readonly string parametersName;
        private readonly PdfGraphicsStateParameters parameters;

        public PdfSetGraphicsStateParametersCommand(PdfGraphicsStateParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            this.parameters = parameters;
        }

        internal PdfSetGraphicsStateParametersCommand(PdfResources resources, PdfStack operands)
        {
            this.parametersName = operands.PopName(true);
            this.parameters = resources.GetGraphicsStateParameters(this.parametersName);
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            PdfGraphicsStateParameters parameters = this.parameters ?? interpreter.PageResources.GetGraphicsStateParameters(this.parametersName);
            if (parameters != null)
            {
                interpreter.ApplyGraphicsStateParameters(parameters);
            }
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteName(string.IsNullOrEmpty(this.parametersName) ? resources.FindGraphicsStateParametersName(this.parameters) : new PdfName(this.parametersName));
            writer.WriteSpace();
            writer.WriteString("gs");
        }

        public PdfGraphicsStateParameters Parameters =>
            this.parameters;
    }
}

