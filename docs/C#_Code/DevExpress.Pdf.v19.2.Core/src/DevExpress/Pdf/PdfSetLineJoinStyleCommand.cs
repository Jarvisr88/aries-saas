namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfSetLineJoinStyleCommand : PdfCommand
    {
        internal const string Name = "j";
        private static readonly List<int> supportedStyles;
        private readonly PdfLineJoinStyle lineJoinStyle;

        static PdfSetLineJoinStyleCommand()
        {
            Array values = Enum.GetValues(typeof(PdfLineJoinStyle));
            supportedStyles = new List<int>(values.Length);
            foreach (PdfLineCapStyle style in values)
            {
                supportedStyles.Add((int) style);
            }
        }

        internal PdfSetLineJoinStyleCommand(PdfStack operands)
        {
            this.lineJoinStyle = ConvertToLineJoinStyle(operands.PopInt());
        }

        public PdfSetLineJoinStyleCommand(PdfLineJoinStyle lineJoinStyle)
        {
            this.lineJoinStyle = lineJoinStyle;
        }

        internal static PdfLineJoinStyle ConvertToLineJoinStyle(int styleIndex)
        {
            if (!supportedStyles.Contains(styleIndex))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (PdfLineJoinStyle) styleIndex;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.SetLineJoinStyle(this.lineJoinStyle);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteInt((int) this.lineJoinStyle);
            writer.WriteSpace();
            writer.WriteString("j");
        }

        public PdfLineJoinStyle LineJoinStyle =>
            this.lineJoinStyle;
    }
}

