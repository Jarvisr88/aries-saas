namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfSetTextRenderingModeCommand : PdfCommand
    {
        internal const string Name = "Tr";
        private static readonly List<int> supportedModes;
        private readonly PdfTextRenderingMode textRenderingMode;

        static PdfSetTextRenderingModeCommand()
        {
            Array values = Enum.GetValues(typeof(PdfTextRenderingMode));
            supportedModes = new List<int>(values.Length);
            foreach (PdfTextRenderingMode mode in values)
            {
                supportedModes.Add((int) mode);
            }
        }

        internal PdfSetTextRenderingModeCommand(PdfStack operands)
        {
            int item = operands.PopInt();
            if (!supportedModes.Contains(item))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.textRenderingMode = (PdfTextRenderingMode) item;
        }

        public PdfSetTextRenderingModeCommand(PdfTextRenderingMode textRenderingMode)
        {
            this.textRenderingMode = textRenderingMode;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetTextRenderingMode(this.textRenderingMode);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteInt((int) this.textRenderingMode);
            writer.WriteSpace();
            writer.WriteString("Tr");
        }

        public PdfTextRenderingMode TextRenderingMode =>
            this.textRenderingMode;
    }
}

