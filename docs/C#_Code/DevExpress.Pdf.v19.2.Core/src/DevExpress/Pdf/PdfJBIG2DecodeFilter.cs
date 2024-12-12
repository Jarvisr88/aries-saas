namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfJBIG2DecodeFilter : PdfFilter
    {
        private const string globalSegmentsDictionaryKey = "JBIG2Globals";
        internal const string Name = "JBIG2Decode";
        private readonly PdfJBIG2GlobalSegments globalSegments;

        internal PdfJBIG2DecodeFilter(PdfReaderDictionary parameters)
        {
            if (parameters != null)
            {
                object objectReference = parameters.GetObjectReference("JBIG2Globals");
                if (objectReference != null)
                {
                    this.globalSegments = PdfJBIG2GlobalSegments.Parse(parameters.Objects, objectReference);
                }
            }
        }

        protected internal override byte[] Decode(byte[] data) => 
            JBIG2Image.Decode(data, this.globalSegments?.Segments);

        protected internal override PdfWriterDictionary Write(PdfObjectCollection objects)
        {
            if (this.globalSegments == null)
            {
                return null;
            }
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("JBIG2Globals", this.globalSegments);
            return dictionary;
        }

        public PdfJBIG2GlobalSegments GlobalSegments =>
            this.globalSegments;

        protected internal override string FilterName =>
            "JBIG2Decode";
    }
}

