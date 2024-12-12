namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfShowTextWithGlyphPositioningCommand : PdfShowTextCommand
    {
        internal const string Name = "TJ";
        private readonly double[] glyphOffsets;

        internal PdfShowTextWithGlyphPositioningCommand(PdfStack operands)
        {
            IList<object> list = operands.Pop(true) as IList<object>;
            if (list == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            List<byte> list2 = new List<byte>();
            List<double> list3 = new List<double>();
            double item = 0.0;
            foreach (object obj2 in list)
            {
                byte[] collection = obj2 as byte[];
                if (collection == null)
                {
                    if (obj2 is double)
                    {
                        item += (double) obj2;
                        continue;
                    }
                    if (!(obj2 is int))
                    {
                        continue;
                    }
                    item += (int) obj2;
                    continue;
                }
                int length = collection.Length;
                if (length > 0)
                {
                    list2.AddRange(collection);
                    list3.Add(item);
                    int num3 = 1;
                    while (true)
                    {
                        if (num3 >= length)
                        {
                            item = 0.0;
                            break;
                        }
                        list3.Add(0.0);
                        num3++;
                    }
                }
            }
            list3.Add(item);
            this.glyphOffsets = list3.ToArray();
            base.Text = list2.ToArray();
        }

        public PdfShowTextWithGlyphPositioningCommand(byte[] text, double[] glyphOffsets) : base(text)
        {
            if (glyphOffsets.Length <= text.Length)
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectGlyphPosition));
            }
            this.glyphOffsets = glyphOffsets;
        }

        protected internal override void Execute(PdfCommandInterpreter interpreter)
        {
            interpreter.DrawString(base.Text, this.glyphOffsets);
        }

        protected internal override void Write(PdfResources resources, PdfDocumentStream writer)
        {
            writer.WriteSpace();
            writer.WriteOpenBracket();
            byte[] text = base.Text;
            int length = text.Length;
            int index = 0;
            while (index < length)
            {
                double num4 = this.glyphOffsets[index];
                if (num4 != 0.0)
                {
                    writer.WriteSpace();
                    writer.WriteDouble(num4);
                }
                writer.WriteSpace();
                List<byte> list1 = new List<byte>();
                list1.Add(text[index]);
                List<byte> list = list1;
                index++;
                while (true)
                {
                    if ((index >= length) || (this.glyphOffsets[index] != 0.0))
                    {
                        writer.WriteHexadecimalString(list.ToArray(), -1);
                        break;
                    }
                    list.Add(text[index]);
                    index++;
                }
            }
            double num2 = this.glyphOffsets[this.glyphOffsets.Length - 1];
            if (num2 != 0.0)
            {
                writer.WriteSpace();
                writer.WriteDouble(num2);
            }
            writer.WriteCloseBracket();
            writer.WriteSpace();
            writer.WriteString("TJ");
        }

        public double[] GlyphOffsets =>
            this.glyphOffsets;
    }
}

