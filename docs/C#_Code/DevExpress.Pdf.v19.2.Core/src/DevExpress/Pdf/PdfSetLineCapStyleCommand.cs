namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfSetLineCapStyleCommand : PdfCommand
    {
        internal const string Name = "J";
        private static readonly List<int> supportedStyles;
        private readonly PdfLineCapStyle lineCapStyle;

        static PdfSetLineCapStyleCommand()
        {
            Array values = Enum.GetValues(typeof(PdfLineCapStyle));
            supportedStyles = new List<int>(values.Length);
            foreach (PdfLineCapStyle style in values)
            {
                supportedStyles.Add((int) style);
            }
        }

        internal PdfSetLineCapStyleCommand(PdfStack operands)
        {
            this.lineCapStyle = ConvertToLineCapStyle(operands.PopInt());
        }

        public PdfSetLineCapStyleCommand(PdfLineCapStyle lineCapStyle)
        {
            this.lineCapStyle = lineCapStyle;
        }

        internal static PdfLineCapStyle ConvertToLineCapStyle(int styleIndex)
        {
            if (!supportedStyles.Contains(styleIndex))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (PdfLineCapStyle) styleIndex;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetLineCapStyle(this.lineCapStyle);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteInt((int) this.lineCapStyle);
            writer.WriteSpace();
            writer.WriteString("J");
        }

        public PdfLineCapStyle LineCapStyle =>
            this.lineCapStyle;
    }
}

