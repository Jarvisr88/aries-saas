namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class PdfStreamWriter : IDisposable
    {
        private static readonly Encoding utf8encoding = Encoding.UTF8;
        private static readonly CultureInfo invariantCulture = CultureInfo.InvariantCulture;
        private const byte space = 0x20;
        private const byte closeBracket = 0x5d;
        private const byte openBracket = 0x5b;
        private readonly System.IO.Stream stream;

        public PdfStreamWriter() : this(new MemoryStream())
        {
        }

        public PdfStreamWriter(System.IO.Stream stream)
        {
            this.stream = stream;
        }

        public void Dispose()
        {
            this.stream.Dispose();
        }

        private FloatingPointNumberFormatInfo GetFloatingPointNumberFormatInfo(double value) => 
            (value <= 0.001) ? ((value <= 1E-05) ? new FloatingPointNumberFormatInfo(5, 1E+10f) : ((value <= 0.0001) ? new FloatingPointNumberFormatInfo(4, 1E+09f) : new FloatingPointNumberFormatInfo(3, 1E+08f))) : ((value <= 0.1) ? ((value <= 0.01) ? new FloatingPointNumberFormatInfo(2, 1E+07f) : new FloatingPointNumberFormatInfo(1, 1000000f)) : new FloatingPointNumberFormatInfo(0, 100000f));

        public void Write(byte[] data)
        {
            this.Write(data, 0, data.Length);
        }

        public void Write(byte[] data, int offset, int length)
        {
            this.stream.Write(data, offset, length);
        }

        public void WriteByte(byte space)
        {
            this.stream.WriteByte(space);
        }

        public void WriteClosedBracket()
        {
            this.stream.WriteByte(0x5d);
        }

        public void WriteDouble(double value, bool prependSpace = true)
        {
            if (prependSpace)
            {
                this.stream.WriteByte(0x20);
            }
            if (((value % 1.0) == 0.0) && ((value <= 2147483647.0) && (value >= -2147483648.0)))
            {
                this.WriteInt((int) value);
            }
            else if (Math.Abs(value) < 1E-06)
            {
                this.WriteString(((float) value).ToString("0.############################################", invariantCulture));
            }
            else
            {
                if (value < 0.0)
                {
                    this.stream.WriteByte(0x2d);
                    value = -value;
                }
                if (value < 1.0)
                {
                    FloatingPointNumberFormatInfo floatingPointNumberFormatInfo = this.GetFloatingPointNumberFormatInfo(value);
                    value += (1f / floatingPointNumberFormatInfo.RoundingConst) / 2f;
                    if (value >= 1.0)
                    {
                        this.stream.WriteByte(0x31);
                    }
                    else
                    {
                        int num2 = (int) (value * floatingPointNumberFormatInfo.RoundingConst);
                        this.stream.WriteByte(0x30);
                        this.stream.WriteByte(0x2e);
                        for (int i = 0; i < (floatingPointNumberFormatInfo.NumberOfLeadingZeroes - 1); i++)
                        {
                            this.stream.WriteByte(0x30);
                        }
                        if (num2 >= 0x186a0)
                        {
                            this.stream.WriteByte(0x31);
                        }
                        else
                        {
                            if (floatingPointNumberFormatInfo.NumberOfLeadingZeroes != 0)
                            {
                                this.stream.WriteByte(0x30);
                            }
                            this.stream.WriteByte((byte) ((num2 / 0x2710) + 0x30));
                            if ((num2 % 0x2710) != 0)
                            {
                                this.stream.WriteByte((byte) (((num2 / 0x3e8) % 10) + 0x30));
                                if ((num2 % 0x3e8) != 0)
                                {
                                    this.stream.WriteByte((byte) (((num2 / 100) % 10) + 0x30));
                                    if ((num2 % 100) != 0)
                                    {
                                        this.stream.WriteByte((byte) (((num2 / 10) % 10) + 0x30));
                                        if ((num2 % 10) != 0)
                                        {
                                            this.stream.WriteByte((byte) ((num2 % 10) + 0x30));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (value > 32767.0)
                {
                    this.WriteString(((float) value).ToString("0.##", invariantCulture));
                }
                else
                {
                    value += 5E-05;
                    int num4 = (int) (value * 10000.0);
                    if (num4 >= 0x5f5e100)
                    {
                        this.stream.WriteByte((byte) (((num4 / 0x5f5e100) % 10) + 0x30));
                    }
                    if (num4 >= 0x989680)
                    {
                        this.stream.WriteByte((byte) (((num4 / 0x989680) % 10) + 0x30));
                    }
                    if (num4 >= 0xf4240)
                    {
                        this.stream.WriteByte((byte) (((num4 / 0xf4240) % 10) + 0x30));
                    }
                    if (num4 >= 0x186a0)
                    {
                        this.stream.WriteByte((byte) (((num4 / 0x186a0) % 10) + 0x30));
                    }
                    if (num4 >= 0x2710)
                    {
                        this.stream.WriteByte((byte) (((num4 / 0x2710) % 10) + 0x30));
                    }
                    if ((num4 % 0x2710) != 0)
                    {
                        this.stream.WriteByte(0x2e);
                        this.stream.WriteByte((byte) (((num4 / 0x3e8) % 10) + 0x30));
                        if ((num4 % 0x3e8) != 0)
                        {
                            this.stream.WriteByte((byte) (((num4 / 100) % 10) + 0x30));
                            if ((num4 % 100) != 0)
                            {
                                this.stream.WriteByte((byte) (((num4 / 10) % 10) + 0x30));
                                if ((num4 % 10) != 0)
                                {
                                    this.stream.WriteByte((byte) ((num4 % 10) + 0x30));
                                }
                            }
                        }
                    }
                }
            }
        }

        public void WriteDoubleArray(double[] data)
        {
            this.WriteDoubleArray(data, data.Length);
        }

        public void WriteDoubleArray(double[] data, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.WriteDouble(data[i], true);
            }
        }

        public void WriteHexadecimalString(IList<byte> data)
        {
            int count = data.Count;
            this.stream.WriteByte(60);
            for (int i = 0; i < count; i++)
            {
                byte num = data[i];
                int num4 = num >> 4;
                this.stream.WriteByte((num4 > 9) ? ((byte) (num4 + 0x37)) : ((byte) (num4 + 0x30)));
                num4 = num & 15;
                this.stream.WriteByte((num4 > 9) ? ((byte) (num4 + 0x37)) : ((byte) (num4 + 0x30)));
            }
            this.stream.WriteByte(0x3e);
        }

        public void WriteInt(int value)
        {
            this.WriteString(value.ToString(invariantCulture));
        }

        public void WriteOpenBracket()
        {
            this.stream.WriteByte(0x5b);
        }

        public void WriteSpace()
        {
            this.stream.WriteByte(0x20);
        }

        public void WriteString(string s)
        {
            byte[] bytes = utf8encoding.GetBytes(s);
            this.stream.Write(bytes, 0, bytes.Length);
        }

        public System.IO.Stream Stream =>
            this.stream;

        public byte[] Commands
        {
            get
            {
                byte[] buffer1;
                MemoryStream stream = this.stream as MemoryStream;
                if (stream != null)
                {
                    buffer1 = stream.ToArray();
                }
                else
                {
                    MemoryStream local1 = stream;
                    buffer1 = null;
                }
                byte[] buffer = buffer1;
                if (buffer == null)
                {
                    long position = this.stream.Position;
                    try
                    {
                        buffer = new byte[this.stream.Length];
                        this.stream.Position = 0L;
                        this.stream.Read(buffer, 0, buffer.Length);
                    }
                    finally
                    {
                        this.stream.Position = position;
                    }
                }
                return buffer;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FloatingPointNumberFormatInfo
        {
            public int NumberOfLeadingZeroes { get; }
            public float RoundingConst { get; }
            public FloatingPointNumberFormatInfo(int numberOfLeadingZeroes, float roundingConst)
            {
                this.<NumberOfLeadingZeroes>k__BackingField = numberOfLeadingZeroes;
                this.<RoundingConst>k__BackingField = roundingConst;
            }
        }
    }
}

