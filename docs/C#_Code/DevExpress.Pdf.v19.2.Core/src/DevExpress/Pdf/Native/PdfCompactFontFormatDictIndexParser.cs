namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfCompactFontFormatDictIndexParser
    {
        private readonly byte[] data;
        private int currentPosition;

        protected PdfCompactFontFormatDictIndexParser(byte[] data)
        {
            this.data = data;
        }

        protected byte GetNextByte()
        {
            if (!this.HasMoreData)
            {
                return 0;
            }
            int currentPosition = this.currentPosition;
            this.currentPosition = currentPosition + 1;
            return this.data[currentPosition];
        }

        protected IList<PdfCompactFontFormatDictIndexOperator> Parse()
        {
            List<PdfCompactFontFormatDictIndexOperator> list = new List<PdfCompactFontFormatDictIndexOperator>();
            List<object> operands = new List<object>();
            while (this.HasMoreData)
            {
                byte nextByte = this.GetNextByte();
                if (nextByte <= 0x1b)
                {
                    PdfCompactFontFormatDictIndexOperator item = this.ParseOperator(nextByte, operands);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                    operands = new List<object>();
                    continue;
                }
                if (nextByte == 0x1c)
                {
                    operands.Add((int) ((short) ((this.GetNextByte() << 8) + this.GetNextByte())));
                    continue;
                }
                if (nextByte == 0x1d)
                {
                    operands.Add((((this.GetNextByte() << 0x18) + (this.GetNextByte() << 0x10)) + (this.GetNextByte() << 8)) + this.GetNextByte());
                    continue;
                }
                if (nextByte == 30)
                {
                    PdfCompactFontFormatNibbleValueConstructor constructor = new PdfCompactFontFormatNibbleValueConstructor();
                    while (true)
                    {
                        if (this.HasMoreData)
                        {
                            nextByte = this.GetNextByte();
                            if (!constructor.AddNibble(nextByte >> 4) && !constructor.AddNibble(nextByte & 15))
                            {
                                continue;
                            }
                        }
                        operands.Add(constructor.Result);
                        break;
                    }
                    continue;
                }
                if ((nextByte >= 0x20) && (nextByte <= 0xf6))
                {
                    operands.Add(nextByte - 0x8b);
                    continue;
                }
                if ((nextByte >= 0xf7) && (nextByte <= 250))
                {
                    operands.Add((((nextByte - 0xf7) * 0x100) + this.GetNextByte()) + 0x6c);
                    continue;
                }
                if ((nextByte >= 0xfb) && (nextByte <= 0xfe))
                {
                    operands.Add((((0xfb - nextByte) * 0x100) - this.GetNextByte()) - 0x6c);
                }
            }
            return list;
        }

        protected abstract PdfCompactFontFormatDictIndexOperator ParseOperator(byte value, IList<object> operands);

        protected bool HasMoreData =>
            this.currentPosition < this.data.Length;
    }
}

