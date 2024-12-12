namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfShowTextCommand : PdfCommand
    {
        internal const string Name = "Tj";
        private byte[] text;

        protected PdfShowTextCommand()
        {
        }

        public PdfShowTextCommand(byte[] text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectText), "text");
            }
            this.text = text;
        }

        internal PdfShowTextCommand(PdfStack operands)
        {
            if (operands.Count == 0)
            {
                this.text = new byte[0];
            }
            else
            {
                this.text = operands.Pop(true) as byte[];
                if (this.text == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.DrawString(this.Text, null);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteHexadecimalString(this.text, -1);
            writer.WriteSpace();
            writer.WriteString("Tj");
        }

        public byte[] Text
        {
            get => 
                this.text;
            protected set => 
                this.text = value;
        }
    }
}

