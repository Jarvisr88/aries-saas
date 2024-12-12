namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetLineStyleCommand : PdfCommand
    {
        internal const string Name = "d";
        private readonly PdfLineStyle lineStyle;

        internal PdfSetLineStyleCommand(PdfStack operands)
        {
            object obj2 = operands.Pop(true);
            object[] parameters = new object[] { operands.Pop(true), obj2 };
            this.lineStyle = PdfLineStyle.Parse(parameters);
        }

        public PdfSetLineStyleCommand(PdfLineStyle lineStyle)
        {
            this.lineStyle = lineStyle;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetLineStyle(this.lineStyle);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            foreach (object obj2 in this.lineStyle.Data)
            {
                writer.WriteSpace();
                writer.WriteObject(obj2, -1);
            }
            writer.WriteSpace();
            writer.WriteString("d");
        }

        public PdfLineStyle LineStyle =>
            this.lineStyle;
    }
}

