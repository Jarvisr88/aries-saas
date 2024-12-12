namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public class PdfDocumentStream
    {
        private static readonly byte[] unicodeStringHeader = new byte[] { 0xfe, 0xff };
        private const string doubleMask = "0.################";
        private const string unicodeStringStart = "FEFF";
        private const byte zero = 0x30;
        private static readonly Encoding utf8encoding = Encoding.UTF8;
        private static readonly Encoding unicodeEncoding = Encoding.BigEndianUnicode;
        private static readonly CultureInfo invariantCulture = CultureInfo.InvariantCulture;
        private static readonly PdfTokenDescription checkObjToken;
        private static readonly PdfTokenDescription endobjToken;
        private static readonly PdfTokenDescription objToken;
        private readonly PdfStreamWriter stream;
        private readonly long startPosition;
        private readonly bool canRead;
        private PdfEncryptionInfo encryptionInfo;

        static PdfDocumentStream()
        {
            byte[] token = new byte[] { 0x20, 0x6f, 0x62, 0x6a };
            checkObjToken = new PdfTokenDescription(token);
            byte[] buffer3 = new byte[] { 0x65, 110, 100, 0x6f, 0x62, 0x6a };
            endobjToken = new PdfTokenDescription(buffer3);
            byte[] buffer4 = new byte[] { 0x6f, 0x62, 0x6a };
            objToken = new PdfTokenDescription(buffer4);
        }

        private PdfDocumentStream(System.IO.Stream stream, bool canRead, long startPosition)
        {
            this.stream = new PdfStreamWriter(stream);
            this.canRead = canRead;
            this.startPosition = startPosition;
        }

        public static byte[] ConvertToArray(string str)
        {
            if (str == null)
            {
                return null;
            }
            int length = str.Length;
            byte[] buffer = new byte[length];
            for (int i = 0; i < length; i++)
            {
                buffer[i] = (byte) str[i];
            }
            return buffer;
        }

        public void CopyFrom(System.IO.Stream sourceStream, int length)
        {
            byte[] buffer = new byte[0x1000];
            for (int i = length; i > 0; i -= 0x1000)
            {
                int count = Math.Min(0x1000, i);
                if (count != sourceStream.Read(buffer, 0, count))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                sourceStream.Position += count;
                this.stream.Write(buffer, 0, count);
            }
        }

        public static PdfDocumentStream CreateStreamForMerging(System.IO.Stream stream) => 
            new PdfDocumentStream(stream, false, 0L);

        public static PdfDocumentStream CreateStreamForReading(System.IO.Stream stream)
        {
            long startPosition = 0L;
            if (stream.CanSeek)
            {
                startPosition = stream.Position;
            }
            return new PdfDocumentStream(stream, stream.CanRead, startPosition);
        }

        public static PdfDocumentStream CreateStreamForWriting(System.IO.Stream stream) => 
            new PdfDocumentStream(stream, stream.CanRead, stream.CanSeek ? stream.Position : 0L);

        public long FindLastToken(PdfTokenDescription description) => 
            this.FindLastToken(description, true);

        public long FindLastToken(PdfTokenDescription description, bool mustBe)
        {
            long position = -1L;
            while (this.FindToken(description))
            {
                position = this.Position;
            }
            if ((position == -1L) & mustBe)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return position;
        }

        public bool FindToken(PdfTokenDescription description)
        {
            PdfTokenDescription description2 = PdfTokenDescription.BeginCompare(description);
            while (true)
            {
                int num = this.Stream.ReadByte();
                if (num == -1)
                {
                    return false;
                }
                if (description2.Compare((byte) num))
                {
                    return true;
                }
            }
        }

        internal void Flush()
        {
            this.Stream.Flush();
        }

        public void Patch(long offset, object objectToWrite)
        {
            long position = this.Position;
            this.Position = offset;
            this.WriteObject(objectToWrite, -1);
            this.Position = position;
        }

        public PdfIndirectObject ReadArrayBasedIndirectObject(long offset)
        {
            int number = this.ReadIndirectObjectNumber(offset);
            int generation = this.ReadNumber();
            if (!this.ReadToken(objToken))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            List<byte> list = new List<byte>();
            PdfTokenDescription description = PdfTokenDescription.BeginCompare(checkObjToken);
            PdfTokenDescription description2 = PdfTokenDescription.BeginCompare(endobjToken);
            bool flag = false;
            while (true)
            {
                int num3 = this.Stream.ReadByte();
                if (num3 == -1)
                {
                    break;
                }
                byte item = (byte) num3;
                list.Add(item);
                if (description2.Compare(item))
                {
                    if (!flag)
                    {
                        int length = description2.Length;
                        list.RemoveRange(list.Count - length, length);
                        break;
                    }
                    description2 = PdfTokenDescription.BeginCompare(endobjToken);
                    flag = false;
                }
                if (description.Compare(item))
                {
                    if (PdfObjectParser.IsDigitSymbol(list[(list.Count - checkObjToken.Length) - 1]))
                    {
                        flag = true;
                    }
                    description = PdfTokenDescription.BeginCompare(checkObjToken);
                }
            }
            return new PdfIndirectObject(number, generation, offset, new PdfArrayDataStream(list.ToArray()));
        }

        public int ReadByte() => 
            this.Stream.ReadByte();

        public byte[] ReadBytes(int count)
        {
            byte[] buffer = new byte[count];
            this.Stream.Read(buffer, 0, count);
            return buffer;
        }

        public int ReadBytes(byte[] buffer) => 
            this.Stream.Read(buffer, (int) this.startPosition, buffer.Length);

        public int ReadIndirectObjectNumber(long offset)
        {
            this.Stream.Position = offset + this.startPosition;
            return this.ReadNumber();
        }

        public int ReadNumber()
        {
            int num = this.SkipSpaces();
            if (num == -1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            bool flag = false;
            int num2 = PdfObjectParser.ConvertToDigit((byte) num);
            while (true)
            {
                num = this.Stream.ReadByte();
                if (num == -1)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                byte symbol = (byte) num;
                if (PdfObjectParser.IsSpaceSymbol(symbol))
                {
                    return num2;
                }
                if (symbol == 0x2e)
                {
                    flag = true;
                }
                else if (!flag)
                {
                    num2 = (num2 * 10) + PdfObjectParser.ConvertToDigit(symbol);
                }
                else if (symbol != 0x30)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
        }

        public long ReadNumber(int digitsCount)
        {
            int num = this.SkipSpaces();
            if (num == -1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num2 = PdfObjectParser.ConvertToDigit((byte) num);
            for (int i = 1; i < digitsCount; i++)
            {
                num = this.Stream.ReadByte();
                if (num == -1)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                num2 = (num2 * 10) + PdfObjectParser.ConvertToDigit((byte) num);
            }
            return (long) num2;
        }

        public PdfObjectSlot ReadObject(long offset)
        {
            this.Position = offset;
            int number = this.ReadNumber();
            int generation = this.ReadNumber();
            if (!this.ReadToken(objToken) || !this.FindToken(endobjToken))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.SkipEmptySpaces();
            return new PdfObjectSlot(number, generation, offset);
        }

        public PdfIndirectObject ReadStreamBasedIndirectObject(long offset)
        {
            int number = this.ReadIndirectObjectNumber(offset);
            int generation = this.ReadNumber();
            if (!this.ReadToken(objToken))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return new PdfIndirectObject(number, generation, offset, new PdfStreamingDataStream(this.Stream, this.Length));
        }

        public string ReadString() => 
            ReadString(this.Stream);

        public static string ReadString(System.IO.Stream stream)
        {
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                int num = stream.ReadByte();
                if ((num == -1) || ((num == 10) || (num == 13)))
                {
                    return builder.ToString();
                }
                builder.Append((char) num);
            }
        }

        public bool ReadToken(PdfTokenDescription description)
        {
            PdfTokenDescription description2 = PdfTokenDescription.BeginCompare(description);
            int num = description2.IsStartWithComment ? this.Stream.ReadByte() : this.SkipSpaces();
            if (num != -1)
            {
                description2.Compare((byte) num);
                int length = description2.Length;
                for (int i = 1; i < length; i++)
                {
                    num = this.Stream.ReadByte();
                    if (num == -1)
                    {
                        return false;
                    }
                    if (description2.Compare((byte) num))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Reset()
        {
            this.Stream.SetLength(1L);
        }

        public long SetPositionFromEnd(int value)
        {
            long length = this.Length;
            long num2 = (length > value) ? (length - value) : length;
            this.Position = num2;
            return num2;
        }

        public void SkipEmptySpaces()
        {
            int num = this.ReadByte();
            while (true)
            {
                if (num < 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                else if (!PdfObjectParser.IsSpaceSymbol((byte) num))
                {
                    System.IO.Stream stream = this.Stream;
                    stream.Position -= 1L;
                    return;
                }
                num = this.ReadByte();
            }
        }

        public int SkipSpaces()
        {
            while (true)
            {
                int num = this.Stream.ReadByte();
                if (num == -1)
                {
                    return -1;
                }
                byte symbol = (byte) num;
                if (symbol == 0x25)
                {
                    this.ReadString();
                    continue;
                }
                if (!PdfObjectParser.IsSpaceSymbol(symbol))
                {
                    return num;
                }
            }
        }

        public void WriteByte(byte b)
        {
            this.stream.WriteByte(b);
        }

        public void WriteBytes(byte[] value)
        {
            this.stream.Write(value);
        }

        public void WriteCloseBracket()
        {
            this.WriteByte(0x5d);
        }

        public void WriteCloseDictionary()
        {
            byte[] data = new byte[] { 0x3e, 0x3e, 13, 10 };
            this.stream.Write(data);
        }

        public void WriteDouble(double value)
        {
            this.stream.WriteDouble(value, false);
        }

        public void WriteHexadecimalString(byte[] data, int number)
        {
            if ((this.encryptionInfo != null) && (number != -1))
            {
                data = this.encryptionInfo.EncryptData(data, number);
            }
            this.stream.WriteHexadecimalString(data);
        }

        public void WriteInt(int value)
        {
            this.stream.WriteInt(value);
        }

        public void WriteName(PdfName name)
        {
            if (name == null)
            {
                this.WriteString("null");
            }
            else
            {
                name.Write(this);
            }
        }

        public void WriteObject(object value, int number)
        {
            IPdfWritableObject obj2 = value as IPdfWritableObject;
            if (obj2 != null)
            {
                obj2.Write(this, number);
            }
            else
            {
                PdfRectangle rectangle = value as PdfRectangle;
                if (rectangle != null)
                {
                    this.WriteObject(rectangle.ToWritableObject(), number);
                }
                else if (value is int)
                {
                    this.WriteInt((int) value);
                }
                else if (value is double)
                {
                    this.WriteDouble((double) value);
                }
                else
                {
                    string s = value as string;
                    if (s != null)
                    {
                        byte[] bytes = unicodeEncoding.GetBytes(s);
                        this.WriteUnicodeHexadecimalString(bytes, number);
                    }
                    else
                    {
                        byte[] data = value as byte[];
                        if (data != null)
                        {
                            this.WriteHexadecimalString(data, number);
                        }
                        else
                        {
                            IEnumerable enumerable = value as IEnumerable;
                            if (enumerable != null)
                            {
                                new PdfWritableArray(enumerable).Write(this, number);
                            }
                            else if (!(value is DateTimeOffset))
                            {
                                if (value as bool)
                                {
                                    this.WriteString(((bool) value) ? "true" : "false");
                                }
                                else
                                {
                                    if (value != null)
                                    {
                                        throw new NotSupportedException();
                                    }
                                    this.WriteString("null");
                                }
                            }
                            else
                            {
                                string str2;
                                DateTimeOffset offset = (DateTimeOffset) value;
                                int num = Convert.ToInt32(offset.Offset.TotalMinutes);
                                if (num == 0)
                                {
                                    str2 = $"D:{offset:yyyyMMddHHmmss}Z";
                                }
                                else
                                {
                                    bool flag = num > 0;
                                    if (!flag)
                                    {
                                        num = -num;
                                    }
                                    object[] objArray1 = new object[4];
                                    objArray1[0] = offset;
                                    objArray1[1] = flag ? '+' : '-';
                                    object[] args = objArray1;
                                    args[2] = num / 60;
                                    args[3] = num % 60;
                                    str2 = string.Format("D:{0:yyyyMMddHHmmss}{1}{2:00}'{3:00}'", args);
                                }
                                this.WriteHexadecimalString(utf8encoding.GetBytes(str2), number);
                            }
                        }
                    }
                }
            }
        }

        public PdfObjectPointer WriteObjectContainer(PdfObjectContainer container)
        {
            PdfObjectPointer pointer = null;
            if (container != null)
            {
                long offset = this.SetPositionFromEnd(0);
                int objectNumber = container.ObjectNumber;
                int objectGeneration = container.ObjectGeneration;
                object[] args = new object[] { objectNumber };
                this.WriteStringFormat("{0} 0 obj\r\n", args);
                if (this.canRead)
                {
                    pointer = new PdfObjectSlot(objectNumber, 0, offset);
                    this.WriteObject(container.Value, objectNumber);
                }
                else
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PdfDocumentStream stream2 = CreateStreamForWriting(stream);
                        stream2.EncryptionInfo = this.encryptionInfo;
                        stream2.WriteObject(container.Value, objectNumber);
                        byte[] data = stream.ToArray();
                        pointer = new PdfIndirectObject(objectNumber, 0, offset, new PdfArrayDataStream(data));
                        this.WriteBytes(data);
                    }
                }
                this.WriteString("\r\nendobj\r\n");
            }
            return pointer;
        }

        public void WriteOpenBracket()
        {
            this.WriteByte(0x5b);
        }

        public void WriteOpenDictionary()
        {
            byte[] data = new byte[] { 60, 60 };
            this.stream.Write(data);
        }

        public void WriteSpace()
        {
            this.WriteByte(0x20);
        }

        public void WriteString(string s)
        {
            this.stream.WriteString(s);
        }

        public void WriteStringFormat(string format, params object[] args)
        {
            this.WriteString(string.Format(invariantCulture, format, args));
        }

        private void WriteUnicodeHexadecimalString(byte[] data, int number)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(unicodeStringHeader, 0, 2);
                stream.Write(data, 0, data.Length);
                this.WriteHexadecimalString(stream.ToArray(), number);
            }
        }

        public System.IO.Stream Stream =>
            this.stream.Stream;

        public PdfEncryptionInfo EncryptionInfo
        {
            get => 
                this.encryptionInfo;
            set => 
                this.encryptionInfo = value;
        }

        public long Length =>
            this.Stream.Length - this.startPosition;

        public long Position
        {
            get => 
                this.Stream.Position - this.startPosition;
            set
            {
                long num = value + this.startPosition;
                if (this.Stream.Position != num)
                {
                    this.Stream.Position = num;
                }
            }
        }
    }
}

