namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class PdfDocumentReader : PdfDocumentStructureReader
    {
        private const string signature = "%PDF-";
        private static readonly PdfTokenDescription xrefToken;
        private static readonly PdfTokenDescription startxrefToken;
        private static readonly Dictionary<string, PdfFileVersion> versionMapping;
        private readonly PdfGetPasswordAction getPasswordAction;
        private object encryptValue;
        private PdfFileVersion version;
        private PdfObjectReference[] idObjects;
        private object documentInfoValue;
        private byte[][] id;

        static PdfDocumentReader()
        {
            byte[] token = new byte[] { 120, 0x72, 0x65, 0x66 };
            xrefToken = new PdfTokenDescription(token);
            byte[] buffer2 = new byte[] { 0x73, 0x74, 0x61, 0x72, 0x74, 120, 0x72, 0x65, 0x66 };
            startxrefToken = new PdfTokenDescription(buffer2);
            Dictionary<string, PdfFileVersion> dictionary1 = new Dictionary<string, PdfFileVersion>();
            dictionary1.Add("2.0", PdfFileVersion.Pdf_2_0);
            dictionary1.Add("1.7", PdfFileVersion.Pdf_1_7);
            dictionary1.Add("1.6", PdfFileVersion.Pdf_1_6);
            dictionary1.Add("1.5", PdfFileVersion.Pdf_1_5);
            dictionary1.Add("1.4", PdfFileVersion.Pdf_1_4);
            dictionary1.Add("1.3", PdfFileVersion.Pdf_1_3);
            dictionary1.Add("1.2", PdfFileVersion.Pdf_1_2);
            dictionary1.Add("1.1", PdfFileVersion.Pdf_1_1);
            dictionary1.Add("1.0", PdfFileVersion.Pdf_1_0);
            versionMapping = dictionary1;
        }

        private PdfDocumentReader(PdfDocumentStream documentStream, PdfGetPasswordAction getPasswordAction, bool isInvalid) : base(documentStream)
        {
            this.version = PdfFileVersion.Unknown;
            this.getPasswordAction = getPasswordAction;
            this.version = this.GetVersion(documentStream);
            PdfObjectCollection objects = base.Objects;
            long length = documentStream.Length;
            try
            {
                int startXRef = this.GetStartXRef(documentStream);
                bool flag = true;
                while (true)
                {
                    PdfReaderDictionary trailerDictionary = this.ReadTrailer((long) startXRef, true);
                    if (flag)
                    {
                        this.UpdateTrailer(trailerDictionary, objects);
                    }
                    flag = false;
                    int? integer = trailerDictionary.GetInteger("Prev");
                    startXRef = (integer != null) ? integer.GetValueOrDefault() : -1;
                    if (startXRef == -1)
                    {
                        if (isInvalid)
                        {
                            this.ReadCorruptedDocument(objects);
                        }
                        break;
                    }
                }
            }
            catch
            {
                this.ReadCorruptedDocument(objects);
            }
        }

        internal static double ConvertToDouble(object value)
        {
            if (value is double)
            {
                return (double) value;
            }
            if (!(value is int))
            {
                ThrowIncorrectDataException();
            }
            return (double) ((int) value);
        }

        internal static int ConvertToInteger(object value)
        {
            if (value is int)
            {
                return (int) value;
            }
            if (!(value is double))
            {
                ThrowIncorrectDataException();
            }
            return (int) ((long) Math.Truncate((double) value));
        }

        internal static string ConvertToString(byte[] value) => 
            IsUnicode(value) ? ConvertToUnicodeString(value) : EncodingHelpers.AnsiEncoding.GetString(value, 0, value.Length);

        internal static string ConvertToTextString(byte[] value) => 
            IsUnicode(value) ? ConvertToUnicodeString(value) : PdfDocEncoding.GetString(value);

        internal static string ConvertToUnicodeString(byte[] value) => 
            Encoding.BigEndianUnicode.GetString(value, 0, value.Length).Substring(1);

        internal static IList<PdfRange> CreateDomain(IList<object> array)
        {
            if (array == null)
            {
                ThrowIncorrectDataException();
            }
            int count = array.Count;
            if ((count == 0) || ((count % 2) > 0))
            {
                ThrowIncorrectDataException();
            }
            count /= 2;
            List<PdfRange> list = new List<PdfRange>();
            int num2 = 0;
            for (int i = 0; num2 < count; i += 2)
            {
                list.Add(CreateDomain(array, i));
                num2++;
            }
            return list;
        }

        internal static PdfRange CreateDomain(IList<object> array, int index)
        {
            double min = ConvertToDouble(array[index++]);
            double max = ConvertToDouble(array[index]);
            if (max < min)
            {
                ThrowIncorrectDataException();
            }
            return new PdfRange(min, max);
        }

        internal static PdfPoint CreatePoint(IList<object> array, int index) => 
            new PdfPoint(ConvertToDouble(array[index++]), ConvertToDouble(array[index]));

        internal static PdfPoint[] CreatePointArray(IList<object> array)
        {
            if (array == null)
            {
                return null;
            }
            int count = array.Count;
            if ((count % 2) != 0)
            {
                ThrowIncorrectDataException();
            }
            count /= 2;
            PdfPoint[] pointArray = new PdfPoint[count];
            int index = 0;
            for (int i = 0; index < count; i += 2)
            {
                pointArray[index] = CreatePoint(array, i);
                index++;
            }
            return pointArray;
        }

        private long FindLastEndOfFile(PdfDocumentStream streamReader)
        {
            long position = streamReader.Position;
            streamReader.SetPositionFromEnd(30);
            long num2 = streamReader.FindLastToken(PdfDocumentStructureReader.EofToken, false);
            if (num2 == -1L)
            {
                streamReader.Position = position;
                num2 = streamReader.FindLastToken(PdfDocumentStructureReader.EofToken);
            }
            return num2;
        }

        internal static void FindObjects(PdfObjectCollection objects, PdfDocumentStream streamReader)
        {
            objects.RemoveCorruptedObjects();
            streamReader.Position = 0L;
            long length = streamReader.Length;
            long position = streamReader.Position;
            long num3 = -1L;
            for (int i = streamReader.ReadByte(); i >= 0; i = streamReader.ReadByte())
            {
                if (!PdfObjectParser.IsSpaceSymbol((byte) i))
                {
                    if (i == 0x25)
                    {
                        streamReader.ReadString();
                    }
                    else
                    {
                        streamReader.Position = position;
                        if (streamReader.ReadToken(xrefToken))
                        {
                            num3 = streamReader.Position;
                            if (!streamReader.FindToken(PdfDocumentStructureReader.EofToken))
                            {
                                break;
                            }
                        }
                        else
                        {
                            streamReader.Position = position;
                            if (streamReader.ReadToken(startxrefToken))
                            {
                                if ((streamReader.ReadNumber() != 0) && streamReader.FindToken(PdfDocumentStructureReader.EofToken))
                                {
                                    num3 = position;
                                }
                            }
                            else
                            {
                                try
                                {
                                    PdfObjectSlot slot = streamReader.ReadObject(position);
                                    objects.AddItem(slot, true);
                                }
                                catch
                                {
                                    if (num3 >= 0L)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                position = streamReader.Position;
            }
            streamReader.Position = Math.Max(0L, num3);
        }

        private static PdfFileVersion FindPdfVersion(string versionString)
        {
            PdfFileVersion version;
            using (Dictionary<string, PdfFileVersion>.Enumerator enumerator = versionMapping.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<string, PdfFileVersion> current = enumerator.Current;
                        if (!versionString.StartsWith(current.Key))
                        {
                            continue;
                        }
                        version = current.Value;
                    }
                    else
                    {
                        return PdfFileVersion.Unknown;
                    }
                    break;
                }
            }
            return version;
        }

        internal static PdfFileVersion FindVersion(string versionString)
        {
            PdfFileVersion version;
            using (Dictionary<string, PdfFileVersion>.Enumerator enumerator = versionMapping.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<string, PdfFileVersion> current = enumerator.Current;
                        if (versionString != current.Key)
                        {
                            continue;
                        }
                        version = current.Value;
                    }
                    else
                    {
                        return PdfFileVersion.Unknown;
                    }
                    break;
                }
            }
            return version;
        }

        private int GetStartXRef(PdfDocumentStream streamReader)
        {
            long num = this.FindLastEndOfFile(streamReader) - 50;
            if (num < 0L)
            {
                ThrowIncorrectDataException();
            }
            streamReader.Position = num;
            int? nullable = PdfObjectParser.ParseStartXRef(streamReader.ReadBytes(50));
            if (nullable == null)
            {
                ThrowIncorrectDataException();
            }
            return nullable.Value;
        }

        private PdfFileVersion GetVersion(PdfDocumentStream streamReader)
        {
            string str = streamReader.ReadString();
            while (!str.StartsWith("%PDF-"))
            {
                if ((str.Length == 0) || (str[0] != '%'))
                {
                    ThrowIncorrectDataException();
                }
                str = streamReader.ReadString();
            }
            return FindPdfVersion(str.Substring("%PDF-".Length));
        }

        internal static bool IsUnicode(byte[] value) => 
            (value.Length >= 2) && ((value[0] == 0xfe) && (value[1] == 0xff));

        private PdfDocument Read(bool detachStreamAfterLoadComplete)
        {
            PdfEncryptionInfo info;
            PdfObjectCollection objects = base.Objects;
            if (detachStreamAfterLoadComplete)
            {
                objects.ResolveAllSlots();
            }
            if (this.encryptValue == null)
            {
                info = null;
            }
            else
            {
                if ((this.id == null) && (this.idObjects != null))
                {
                    byte[] buffer = objects.TryResolve(this.idObjects[0].Number, null) as byte[];
                    byte[] buffer2 = objects.TryResolve(this.idObjects[1].Number, null) as byte[];
                    if ((buffer == null) || (buffer2 == null))
                    {
                        ThrowIncorrectDataException();
                    }
                    this.id = new byte[][] { buffer, buffer2 };
                }
                info = objects.EnsureEncryptionInfo(this.encryptValue, this.id, this.getPasswordAction);
            }
            PdfReaderDictionary dictionary = objects.TryResolve(base.RootObjectReference, null) as PdfReaderDictionary;
            if (dictionary == null)
            {
                ThrowIncorrectDataException();
            }
            PdfDocumentCatalog documentCatalog = new PdfDocumentCatalog(dictionary);
            string str = documentCatalog.Version;
            if (!string.IsNullOrEmpty(str))
            {
                PdfFileVersion version = FindVersion(str);
                if (version > this.version)
                {
                    this.version = version;
                }
            }
            PdfDocumentInfo info2 = null;
            if (this.documentInfoValue != null)
            {
                PdfReaderDictionary dictionary2 = objects.TryResolve(this.documentInfoValue, null) as PdfReaderDictionary;
                if (dictionary2 != null)
                {
                    info2 = new PdfDocumentInfo(dictionary2);
                }
            }
            PdfDocumentInfo documentInfo = info2;
            if (info2 == null)
            {
                PdfDocumentInfo local1 = info2;
                documentInfo = new PdfDocumentInfo();
            }
            return new PdfDocument(this.version, documentInfo, documentCatalog, info, this.id);
        }

        internal static PdfDocument Read(Stream stream, bool detachStreamAfterLoadComplete, PdfGetPasswordAction getPasswordAction = null)
        {
            if (!stream.CanRead || !stream.CanSeek)
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgUnsupportedStreamForLoadOperation), "stream");
            }
            BufferedStream stream2 = new BufferedStream(stream);
            int num = 0;
            int length = "%PDF-".Length;
            long num3 = stream.Length;
            while (true)
            {
                PdfDocument document;
                if (stream2.Position < num3)
                {
                    int num4 = stream2.ReadByte();
                    num = (num4 != "%PDF-"[num]) ? ((num <= 0) ? 0 : ((num4 == "%PDF-"[0]) ? 1 : 0)) : (num + 1);
                    if (num != length)
                    {
                        continue;
                    }
                    stream2.Position -= length;
                }
                try
                {
                    if (stream2.Position >= num3)
                    {
                        stream2.Position = 0L;
                    }
                    try
                    {
                        PdfGetPasswordAction action1 = getPasswordAction;
                        if (getPasswordAction == null)
                        {
                            PdfGetPasswordAction local1 = getPasswordAction;
                            action1 = <>c.<>9__15_0;
                            if (<>c.<>9__15_0 == null)
                            {
                                PdfGetPasswordAction local2 = <>c.<>9__15_0;
                                action1 = <>c.<>9__15_0 = (PdfGetPasswordAction) (n => null);
                            }
                        }
                        document = new PdfDocumentReader(PdfDocumentStream.CreateStreamForReading(stream2), action1, false).Read(detachStreamAfterLoadComplete);
                    }
                    catch (PdfIncorrectPasswordException)
                    {
                        throw;
                    }
                    catch
                    {
                        long num5;
                        BufferedStream stream1 = stream2;
                        stream1.Position = (num5 >= num3) ? 0L : num5;
                        PdfGetPasswordAction action2 = (PdfGetPasswordAction) PdfDocumentStream.CreateStreamForReading(stream2);
                        if (getPasswordAction == null)
                        {
                            PdfDocumentStream local5 = PdfDocumentStream.CreateStreamForReading(stream2);
                            action2 = <>c.<>9__15_1;
                            if (<>c.<>9__15_1 == null)
                            {
                                PdfGetPasswordAction local6 = <>c.<>9__15_1;
                                action2 = <>c.<>9__15_1 = (PdfGetPasswordAction) (n => null);
                            }
                        }
                        document = new PdfDocumentReader((PdfDocumentStream) getPasswordAction, action2, true).Read(detachStreamAfterLoadComplete);
                    }
                }
                catch
                {
                    if (stream2 != null)
                    {
                        stream2 = null;
                    }
                    throw;
                }
                return document;
            }
        }

        private void ReadCorruptedDocument(PdfObjectCollection objects)
        {
            PdfDocumentStream documentStream = base.DocumentStream;
            FindObjects(objects, documentStream);
            if (documentStream.FindToken(PdfDocumentStructureReader.TrailerToken))
            {
                this.UpdateTrailer(PdfDocumentParser.ParseDictionary(objects, 0, 0, new PdfArrayDataStream(this.ReadTrailerData())), objects);
            }
        }

        private PdfReaderDictionary ReadTrailer(long offset, bool fillCrossReferenceTable)
        {
            PdfObjectCollection objects = base.Objects;
            PdfDocumentStream documentStream = base.DocumentStream;
            documentStream.Position = offset;
            if (!documentStream.ReadToken(xrefToken))
            {
                PdfReaderStream stream2 = PdfDocumentParser.ParseStream(objects, 0, 0, documentStream.ReadStreamBasedIndirectObject(offset).Stream);
                PdfReaderDictionary dictionary = stream2.Dictionary;
                IList<object> array = dictionary.GetArray("W");
                if ((array == null) || (array.Count != 3))
                {
                    ThrowIncorrectDataException();
                }
                if (fillCrossReferenceTable)
                {
                    object obj2 = array[0];
                    object obj3 = array[1];
                    object obj4 = array[2];
                    if (!(obj2 is int) || (!(obj3 is int) || !(obj4 is int)))
                    {
                        ThrowIncorrectDataException();
                    }
                    List<PdfIndexDescription> indices = new List<PdfIndexDescription>();
                    array = dictionary.GetArray("Index");
                    if (array == null)
                    {
                        int? integer = dictionary.GetInteger("Size");
                        if (integer == null)
                        {
                            ThrowIncorrectDataException();
                        }
                        int count = integer.Value;
                        if (count < 1)
                        {
                            ThrowIncorrectDataException();
                        }
                        indices.Add(new PdfIndexDescription(0, count));
                    }
                    else
                    {
                        int count = array.Count;
                        if ((count % 2) == 1)
                        {
                            ThrowIncorrectDataException();
                        }
                        count /= 2;
                        int num11 = 0;
                        int num12 = 0;
                        while (num11 < count)
                        {
                            object obj5 = array[num12++];
                            object obj6 = array[num12++];
                            if (!(obj5 is int) || !(obj6 is int))
                            {
                                ThrowIncorrectDataException();
                            }
                            indices.Add(new PdfIndexDescription((int) obj5, (int) obj6));
                            num11++;
                        }
                    }
                    PdfCrossReferenceDecoder.Decode(stream2.UncompressedData, indices, objects, (int) obj2, (int) obj3, (int) obj4);
                }
                return dictionary;
            }
            if (!fillCrossReferenceTable)
            {
                if (!documentStream.FindToken(PdfDocumentStructureReader.TrailerToken))
                {
                    ThrowIncorrectDataException();
                }
            }
            else
            {
                while (true)
                {
                    long position = documentStream.Position;
                    int num2 = documentStream.SkipSpaces();
                    if ((num2 == -1) || !PdfObjectParser.IsDigitSymbol((byte) num2))
                    {
                        documentStream.Position = position;
                        if (!documentStream.ReadToken(PdfDocumentStructureReader.TrailerToken))
                        {
                            ThrowIncorrectDataException();
                        }
                        break;
                    }
                    documentStream.Position -= 1L;
                    int number = documentStream.ReadNumber();
                    int num4 = documentStream.ReadNumber();
                    for (int i = 0; i < num4; i++)
                    {
                        long num7 = documentStream.ReadNumber(10);
                        if (documentStream.ReadByte() != 0x20)
                        {
                            ThrowIncorrectDataException();
                        }
                        int generation = (int) documentStream.ReadNumber(5);
                        if (documentStream.ReadByte() != 0x20)
                        {
                            ThrowIncorrectDataException();
                        }
                        num2 = documentStream.ReadByte();
                        if (num2 == 0x66)
                        {
                            objects.AddFreeObject(number, generation);
                        }
                        else if (num2 == 110)
                        {
                            objects.AddItem(new PdfObjectSlot(number, generation, num7), false);
                        }
                        else
                        {
                            ThrowIncorrectDataException();
                        }
                        num2 = documentStream.ReadByte();
                        if (num2 != 10)
                        {
                            if ((num2 != 13) && (num2 != 0x20))
                            {
                                ThrowIncorrectDataException();
                            }
                            else
                            {
                                num2 = documentStream.ReadByte();
                                if ((num2 != 10) && (num2 != 13))
                                {
                                    ThrowIncorrectDataException();
                                }
                            }
                        }
                        number++;
                    }
                }
            }
            return PdfDocumentParser.ParseDictionary(objects, 0, 0, new PdfArrayDataStream(this.ReadTrailerData()));
        }

        private byte[] ReadTrailerData()
        {
            PdfDocumentStream documentStream = base.DocumentStream;
            List<byte> list = new List<byte>();
            PdfTokenDescription description = PdfTokenDescription.BeginCompare(startxrefToken);
            while (true)
            {
                int num = documentStream.ReadByte();
                if (num == -1)
                {
                    return list.ToArray();
                }
                byte item = (byte) num;
                list.Add(item);
                if (description.Compare(item))
                {
                    int length = description.Length;
                    list.RemoveRange(list.Count - length, length);
                    return list.ToArray();
                }
            }
        }

        protected override void UpdateTrailer(PdfReaderDictionary trailerDictionary, PdfObjectCollection objects)
        {
            base.UpdateTrailer(trailerDictionary, objects);
            if (!trailerDictionary.TryGetValue("Encrypt", out this.encryptValue))
            {
                this.encryptValue = null;
            }
            trailerDictionary.TryGetValue("Info", out this.documentInfoValue);
            IList<object> array = trailerDictionary.GetArray("ID");
            if (array != null)
            {
                if (array.Count != 2)
                {
                    ThrowIncorrectDataException();
                }
                byte[] buffer = array[0] as byte[];
                if (buffer != null)
                {
                    this.id = new byte[2][];
                    this.id[0] = buffer;
                    byte[] buffer2 = array[1] as byte[];
                    if (buffer2 == null)
                    {
                        ThrowIncorrectDataException();
                    }
                    this.id[1] = buffer2;
                }
                else
                {
                    PdfObjectReference reference = array[0] as PdfObjectReference;
                    PdfObjectReference reference2 = array[1] as PdfObjectReference;
                    if ((reference == null) || (reference2 == null))
                    {
                        ThrowIncorrectDataException();
                    }
                    this.idObjects = new PdfObjectReference[] { reference, reference };
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfDocumentReader.<>c <>9 = new PdfDocumentReader.<>c();
            public static PdfGetPasswordAction <>9__15_0;
            public static PdfGetPasswordAction <>9__15_1;

            internal string <Read>b__15_0(int n) => 
                null;

            internal string <Read>b__15_1(int n) => 
                null;
        }
    }
}

