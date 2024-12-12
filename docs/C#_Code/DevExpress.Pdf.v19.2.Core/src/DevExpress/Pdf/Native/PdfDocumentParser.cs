namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PdfDocumentParser : PdfObjectParser
    {
        protected const byte StartObject = 60;
        protected const byte EndObject = 0x3e;
        private static readonly byte[] streamToken = new byte[] { 0x73, 0x74, 0x72, 0x65, 0x61, 0x6d };
        private static readonly byte[] endstreamToken = new byte[] { 0x65, 110, 100, 0x73, 0x74, 0x72, 0x65, 0x61, 0x6d };
        private static readonly byte[] endobjToken = new byte[] { 0x65, 110, 100, 0x6f, 0x62, 0x6a };
        private readonly PdfObjectCollection objects;
        private readonly int number;
        private readonly int generation;
        private string currentlyReadingDictionaryKey;

        public PdfDocumentParser(PdfObjectCollection objects, int number, int generation, PdfDataStream stream) : this(objects, number, generation, stream, 0)
        {
        }

        public PdfDocumentParser(PdfObjectCollection objects, int number, int generation, PdfDataStream stream, int position) : base(stream, (long) position)
        {
            this.objects = objects;
            this.number = number;
            this.generation = generation;
            base.SkipSpaces();
        }

        protected override bool CanReadObject() => 
            base.SkipSpaces();

        protected virtual bool CheckDictionaryAlphabeticalToken(string token) => 
            false;

        protected virtual PdfReaderStream CreateStream(PdfReaderDictionary dictionary, byte[] data) => 
            new PdfReaderStream(dictionary, data, null);

        private int FindTokenOffset(byte[] token, IList<byte> buffer, int offset, int limit)
        {
            int index = 0;
            for (int i = offset; i < limit; i++)
            {
                byte num3 = buffer[i];
                if (num3 != token[index])
                {
                    if (num3 == token[0])
                    {
                        index = 1;
                    }
                }
                else
                {
                    index++;
                    if (index == token.Length)
                    {
                        return (i + 1);
                    }
                }
            }
            return -1;
        }

        public static PdfReaderDictionary ParseDictionary(PdfObjectCollection objects, int number, int generation, PdfDataStream stream) => 
            new PdfDocumentParser(objects, number, generation, stream).ReadDictionary(true);

        public static object ParseObject(PdfObjectCollection objects, int number, int generation, PdfDataStream stream, int position) => 
            new PdfDocumentParser(objects, number, generation, stream, position).ReadObject(false, true);

        public static PdfReaderStream ParseStream(PdfObjectCollection objects, int number, int generation, PdfDataStream stream) => 
            new PdfDocumentParser(objects, number, generation, stream).ReadStream();

        protected override object ReadAlphabeticalObject(bool isHexadecimalStringSeparatedUsingWhiteSpaces, bool isIndirect)
        {
            object obj2 = this.ReadDictionaryOrStream(isHexadecimalStringSeparatedUsingWhiteSpaces, isIndirect);
            if (obj2 == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return obj2;
        }

        protected byte[] ReadData(byte[] token, bool ignoreWhiteSpace, byte[] endOfDataToken = null)
        {
            int length = token.Length;
            int count = length + 1;
            int index = 0;
            List<byte> list = new List<byte>();
            int num4 = -count;
            goto TR_0015;
        TR_0000:
            count++;
            list.RemoveRange(list.Count - count, count);
            return list.ToArray();
        TR_0002:
            num4++;
            goto TR_0015;
        TR_0005:
            if (base.ReadNext())
            {
                goto TR_0002;
            }
            else if (index != length)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
                goto TR_0002;
            }
            else
            {
                list.Add(base.Current);
            }
            goto TR_0000;
        TR_000A:
            index = 0;
            goto TR_0005;
        TR_0015:
            while (true)
            {
                byte current = base.Current;
                list.Add(current);
                if ((index >= length) || (current != token[index]))
                {
                    if (!base.IsSpace || (index != length))
                    {
                        goto TR_000A;
                    }
                    else if (!ignoreWhiteSpace)
                    {
                        byte[] buffer = new byte[10];
                        if (this.FindTokenOffset(token, buffer, 0, base.Stream.Read(buffer, 0, 10)) == -1)
                        {
                            if (endOfDataToken != null)
                            {
                                int offset = Math.Max(0, list.Count - 15);
                                offset = this.FindTokenOffset(endOfDataToken, list, offset, Math.Min(list.Count, offset + 15));
                                if (offset != -1)
                                {
                                    if (offset < list.Count)
                                    {
                                        list.RemoveRange(offset, list.Count - offset);
                                    }
                                    return list.ToArray();
                                }
                            }
                            if (this.TryReadKnownObject())
                            {
                                goto TR_0000;
                            }
                        }
                        goto TR_000A;
                    }
                    goto TR_0000;
                }
                else
                {
                    index++;
                }
                break;
            }
            goto TR_0005;
        }

        protected byte[] ReadData(int length, byte[] token, byte[] alternativeToken, bool ignoreWhiteSpace)
        {
            if (!base.ReadNext())
            {
                if (length == 0)
                {
                    return new byte[0];
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            long currentPosition = base.CurrentPosition;
            byte[] buffer = new byte[length];
            try
            {
                if (length > 0)
                {
                    if (base.Stream.Read(buffer, 0, length) != length)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    base.CurrentPosition = currentPosition + length;
                }
                long num2 = base.CurrentPosition;
                if (!base.ReadToken(token) && ((alternativeToken == null) || !base.ReadToken(alternativeToken)))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return buffer;
            }
            catch
            {
                base.CurrentPosition = currentPosition;
                return this.ReadData(token, ignoreWhiteSpace, null);
            }
        }

        private PdfReaderDictionary ReadDictionary(bool isIndirect)
        {
            PdfReaderDictionary dictionary2;
            if ((base.Current != 60) || (!base.ReadNext() || ((base.Current != 60) || !base.ReadNext())))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfReaderDictionary dictionary = new PdfReaderDictionary(this.objects, isIndirect ? this.number : -1, this.generation);
            while (true)
            {
                if (!base.SkipSpaces())
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return dictionary;
                }
                byte current = base.Current;
                if (current != 0x2f)
                {
                    if (current == 0x3e)
                    {
                        if (!base.ReadNext() && (base.Current != 0x3e))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        base.ReadNext();
                        return dictionary;
                    }
                    long currentPosition = base.CurrentPosition;
                    if (!this.CheckDictionaryAlphabeticalToken(base.ReadToken()))
                    {
                        base.CurrentPosition = currentPosition;
                        return dictionary;
                    }
                    continue;
                }
                this.currentlyReadingDictionaryKey = base.ReadName().Name;
                if (!base.SkipSpaces())
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                try
                {
                    dictionary[this.currentlyReadingDictionaryKey] = base.ReadObject(this.currentlyReadingDictionaryKey == "Panose", false);
                }
                catch
                {
                    dictionary2 = dictionary;
                    break;
                }
                finally
                {
                    this.currentlyReadingDictionaryKey = null;
                }
            }
            return dictionary2;
        }

        protected object ReadDictionaryOrStream(bool isHexadecimalStringSeparatedUsingWhiteSpaces, bool isIndirect)
        {
            if ((base.Current == 60) && base.ReadNext())
            {
                bool flag = base.Current == 60;
                base.ReadPrev();
                if (flag)
                {
                    PdfReaderDictionary dictionary = this.ReadDictionary(isIndirect);
                    return (base.ReadToken(streamToken) ? ((object) this.ReadStream(dictionary)) : ((object) dictionary));
                }
                if ((base.Current != 60) || !base.ReadNext())
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                List<byte> list = new List<byte>();
                while (base.SkipSpaces())
                {
                    if (base.Current == 0x3e)
                    {
                        base.ReadNext();
                        return this.DecryptString(list);
                    }
                    byte hexadecimalDigit = base.HexadecimalDigit;
                    if (!base.ReadNext())
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    if (!isHexadecimalStringSeparatedUsingWhiteSpaces)
                    {
                        if (!base.SkipSpaces())
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        hexadecimalDigit = (byte) (hexadecimalDigit * 0x10);
                    }
                    else
                    {
                        if (base.IsSpace)
                        {
                            list.Add(hexadecimalDigit);
                            if (!base.ReadNext())
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            continue;
                        }
                        hexadecimalDigit = (byte) (hexadecimalDigit * 0x10);
                    }
                    if (base.Current == 0x3e)
                    {
                        list.Add(hexadecimalDigit);
                    }
                    else
                    {
                        list.Add((byte) (hexadecimalDigit + base.HexadecimalDigit));
                        if (!base.ReadNext())
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return null;
        }

        protected override object ReadNumericObject()
        {
            object obj2 = base.ReadNumber();
            if (!(obj2 is int))
            {
                return obj2;
            }
            int number = (int) obj2;
            if (base.SkipSpaces() && base.IsDigit)
            {
                long currentPosition = base.CurrentPosition;
                obj2 = base.ReadNumber();
                if ((obj2 is int) && (base.SkipSpaces() && ((base.Current == 0x52) && (!base.ReadNext() || !this.CanContinueReading))))
                {
                    return new PdfObjectReference(number, (int) obj2);
                }
                base.CurrentPosition = currentPosition;
            }
            return number;
        }

        private PdfReaderStream ReadStream()
        {
            PdfReaderDictionary dictionary = this.ReadDictionary(true);
            if (!base.ReadToken(streamToken))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return this.ReadStream(dictionary);
        }

        private PdfReaderStream ReadStream(PdfReaderDictionary dictionary)
        {
            this.SkipStreamLeadingWhiteSpace();
            int? integer = dictionary.GetInteger("Length");
            if (integer == null)
            {
                if ((base.Current == 10) && !base.ReadNext())
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return this.CreateStream(dictionary, this.ReadData(endstreamToken, true, null));
            }
            int length = integer.Value;
            if (length < 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            base.Stream.Synchronize();
            return this.CreateStream(dictionary, this.ReadData(length, endstreamToken, endobjToken, true));
        }

        private void SetPositionToStreamDataFirstByte()
        {
            this.ReadDictionary(true);
            if (!base.ReadToken(streamToken))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.SkipStreamLeadingWhiteSpace();
            if (!base.ReadNext())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        public static void SetPositionToStreamDataFirstByte(PdfDataStream stream)
        {
            new PdfDocumentParser(null, -1, 0, stream).SetPositionToStreamDataFirstByte();
        }

        private void SkipStreamLeadingWhiteSpace()
        {
            while (base.Current == 0x20)
            {
                if (base.ReadNext())
                {
                    continue;
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            byte current = base.Current;
            if (current != 10)
            {
                if (current != 13)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                else
                {
                    if (!base.ReadNext())
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    if (base.Current != 10)
                    {
                        base.ReadPrev();
                    }
                }
            }
        }

        protected virtual bool TryReadKnownObject() => 
            true;

        protected PdfObjectCollection Objects =>
            this.objects;

        protected int Number =>
            this.number;

        protected int Generation =>
            this.generation;

        protected string CurrentlyReadingDictionaryKey =>
            this.currentlyReadingDictionaryKey;

        protected override bool CanContinueReading
        {
            get
            {
                byte current = base.Current;
                return (base.CanContinueReading && ((current != 60) && (current != 0x3e)));
            }
        }
    }
}

