namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfSetColorAdvancedCommand : PdfSetColorCommand
    {
        private readonly string patternName;
        private readonly PdfPattern pattern;

        protected PdfSetColorAdvancedCommand(PdfResources resources, PdfStack operands) : base(new PdfStack())
        {
            object obj2 = operands.Pop(true);
            PdfName name = obj2 as PdfName;
            if (name == null)
            {
                operands.Push(obj2);
            }
            else
            {
                this.patternName = name.Name;
                if (!string.IsNullOrEmpty(this.patternName))
                {
                    this.pattern = resources.GetPattern(this.patternName);
                }
            }
            base.Parse(operands);
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            if (string.IsNullOrEmpty(this.patternName))
            {
                this.Execute(interpreter, new PdfColor(null, (double[]) base.Components.Clone()));
            }
            else
            {
                PdfPattern pattern = this.pattern ?? interpreter.PageResources.GetPattern(this.patternName);
                if (pattern != null)
                {
                    this.Execute(interpreter, new PdfColor(pattern, (double[]) base.Components.Clone()));
                }
            }
        }

        protected abstract void Execute(PdfCommandInterpreter interpreter, PdfColor color);
        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            base.Write(resources, writer);
            if (!string.IsNullOrEmpty(this.patternName))
            {
                writer.WriteName(new PdfName(this.patternName));
                writer.WriteSpace();
            }
        }

        public PdfPattern Pattern =>
            this.pattern;
    }
}

