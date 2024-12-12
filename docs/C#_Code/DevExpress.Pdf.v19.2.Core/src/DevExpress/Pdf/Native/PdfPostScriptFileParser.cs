namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfPostScriptFileParser : PdfObjectParser
    {
        private const byte beginProcedure = 0x7b;
        private const byte endProcedure = 0x7d;
        private const byte radixNumberIdentifier = 0x23;
        private const byte zero = 0x30;
        private const byte one = 0x31;
        private readonly List<object> operators;
        private readonly bool shouldExpectClosing;
        private bool closed;

        public PdfPostScriptFileParser(byte[] data) : base(new PdfArrayDataStream(data, data.Length), 0L)
        {
        }

        private PdfPostScriptFileParser(PdfDataStream stream, long position) : base(stream, position)
        {
            this.shouldExpectClosing = position != 0L;
            this.operators = new List<object>();
        }

        private void Parse()
        {
            while (true)
            {
                object item = this.ReadNextObject();
                if (item == null)
                {
                    if (this.shouldExpectClosing && !this.closed)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return;
                }
                this.operators.Add(item);
            }
        }

        public static IList<object> Parse(byte[] data) => 
            Parse(data, data.Length);

        public static IList<object> Parse(byte[] data, int dataLength)
        {
            PdfPostScriptFileParser parser = new PdfPostScriptFileParser(new PdfArrayDataStream(data, dataLength), 0L);
            parser.Parse();
            return parser.operators;
        }

        protected override object ReadAlphabeticalObject(bool isHexadecimalStringSeparatedUsingWhiteSpaces, bool isIndirect)
        {
            byte current = base.Current;
            if (current == 0x7b)
            {
                PdfPostScriptFileParser parser = new PdfPostScriptFileParser(base.Stream, base.CurrentPosition + 1L);
                parser.Parse();
                base.CurrentPosition = parser.CurrentPosition + 1L;
                return parser.operators;
            }
            if (current != 0x7d)
            {
                return base.ReadAlphabeticalObject(isHexadecimalStringSeparatedUsingWhiteSpaces, isIndirect);
            }
            if (!this.shouldExpectClosing)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.closed = true;
            return null;
        }

        public object ReadNextObject()
        {
            object obj2 = base.ReadObject(false, false);
            return ((obj2 == null) ? null : PdfPostScriptOperator.Parse(obj2));
        }

        protected override object ReadNumericObject()
        {
            if (base.Current == 0x2d)
            {
                if (!base.ReadNext())
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                byte current = base.Current;
                base.ReadPrev();
                if (current == 0x7c)
                {
                    return null;
                }
            }
            object obj2 = base.ReadNumericObject();
            if ((base.Current == 0x23) && (obj2 is int))
            {
                int num2 = (int) obj2;
                if (num2 == 2)
                {
                    Func<byte, bool> checkByte = <>c.<>9__18_0;
                    if (<>c.<>9__18_0 == null)
                    {
                        Func<byte, bool> local1 = <>c.<>9__18_0;
                        checkByte = <>c.<>9__18_0 = current => (current == 0x30) || (current == 0x31);
                    }
                    return this.ReadRadixNumber(checkByte, <>c.<>9__18_1 ??= (result, current) => ((result * 2) + ConvertToDigit(current)));
                }
                if (num2 == 8)
                {
                    Func<byte, bool> checkByte = <>c.<>9__18_2;
                    if (<>c.<>9__18_2 == null)
                    {
                        Func<byte, bool> local3 = <>c.<>9__18_2;
                        checkByte = <>c.<>9__18_2 = current => IsOctalDigitSymbol(current);
                    }
                    return this.ReadRadixNumber(checkByte, <>c.<>9__18_3 ??= (result, current) => ((result * 8) + ConvertToDigit(current)));
                }
                if (num2 == 0x10)
                {
                    Func<byte, bool> checkByte = <>c.<>9__18_4;
                    if (<>c.<>9__18_4 == null)
                    {
                        Func<byte, bool> local5 = <>c.<>9__18_4;
                        checkByte = <>c.<>9__18_4 = current => IsHexadecimalDigitSymbol(current);
                    }
                    return this.ReadRadixNumber(checkByte, <>c.<>9__18_5 ??= (result, current) => ((result * 0x10) + ConvertToHexadecimalDigit(current)));
                }
            }
            return obj2;
        }

        private int ReadRadixNumber(Func<byte, bool> checkByte, Func<int, byte, int> accumulate)
        {
            int num = 0;
            while (true)
            {
                if (base.ReadNext())
                {
                    byte current = base.Current;
                    if (checkByte(current))
                    {
                        num = accumulate(num, current);
                        continue;
                    }
                }
                return num;
            }
        }

        public int ReadString(byte[] str)
        {
            if (!base.IsSpace || !base.ReadNext())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num = base.Stream.Read(str, 0, str.Length);
            base.CurrentPosition += num;
            return num;
        }

        protected override bool CanContinueReading
        {
            get
            {
                byte current = base.Current;
                return (base.CanContinueReading && ((current != 0x7b) && (current != 0x7d)));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPostScriptFileParser.<>c <>9 = new PdfPostScriptFileParser.<>c();
            public static Func<byte, bool> <>9__18_0;
            public static Func<int, byte, int> <>9__18_1;
            public static Func<byte, bool> <>9__18_2;
            public static Func<int, byte, int> <>9__18_3;
            public static Func<byte, bool> <>9__18_4;
            public static Func<int, byte, int> <>9__18_5;

            internal bool <ReadNumericObject>b__18_0(byte current) => 
                (current == 0x30) || (current == 0x31);

            internal int <ReadNumericObject>b__18_1(int result, byte current) => 
                (result * 2) + PdfObjectParser.ConvertToDigit(current);

            internal bool <ReadNumericObject>b__18_2(byte current) => 
                PdfObjectParser.IsOctalDigitSymbol(current);

            internal int <ReadNumericObject>b__18_3(int result, byte current) => 
                (result * 8) + PdfObjectParser.ConvertToDigit(current);

            internal bool <ReadNumericObject>b__18_4(byte current) => 
                PdfObjectParser.IsHexadecimalDigitSymbol(current);

            internal int <ReadNumericObject>b__18_5(int result, byte current) => 
                (result * 0x10) + PdfObjectParser.ConvertToHexadecimalDigit(current);
        }
    }
}

