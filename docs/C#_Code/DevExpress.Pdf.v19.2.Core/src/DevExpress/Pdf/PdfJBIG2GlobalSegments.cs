namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class PdfJBIG2GlobalSegments : PdfObject
    {
        private readonly byte[] data;
        private Dictionary<int, JBIG2SegmentHeader> segments;

        private PdfJBIG2GlobalSegments(byte[] data)
        {
            this.data = data;
        }

        internal static PdfJBIG2GlobalSegments Parse(PdfObjectCollection objects, object value)
        {
            Func<object, PdfJBIG2GlobalSegments> create = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<object, PdfJBIG2GlobalSegments> local1 = <>c.<>9__0_0;
                create = <>c.<>9__0_0 = delegate (object o) {
                    PdfReaderStream stream = o as PdfReaderStream;
                    if (stream == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return new PdfJBIG2GlobalSegments(stream.UncompressedData);
                };
            }
            return objects.GetObject<PdfJBIG2GlobalSegments>(value, create);
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            PdfWriterStream.CreateCompressedStream(new PdfWriterDictionary(objects), this.data);

        public byte[] Data =>
            this.data;

        internal Dictionary<int, JBIG2SegmentHeader> Segments
        {
            get
            {
                if (this.segments == null)
                {
                    try
                    {
                        MemoryStream stream;
                        JBIG2Image image = new JBIG2Image();
                        if (this.data.Length == 0)
                        {
                            goto TR_0006;
                        }
                        else
                        {
                            stream = new MemoryStream(this.data);
                        }
                        goto TR_000E;
                    TR_0006:
                        this.segments = image.GlobalSegments;
                        goto TR_0000;
                    TR_000E:
                        try
                        {
                            while (true)
                            {
                                JBIG2SegmentHeader header = new JBIG2SegmentHeader(stream, image);
                                image.GlobalSegments.Add(header.Number, header);
                                if (header.EndOfFile || (stream.Position > (stream.Length - 1L)))
                                {
                                    foreach (JBIG2SegmentHeader header2 in image.GlobalSegments.Values)
                                    {
                                        header2.Process();
                                    }
                                }
                                else
                                {
                                    goto TR_000E;
                                }
                                break;
                            }
                        }
                        finally
                        {
                            if (stream != null)
                            {
                                stream.Dispose();
                            }
                        }
                        goto TR_0006;
                    }
                    catch
                    {
                        this.segments = new Dictionary<int, JBIG2SegmentHeader>();
                    }
                }
            TR_0000:
                return this.segments;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfJBIG2GlobalSegments.<>c <>9 = new PdfJBIG2GlobalSegments.<>c();
            public static Func<object, PdfJBIG2GlobalSegments> <>9__0_0;

            internal PdfJBIG2GlobalSegments <Parse>b__0_0(object o)
            {
                PdfReaderStream stream = o as PdfReaderStream;
                if (stream == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return new PdfJBIG2GlobalSegments(stream.UncompressedData);
            }
        }
    }
}

