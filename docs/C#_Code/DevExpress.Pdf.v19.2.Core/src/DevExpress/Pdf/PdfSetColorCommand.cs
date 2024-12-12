namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfSetColorCommand : PdfCommand
    {
        private double[] components;

        protected PdfSetColorCommand(PdfStack operands)
        {
            this.Parse(operands);
        }

        protected void Parse(PdfStack operands)
        {
            int count = operands.Count;
            this.components = new double[count];
            for (int i = count - 1; i >= 0; i--)
            {
                this.components[i] = operands.PopDouble();
            }
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            foreach (double num2 in this.components)
            {
                writer.WriteSpace();
                writer.WriteDouble(num2);
            }
            writer.WriteSpace();
        }

        public double[] Components =>
            this.components;

        protected PdfColor Color =>
            new PdfColor((double[]) this.components.Clone());
    }
}

