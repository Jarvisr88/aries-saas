namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public abstract class PdfCompactFontFormatDictIndexOperator
    {
        private const string doubleFormat = "G15";

        protected PdfCompactFontFormatDictIndexOperator()
        {
        }

        protected static int CalcDoubleArraySize(double[] array)
        {
            int num = 0;
            int length = array.Length;
            double num3 = 0.0;
            for (int i = 0; i < length; i++)
            {
                double num5 = array[i];
                num += CalcDoubleSize(num5 - num3);
                num3 = num5;
            }
            return num;
        }

        protected static int CalcDoubleSize(double value)
        {
            int num = (int) value;
            return ((num == value) ? CalcIntegerSize(num) : ((value.ToString("G15", CultureInfo.InvariantCulture).Length / 2) + 2));
        }

        protected static int CalcGlyphZonesSize(PdfType1FontGlyphZone[] glyphZones)
        {
            int num = 0;
            double num2 = 0.0;
            foreach (PdfType1FontGlyphZone zone in glyphZones)
            {
                double bottom = zone.Bottom;
                double top = zone.Top;
                num = (num + CalcDoubleSize(bottom - num2)) + CalcDoubleSize(top - bottom);
                num2 = top;
            }
            return num;
        }

        protected static int CalcIntegerSize(int value) => 
            (value < 0) ? ((value >= -32768) ? ((value >= -1131) ? ((value < -107) ? 2 : 1) : 3) : 5) : ((value <= 0x7fff) ? ((value <= 0x46b) ? ((value > 0x6b) ? 2 : 1) : 3) : 5);

        public virtual void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }

        public virtual void Execute(PdfType1FontCompactFontPrivateData privateData, PdfBinaryStream stream)
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }

        public virtual void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }

        protected static bool GetBoolean(IList<object> operands)
        {
            int integer = GetInteger(operands);
            if (integer != 0)
            {
                if (integer == 1)
                {
                    return true;
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return false;
        }

        protected static double GetDouble(IList<object> operands)
        {
            if (operands.Count != 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfDocumentReader.ConvertToDouble(operands[0]);
        }

        protected static double[] GetDoubleArray(IList<object> operands)
        {
            int count = operands.Count;
            double[] numArray = new double[count];
            double num2 = 0.0;
            for (int i = 0; i < count; i++)
            {
                double num4 = PdfDocumentReader.ConvertToDouble(operands[i]) + num2;
                numArray[i] = num4;
                num2 = num4;
            }
            return numArray;
        }

        protected static PdfType1FontGlyphZone[] GetGlyphZones(IList<object> operands)
        {
            int count = operands.Count;
            if ((count % 2) != 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            count /= 2;
            PdfType1FontGlyphZone[] zoneArray = new PdfType1FontGlyphZone[count];
            double num2 = 0.0;
            int index = 0;
            int num4 = 0;
            while (index < count)
            {
                double bottom = PdfDocumentReader.ConvertToDouble(operands[num4++]) + num2;
                double top = PdfDocumentReader.ConvertToDouble(operands[num4++]) + bottom;
                zoneArray[index] = new PdfType1FontGlyphZone(bottom, top);
                num2 = top;
                index++;
            }
            return zoneArray;
        }

        protected static int GetInteger(IList<object> operands)
        {
            if (operands.Count != 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfDocumentReader.ConvertToInteger(operands[0]);
        }

        public virtual int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            1;

        protected static PdfType1FontCompactCIDFontProgram ToCIDFontProgram(PdfType1FontCompactFontProgram fontProgram)
        {
            PdfType1FontCompactCIDFontProgram program = fontProgram as PdfType1FontCompactCIDFontProgram;
            if (program == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return program;
        }

        public virtual void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }

        protected static void WriteBoolean(PdfBinaryStream stream, bool value)
        {
            stream.WriteByte(value ? ((byte) 140) : ((byte) 0x8b));
        }

        protected static void WriteDouble(PdfBinaryStream stream, double value)
        {
            int num = (int) value;
            if (num == value)
            {
                WriteInteger(stream, num);
                return;
            }
            List<byte> list = new List<byte>();
            string str = value.ToString("G15", CultureInfo.InvariantCulture);
            int num3 = 0;
            goto TR_0015;
        TR_0009:
            num3++;
        TR_0015:
            while (true)
            {
                if (num3 >= str.Length)
                {
                    list.Add(15);
                    int count = list.Count;
                    if ((count % 2) > 0)
                    {
                        list.Add(15);
                        count++;
                    }
                    stream.WriteByte(30);
                    int num5 = 0;
                    int num6 = 0;
                    while (num6 < count)
                    {
                        stream.WriteByte((list[num6++] << 4) + list[num6++]);
                        num5++;
                    }
                    return;
                }
                char ch = str[num3];
                switch (ch)
                {
                    case '+':
                        goto TR_0009;

                    case ',':
                        break;

                    case '-':
                    {
                        int num4 = list.Count - 1;
                        if (num4 >= 0)
                        {
                            list[num4] = 12;
                        }
                        else
                        {
                            list.Add(14);
                        }
                        goto TR_0009;
                    }
                    case '.':
                        list.Add(10);
                        goto TR_0009;

                    default:
                        if (ch != 'E')
                        {
                            break;
                        }
                        list.Add(11);
                        goto TR_0009;
                }
                list.Add((byte) (ch - '0'));
                break;
            }
            goto TR_0009;
        }

        protected static void WriteDoubleArray(PdfBinaryStream stream, double[] array)
        {
            double num = 0.0;
            foreach (double num3 in array)
            {
                WriteDouble(stream, num3 - num);
                num = num3;
            }
        }

        protected static void WriteGlyphZones(PdfBinaryStream stream, PdfType1FontGlyphZone[] glyphZones)
        {
            double num = 0.0;
            foreach (PdfType1FontGlyphZone zone in glyphZones)
            {
                double bottom = zone.Bottom;
                WriteDouble(stream, bottom - num);
                double top = zone.Top;
                WriteDouble(stream, top - bottom);
                num = top;
            }
        }

        protected static void WriteInteger(PdfBinaryStream stream, int value)
        {
            if ((value > 0x7fff) || (value < -32768))
            {
                stream.WriteByte(0x1d);
                stream.WriteByte((byte) (value >> 0x18));
                stream.WriteByte((byte) ((value >> 0x10) & 0xff));
                stream.WriteByte((byte) ((value >> 8) & 0xff));
                stream.WriteByte((byte) (value & 0xff));
            }
            else if ((value > 0x46b) || (value < -1131))
            {
                stream.WriteByte(0x1c);
                stream.WriteByte((byte) (value >> 8));
                stream.WriteByte((byte) (value & 0xff));
            }
            else if (value >= 0x6c)
            {
                value -= 0x6c;
                stream.WriteByte((byte) ((value / 0x100) + 0xf7));
                stream.WriteByte((byte) (value % 0x100));
            }
            else if (value > -108)
            {
                stream.WriteByte((byte) (value + 0x8b));
            }
            else
            {
                value = -value - 0x6c;
                stream.WriteByte((byte) ((value / 0x100) + 0xfb));
                stream.WriteByte((byte) (value % 0x100));
            }
        }
    }
}

