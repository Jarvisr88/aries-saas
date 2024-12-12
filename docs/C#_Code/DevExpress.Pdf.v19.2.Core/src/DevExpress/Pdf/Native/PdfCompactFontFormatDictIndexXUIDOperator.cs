namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexXUIDOperator : PdfCompactFontFormatDictIndexOperator
    {
        public const byte Code = 14;
        private readonly int[] xuid;

        public PdfCompactFontFormatDictIndexXUIDOperator(IList<object> operands)
        {
            int count = operands.Count;
            this.xuid = new int[count];
            for (int i = 0; i < count; i++)
            {
                object obj2 = operands[i];
                if (!(obj2 is int))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.xuid[i] = (int) obj2;
            }
        }

        public PdfCompactFontFormatDictIndexXUIDOperator(int[] xuid)
        {
            this.xuid = xuid;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.XUID = this.xuid;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex)
        {
            int num = 0;
            foreach (int num3 in this.xuid)
            {
                num += CalcIntegerSize(num3);
            }
            return (base.GetSize(stringIndex) + num);
        }

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            foreach (int num2 in this.xuid)
            {
                WriteInteger(stream, num2);
            }
            stream.WriteByte(14);
        }
    }
}

