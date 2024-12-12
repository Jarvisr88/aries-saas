namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class FdfDocumentReader : PdfDocumentStructureReader
    {
        private FdfDocumentReader(PdfDocumentStream streamReader) : base(streamReader)
        {
            string str = streamReader.ReadString();
            long length = streamReader.Length;
            long position = streamReader.Position;
            streamReader.SkipSpaces();
            streamReader.Position = position;
            long num3 = streamReader.FindLastToken(PdfDocumentStructureReader.TrailerToken);
            streamReader.Position = position;
            while (true)
            {
                position = streamReader.Position;
                if (!streamReader.ReadToken(PdfDocumentStructureReader.TrailerToken))
                {
                    base.Objects.AddItem(streamReader.ReadObject(position), true);
                    if (streamReader.Position < length)
                    {
                        continue;
                    }
                }
                streamReader.Position = num3;
                this.UpdateTrailer(PdfDocumentParser.ParseDictionary(base.Objects, 0, 0, new PdfArrayDataStream(this.ReadFdfTrailerData())), base.Objects);
                return;
            }
        }

        private void ParseItem(PdfFormData result, string parentName, PdfReaderDictionary item)
        {
            object obj2;
            string str = ((parentName == null) ? string.Empty : (parentName + ".")) + item.GetString("T");
            if (!item.TryGetValue("V", out obj2))
            {
                IList<object> array = item.GetArray("Kids");
                if (array != null)
                {
                    foreach (PdfReaderDictionary dictionary in array)
                    {
                        this.ParseItem(result, str, dictionary);
                    }
                }
            }
            else
            {
                IList<object> list = obj2 as IList<object>;
                if (list != null)
                {
                    List<string> list2 = new List<string>();
                    foreach (byte[] buffer in list)
                    {
                        list2.Add(PdfDocumentReader.ConvertToString(buffer));
                    }
                    result[str].Value = list2;
                }
                else
                {
                    byte[] buffer2 = obj2 as byte[];
                    if (buffer2 != null)
                    {
                        string str2 = PdfDocumentReader.ConvertToString(buffer2);
                        if (str2 != null)
                        {
                            result[str].Value = str2;
                        }
                        else
                        {
                            ThrowIncorrectDataException();
                        }
                    }
                    else
                    {
                        PdfName name = obj2 as PdfName;
                        if (name != null)
                        {
                            result[str].Value = name.Name;
                        }
                        else
                        {
                            ThrowIncorrectDataException();
                        }
                    }
                }
            }
        }

        private void Read(PdfFormData root)
        {
            base.Objects.ResolveAllSlots();
            PdfReaderDictionary dictionary = base.Objects.TryResolve(base.RootObjectReference, null) as PdfReaderDictionary;
            if (dictionary == null)
            {
                ThrowIncorrectDataException();
            }
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("FDF");
            if (dictionary2 == null)
            {
                ThrowIncorrectDataException();
            }
            IList<object> array = dictionary2.GetArray("Fields");
            if (array != null)
            {
                foreach (PdfReaderDictionary dictionary3 in array)
                {
                    this.ParseItem(root, null, dictionary3);
                }
            }
        }

        internal static void Read(Stream stream, PdfFormData root)
        {
            new FdfDocumentReader(PdfDocumentStream.CreateStreamForReading(new BufferedStream(stream))).Read(root);
        }

        private byte[] ReadFdfTrailerData()
        {
            List<byte> list = new List<byte>();
            PdfTokenDescription description = PdfTokenDescription.BeginCompare(PdfDocumentStructureReader.EofToken);
            PdfDocumentStream documentStream = base.DocumentStream;
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
    }
}

