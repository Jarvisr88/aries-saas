namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSetCharacterSpacingCommand : PdfCommand
    {
        internal const string Name = "Tc";
        private readonly double characterSpacing;

        internal PdfSetCharacterSpacingCommand(PdfStack operands)
        {
            this.characterSpacing = operands.PopDouble();
        }

        public PdfSetCharacterSpacingCommand(double characterSpacing)
        {
            this.characterSpacing = characterSpacing;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetCharacterSpacing(this.characterSpacing);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteDouble(this.characterSpacing);
            writer.WriteSpace();
            writer.WriteString("Tc");
        }

        public double CharacterSpacing =>
            this.characterSpacing;
    }
}

