namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfCommandGroup : PdfCommand
    {
        private readonly PdfCommandList children = new PdfCommandList();

        protected PdfCommandGroup()
        {
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            foreach (PdfCommand command in this.children)
            {
                try
                {
                    command.Execute(interpreter);
                }
                catch
                {
                }
            }
        }

        protected abstract IEnumerable<object> GetPrefix(PdfResources resources);
        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            foreach (object obj2 in this.GetPrefix(resources))
            {
                writer.WriteSpace();
                writer.WriteObject(obj2, -1);
            }
            foreach (PdfCommand command in this.children)
            {
                command.Write(resources, writer);
            }
            writer.WriteSpace();
            writer.WriteObject(new PdfToken(this.Suffix), -1);
        }

        public PdfCommandList Children =>
            this.children;

        protected internal virtual bool ShouldIgnoreUnknownCommands =>
            false;

        protected abstract string Suffix { get; }
    }
}

