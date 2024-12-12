namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    internal class PdfDocumentWriter : PdfObjectWriter
    {
        private static byte[] headerComment = new byte[] { 0x25, 0xa2, 0xa3, 0x8f, 0x93, 13, 10 };
        private readonly PdfObjectCollection objects;
        private readonly PdfDocument document;
        private readonly PdfSignature signature;
        private readonly Action<int> progressChanged;
        private readonly SortedDictionary<int, long> xref;

        internal PdfDocumentWriter(Stream stream, PdfDocument document, PdfSignature signature, PdfEncryptionParameters encryptionParameters, PdfFileVersion fileVersion, Action<int> progressChanged) : base(stream)
        {
            this.xref = new SortedDictionary<int, long>();
            this.document = document;
            this.signature = signature;
            this.progressChanged = progressChanged;
            this.objects = new PdfObjectCollection(base.Stream, new Func<PdfObjectContainer, PdfObjectPointer>(this.WriteIndirectObject));
            this.objects.PrepareToWrite(document.DocumentCatalog);
            if (encryptionParameters != null)
            {
                this.objects.EncryptionInfo = new PdfEncryptionInfo(document.ID, encryptionParameters);
            }
            this.objects.AddFreeObject(0, 0xffff);
            base.Stream.WriteString(GetVersionString(fileVersion) + "\r\n");
            base.Stream.WriteBytes(headerComment);
        }

        private void ElementWriting(object sender, EventArgs e)
        {
            int writtenObjectsCount = this.objects.WrittenObjectsCount;
            int count = this.document.DocumentCatalog.Objects.Count;
            this.progressChanged((int) Math.Min((double) 100.0, (double) ((100.0 * writtenObjectsCount) / ((double) count))));
        }

        private static string GetVersionString(PdfFileVersion fileVersion)
        {
            switch (fileVersion)
            {
                case PdfFileVersion.Pdf_1_0:
                    return "%PDF-1.0";

                case PdfFileVersion.Pdf_1_1:
                    return "%PDF-1.1";

                case PdfFileVersion.Pdf_1_2:
                    return "%PDF-1.2";

                case PdfFileVersion.Pdf_1_3:
                    return "%PDF-1.3";

                case PdfFileVersion.Pdf_1_4:
                    return "%PDF-1.4";

                case PdfFileVersion.Pdf_1_5:
                    return "%PDF-1.5";

                case PdfFileVersion.Pdf_1_6:
                    return "%PDF-1.6";

                case PdfFileVersion.Pdf_2_0:
                    return "%PDF-2.0";
            }
            return "%PDF-1.7";
        }

        internal PdfObjectCollection Write()
        {
            PdfObjectCollection objects;
            if (this.progressChanged != null)
            {
                this.objects.ElementWriting += new EventHandler(this.ElementWriting);
            }
            try
            {
                if (this.signature != null)
                {
                    this.document.DocumentCatalog.CreateSignatureFormField(this.signature);
                }
                PdfObjectReference[] referenceArray = this.document.Write(this.objects);
                this.objects.FinalizeWritingAndClearWriteParameters();
                this.WriteIndirectObjects();
                long num = this.WriteCrossReferenceTable();
                this.WriteTrailer(referenceArray[0], referenceArray[1], this.objects.AddObject((PdfObject) this.objects.EncryptionInfo));
                base.Stream.WriteString("\r\nstartxref\r\n");
                base.Stream.WriteString(num.ToString(CultureInfo.InvariantCulture));
                base.WriteEndOfDocument();
                if (this.signature != null)
                {
                    this.signature.PatchStream(base.Stream);
                }
                base.Stream.Flush();
                objects = this.objects;
            }
            finally
            {
                if (this.progressChanged != null)
                {
                    this.objects.ElementWriting -= new EventHandler(this.ElementWriting);
                }
            }
            return objects;
        }

        public static PdfObjectCollection Write(Stream stream, PdfDocument document, PdfSaveOptions options, Action<int> progressChanged)
        {
            PdfEncryptionOptions encryptionOptions = options.EncryptionOptions;
            return new PdfDocumentWriter(new BufferedStream(stream), document, options.Signature, encryptionOptions?.EncryptionParameters, PdfFileVersion.Pdf_1_7, progressChanged).Write();
        }

        private long WriteCrossReferenceTable()
        {
            long position = base.Stream.Position;
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;
            base.Stream.WriteString("xref\r\n");
            using (IEnumerator<KeyValuePair<int, long>> enumerator = this.xref.GetEnumerator())
            {
                int key = 0;
                int firstNumber = 0;
                List<string> list1 = new List<string>();
                list1.Add("0000000000 00000 f\r\n");
                List<string> references = list1;
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        this.WriteReferencesSection(firstNumber, references);
                        break;
                    }
                    KeyValuePair<int, long> current = enumerator.Current;
                    if (++key != current.Key)
                    {
                        this.WriteReferencesSection(firstNumber, references);
                        key = current.Key;
                        firstNumber = current.Key;
                        references.Clear();
                    }
                    object[] args = new object[] { current.Value };
                    references.Add(string.Format(invariantCulture, "{0:0000000000} 00000 n\r\n", args));
                }
            }
            return position;
        }

        public override PdfObjectPointer WriteIndirectObject(PdfObjectContainer container)
        {
            PdfObjectPointer pointer = base.WriteIndirectObject(container);
            this.xref.Add(pointer.ObjectNumber, pointer.Offset);
            this.objects.AddItem(pointer, true);
            return pointer;
        }

        private void WriteIndirectObjects()
        {
            using (IEnumerator<PdfObjectContainer> enumerator = this.objects.EnumeratorOfContainers)
            {
                while (enumerator.MoveNext())
                {
                    this.WriteIndirectObject(enumerator.Current);
                }
            }
        }

        private void WriteReferencesSection(int firstNumber, List<string> references)
        {
            object[] args = new object[] { firstNumber, references.Count };
            base.Stream.WriteStringFormat("{0} {1}\r\n", args);
            foreach (string str in references)
            {
                base.Stream.WriteString(str);
            }
        }

        private void WriteTrailer(PdfObjectReference info, PdfObjectReference catalog, PdfObjectReference encryptionReference)
        {
            base.Stream.WriteString("trailer\r\n");
            PdfWriterDictionary dictionary = new PdfWriterDictionary(null);
            dictionary.Add("Size", (this.xref.Keys.Count == 0) ? 1 : (((IEnumerable<int>) this.xref.Keys).Max() + 1));
            if (info != null)
            {
                dictionary.Add("Info", info);
            }
            dictionary.Add("ID", this.document.ID);
            if (encryptionReference != null)
            {
                dictionary.Add("Encrypt", encryptionReference);
            }
            dictionary.Add("Root", catalog);
            base.Stream.WriteObject(dictionary, -1);
        }

        internal PdfObjectCollection Objects =>
            this.objects;
    }
}

