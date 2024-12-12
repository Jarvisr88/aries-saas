namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfShowTextOnNextLineWithSpacingCommand : PdfShowTextCommand
    {
        internal const string Name = "\"";
        private readonly double wordSpacing;
        private readonly double characterSpacing;

        internal PdfShowTextOnNextLineWithSpacingCommand(PdfStack operands)
        {
            byte[] buffer = operands.Pop(true) as byte[];
            if (buffer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            base.Text = buffer;
            this.characterSpacing = operands.PopDouble();
            this.wordSpacing = operands.PopDouble();
        }

        public PdfShowTextOnNextLineWithSpacingCommand(byte[] text, double wordSpacing, double characterSpacing) : base(text)
        {
            this.wordSpacing = wordSpacing;
            this.characterSpacing = characterSpacing;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetWordSpacing(this.wordSpacing);
            interpreter.SetCharacterSpacing(this.characterSpacing);
            interpreter.MoveToNextLine();
            interpreter.DrawString(base.Text, null);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.wordSpacing);
            writer.WriteSpace();
            writer.WriteDouble(this.characterSpacing);
            writer.WriteSpace();
            writer.WriteHexadecimalString(base.Text, -1);
            writer.WriteSpace();
            writer.WriteString("\"");
        }

        public double WordSpacing =>
            this.wordSpacing;

        public double CharacterSpacing =>
            this.characterSpacing;
    }
}

