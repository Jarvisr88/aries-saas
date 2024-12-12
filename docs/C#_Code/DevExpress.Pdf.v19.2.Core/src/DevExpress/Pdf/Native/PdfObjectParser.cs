namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PdfObjectParser
    {
        private const byte plus = 0x2b;
        private const byte period = 0x2e;
        private const byte digitStart = 0x30;
        private const byte digitEnd = 0x39;
        private const byte hexadecimalDigitStart = 0x41;
        private const byte hexadecimalDigitEnd = 70;
        private const byte octalDigitEnd = 0x37;
        private const byte lowercaseHexadecimalDigitStart = 0x61;
        private const byte lowercaseHexadecimalDigitEnd = 0x66;
        private const byte numberSign = 0x23;
        private const byte escape = 0x5c;
        private const byte horizontalTab = 9;
        private const byte endString = 0x29;
        protected const byte Minus = 0x2d;
        protected const byte StartString = 40;
        protected const byte StartArray = 0x5b;
        protected const byte EndArray = 0x5d;
        protected const byte CarriageReturn = 13;
        protected const byte LineFeed = 10;
        protected const byte Space = 0x20;
        protected const byte Comment = 0x25;
        protected const byte NameIdentifier = 0x2f;
        private static readonly byte[] nullToken = new byte[] { 110, 0x75, 0x6c, 0x6c };
        private static readonly byte[] trueToken = new byte[] { 0x74, 0x72, 0x75, 0x65 };
        private static readonly byte[] falseToken = new byte[] { 0x66, 0x61, 0x6c, 0x73, 0x65 };
        private static readonly PdfTokenDescription startxrefToken;
        private readonly PdfDataStream stream;
        private byte current;

        static PdfObjectParser()
        {
            byte[] token = new byte[] { 0x73, 0x74, 0x61, 0x72, 0x74, 120, 0x72, 0x65, 0x66 };
            startxrefToken = new PdfTokenDescription(token);
        }

        protected PdfObjectParser(PdfDataStream stream, long position)
        {
            this.stream = stream;
            this.CurrentPosition = position;
        }

        protected virtual bool CanReadObject()
        {
            if (!this.stream.CanRead)
            {
                return false;
            }
            while (this.IsSpace)
            {
                if (!this.ReadNext())
                {
                    return false;
                }
            }
            return true;
        }

        public static byte ConvertToDigit(byte symbol)
        {
            if ((symbol < 0x30) || (symbol > 0x39))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (byte) (symbol - 0x30);
        }

        public static byte ConvertToHexadecimalDigit(byte symbol)
        {
            if ((symbol >= 0x30) && (symbol <= 0x39))
            {
                return (byte) (symbol - 0x30);
            }
            if ((symbol >= 0x41) && (symbol <= 70))
            {
                return (byte) ((symbol - 0x41) + 10);
            }
            if ((symbol < 0x61) || (symbol > 0x66))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (byte) ((symbol - 0x61) + 10);
        }

        protected virtual byte[] DecryptString(List<byte> list) => 
            list.ToArray();

        public static bool IsDigitSymbol(byte symbol) => 
            (symbol >= 0x30) && (symbol <= 0x39);

        public static bool IsHexadecimalDigitSymbol(byte symbol) => 
            ((symbol < 0x30) || (symbol > 0x39)) ? (((symbol >= 0x41) && (symbol <= 70)) || ((symbol >= 0x61) && (symbol <= 0x66))) : true;

        protected static bool IsOctalDigitSymbol(byte symbol) => 
            (symbol >= 0x30) && (symbol <= 0x37);

        public static bool IsSpaceSymbol(byte symbol) => 
            (symbol == 0x20) || ((symbol == 10) || ((symbol == 13) || ((symbol == 0) || ((symbol == 9) || (symbol == 12)))));

        public static string[] ParseNameArray(byte[] data) => 
            new PdfObjectParser(new PdfArrayDataStream(data), 0L).ReadNameArray();

        public static int? ParseStartXRef(byte[] data) => 
            new PdfObjectParser(new PdfArrayDataStream(data), 0L).ReadStartXRef();

        protected virtual object ReadAlphabeticalObject(bool isHexadecimalStringSeparatedUsingWhiteSpaces, bool isIndirect) => 
            this.ReadToken();

        private IList<object> ReadArray()
        {
            if ((this.current != 0x5b) || !this.ReadNext())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            List<object> list = new List<object>();
            while (this.SkipSpaces())
            {
                if (this.current == 0x5d)
                {
                    this.ReadNext();
                    return list;
                }
                list.Add(this.ReadObject(false, false));
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return list;
        }

        public int ReadInteger()
        {
            object obj2 = this.ReadObject(false, false);
            if (!(obj2 is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (int) obj2;
        }

        protected PdfName ReadName()
        {
            if ((this.current != 0x2f) || !this.ReadNext())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            StringBuilder builder = new StringBuilder();
            while (this.CanContinueReading)
            {
                if (this.current == 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                else if (this.current != 0x23)
                {
                    builder.Append((char) this.current);
                }
                else
                {
                    if (!this.ReadNext())
                    {
                        builder.Append('#');
                        break;
                    }
                    if (!this.IsHexadecimalDigit)
                    {
                        builder.Append('#');
                        continue;
                    }
                    byte current = this.current;
                    byte num2 = (byte) (this.HexadecimalDigit * 0x10);
                    if (!this.ReadNext())
                    {
                        builder.Append('#');
                        builder.Append((char) current);
                        break;
                    }
                    if (!this.IsHexadecimalDigit)
                    {
                        builder.Append('#');
                        builder.Append((char) current);
                        continue;
                    }
                    builder.Append((char) (num2 + this.HexadecimalDigit));
                }
                if (!this.ReadNext())
                {
                    break;
                }
            }
            return new PdfName(builder.ToString());
        }

        private string[] ReadNameArray()
        {
            List<string> list = new List<string>();
            while (this.SkipSpaces())
            {
                list.Add(this.ReadName().Name);
            }
            return list.ToArray();
        }

        protected bool ReadNext()
        {
            this.stream.Position += 1L;
            int currentByte = this.stream.CurrentByte;
            if (currentByte < 0)
            {
                return false;
            }
            this.current = (byte) currentByte;
            return true;
        }

        protected object ReadNumber()
        {
            bool flag;
            byte current = this.current;
            if (current == 0x2b)
            {
                if (!this.ReadNext())
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                flag = false;
            }
            else if (current != 0x2d)
            {
                flag = false;
            }
            else
            {
                if (!this.ReadNext() || (!this.IsDigit && (!this.IsPeriod && ((this.current != 0x2d) || !this.ReadNext()))))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                flag = true;
            }
            int initialValue = 0;
            int num2 = 0;
            bool flag2 = false;
            while (true)
            {
                if (this.IsDigit)
                {
                    num2 = initialValue;
                    initialValue = (initialValue * 10) + this.Digit;
                    flag2 = initialValue < 0;
                    if (!flag2 && this.ReadNext())
                    {
                        continue;
                    }
                }
                if (flag2)
                {
                    initialValue = num2;
                }
                return (!(this.IsPeriod | flag2) ? (flag ? -initialValue : initialValue) : (flag ? -this.ReadReal(initialValue) : this.ReadReal(initialValue)));
            }
        }

        protected virtual object ReadNumericObject() => 
            this.ReadNumber();

        public object ReadObject(bool isHexadecimalStringSeparatedUsingWhiteSpaces, bool isIndirect)
        {
            if (!this.CanReadObject())
            {
                return null;
            }
            if (this.IsDigit || (this.current == 0x2d))
            {
                object obj2 = this.ReadNumericObject();
                if (obj2 != null)
                {
                    return obj2;
                }
            }
            else
            {
                byte current = this.current;
                if (current > 0x2f)
                {
                    if (current <= 0x66)
                    {
                        if (current == 0x5b)
                        {
                            return this.ReadArray();
                        }
                        if ((current == 0x66) && this.ReadToken(falseToken))
                        {
                            return false;
                        }
                    }
                    else if (current != 110)
                    {
                        if ((current == 0x74) && this.ReadToken(trueToken))
                        {
                            return true;
                        }
                    }
                    else if (this.ReadToken(nullToken))
                    {
                        return null;
                    }
                }
                else
                {
                    if (current == 0x25)
                    {
                        StringBuilder builder = new StringBuilder();
                        while (this.ReadNext() && ((this.current != 13) && (this.current != 10)))
                        {
                            builder.Append((char) this.current);
                        }
                        return new PdfComment(builder.ToString());
                    }
                    if (current == 40)
                    {
                        return this.ReadString();
                    }
                    switch (current)
                    {
                        case 0x2b:
                        case 0x2e:
                            return this.ReadNumber();

                        case 0x2f:
                            return this.ReadName();

                        default:
                            break;
                    }
                }
            }
            return this.ReadAlphabeticalObject(isHexadecimalStringSeparatedUsingWhiteSpaces, isIndirect);
        }

        protected bool ReadPrev()
        {
            this.stream.Position -= 1L;
            int currentByte = this.stream.CurrentByte;
            if (currentByte < 0)
            {
                return false;
            }
            this.current = (byte) currentByte;
            return true;
        }

        private double ReadReal(int initialValue)
        {
            double num = initialValue;
            while (true)
            {
                if (this.IsDigit)
                {
                    num = (num * 10.0) + this.Digit;
                    if (this.ReadNext())
                    {
                        continue;
                    }
                }
                if (this.IsPeriod && this.ReadNext())
                {
                    double num2 = 0.0;
                    double num3 = 10.0;
                    while (true)
                    {
                        if (this.IsDigit)
                        {
                            num2 += ((double) this.Digit) / num3;
                            num3 *= 10.0;
                            if (this.ReadNext())
                            {
                                continue;
                            }
                        }
                        num += num2;
                        if (this.current != 0x2d)
                        {
                            break;
                        }
                        while (this.ReadNext() && (this.IsDigit || (this.IsPeriod || ((this.current == 0x2d) || (this.current == 0x2b)))))
                        {
                        }
                        return num;
                    }
                }
                if (this.current == 0x45)
                {
                    if (!this.ReadNext())
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    object obj2 = this.ReadNumber();
                    if (!(obj2 is int))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    num *= Math.Pow(10.0, (double) ((int) obj2));
                }
                if ((num > 3.4028234663852886E+38) || (num < -3.4028234663852886E+38))
                {
                    num = 0.0;
                }
                return num;
            }
        }

        private int? ReadStartXRef()
        {
            PdfTokenDescription description = PdfTokenDescription.BeginCompare(startxrefToken);
            while (!description.Compare(this.current))
            {
                if (!this.ReadNext())
                {
                    return null;
                }
            }
            if (this.ReadNext() && this.SkipSpaces())
            {
                object obj2 = this.ReadNumber();
                if (obj2 is int)
                {
                    return (int?) obj2;
                }
            }
            return null;
        }

        private byte[] ReadString()
        {
            byte digit;
            if (this.current != 40)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num = 1;
            List<byte> list = new List<byte>();
            goto TR_002A;
        TR_0001:
            list.Add(this.current);
        TR_002A:
            while (true)
            {
                if (!this.ReadNext())
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return null;
                }
                byte current = this.current;
                if (current > 40)
                {
                    if (current == 0x29)
                    {
                        if (--num == 0)
                        {
                            this.ReadNext();
                            return this.DecryptString(list);
                        }
                    }
                    else if (current == 0x5c)
                    {
                        if (!this.ReadNext())
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        if (IsOctalDigitSymbol(this.current))
                        {
                            digit = this.Digit;
                            for (int i = 0; i < 2; i++)
                            {
                                if (!this.ReadNext())
                                {
                                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                                }
                                if (!IsOctalDigitSymbol(this.current))
                                {
                                    this.ReadPrev();
                                    break;
                                }
                                digit = (byte) ((digit * 8) + this.Digit);
                            }
                            break;
                        }
                        current = this.current;
                        if (current <= 0x62)
                        {
                            if (current == 10)
                            {
                                continue;
                            }
                            if (current == 13)
                            {
                                this.SkipLineFeed();
                                continue;
                            }
                            if (current == 0x62)
                            {
                                list.Add(8);
                                continue;
                            }
                        }
                        else if (current <= 110)
                        {
                            if (current == 0x66)
                            {
                                list.Add(12);
                                continue;
                            }
                            if (current == 110)
                            {
                                list.Add(10);
                                continue;
                            }
                        }
                        else
                        {
                            if (current == 0x72)
                            {
                                list.Add(13);
                                continue;
                            }
                            if (current == 0x74)
                            {
                                list.Add(9);
                                continue;
                            }
                        }
                        list.Add(this.current);
                        continue;
                    }
                    goto TR_0001;
                }
                else
                {
                    if (current == 13)
                    {
                        list.Add(10);
                        this.SkipLineFeed();
                        continue;
                    }
                    if (current == 40)
                    {
                        num++;
                    }
                    goto TR_0001;
                }
                break;
            }
            list.Add(digit);
            goto TR_002A;
        }

        protected string ReadToken()
        {
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                if (this.CanContinueReading)
                {
                    builder.Append((char) this.current);
                    if (this.ReadNext())
                    {
                        continue;
                    }
                }
                return builder.ToString();
            }
        }

        protected bool ReadToken(byte[] token)
        {
            if (!this.SkipSpaces())
            {
                return false;
            }
            long currentPosition = this.CurrentPosition;
            bool flag = false;
            foreach (byte num3 in token)
            {
                if (flag || (this.current != num3))
                {
                    this.CurrentPosition = currentPosition;
                    return false;
                }
                if (!this.ReadNext())
                {
                    flag = true;
                }
            }
            return true;
        }

        private void SkipLineFeed()
        {
            if (!this.ReadNext())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if (this.current != 10)
            {
                this.ReadPrev();
            }
        }

        protected bool SkipSpaces()
        {
            if (!this.stream.CanRead)
            {
                return false;
            }
            while (true)
            {
                if (this.IsSpace)
                {
                    if (!this.ReadNext())
                    {
                        return false;
                    }
                    continue;
                }
                if (this.current != 0x25)
                {
                    return true;
                }
                do
                {
                    if (!this.ReadNext())
                    {
                        return false;
                    }
                }
                while ((this.current != 13) && (this.current != 10));
            }
        }

        public long CurrentPosition
        {
            get => 
                this.stream.Position;
            set
            {
                this.stream.Position = value;
                this.current = (byte) this.stream.CurrentByte;
            }
        }

        private byte Digit =>
            ConvertToDigit(this.current);

        private bool IsHexadecimalDigit =>
            IsHexadecimalDigitSymbol(this.current);

        private bool IsPeriod =>
            (this.current == 0x2e) || (this.current == 0x2c);

        protected PdfDataStream Stream =>
            this.stream;

        protected byte Current =>
            this.current;

        protected bool IsSpace =>
            IsSpaceSymbol(this.current);

        protected bool IsDigit =>
            IsDigitSymbol(this.current);

        protected byte HexadecimalDigit =>
            ConvertToHexadecimalDigit(this.current);

        protected virtual bool CanContinueReading =>
            !this.IsSpace && ((this.current != 0x25) && ((this.current != 0x2f) && ((this.current != 40) && ((this.current != 0x29) && ((this.current != 0x5b) && (this.current != 0x5d))))));

        protected virtual bool IgnoreIncorrectSymbolsInNames =>
            false;
    }
}

