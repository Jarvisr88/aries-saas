namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfUnknownCommand : PdfCommand
    {
        private readonly string name;
        private readonly List<object> parameters = new List<object>();

        internal PdfUnknownCommand(string name, PdfStack operands)
        {
            this.name = name;
            for (int i = operands.Count - 1; i >= 0; i--)
            {
                this.parameters.Add(operands.Pop(true));
            }
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            foreach (object obj2 in this.parameters)
            {
                writer.WriteSpace();
                writer.WriteObject(obj2, -1);
            }
            writer.WriteSpace();
            writer.WriteString(this.Name);
        }

        public string Name =>
            this.name;

        public IList<object> Parameters =>
            this.parameters;
    }
}

