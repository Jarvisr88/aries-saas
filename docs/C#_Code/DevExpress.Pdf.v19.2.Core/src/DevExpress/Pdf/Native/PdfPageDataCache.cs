namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class PdfPageDataCache : PdfCache<int, PdfPageData>
    {
        public const long DefaultLimit = 0x41L;
        private readonly IList<PdfPage> documentPages;
        private readonly bool recognizeAnnotationsData;
        private readonly bool clipTextToCropBox;

        public PdfPageDataCache(IList<PdfPage> documentPages, bool recognizeAnnotationsData, bool clipTextToCropBox = true) : base((long) 0x41)
        {
            this.documentPages = documentPages;
            this.recognizeAnnotationsData = recognizeAnnotationsData;
            this.clipTextToCropBox = clipTextToCropBox;
        }

        protected PdfPageData GetData(int key)
        {
            PdfPageData data;
            try
            {
                data = PdfDataRecognizer.Recognize(this.documentPages[key], this.recognizeAnnotationsData, this.clipTextToCropBox);
            }
            catch
            {
                return new PdfPageData(new List<PdfTextLine>(), new List<PdfPageImageData>());
            }
            return data;
        }

        public IList<PdfPageImageData> GetImageData(int pageIndex) => 
            this[pageIndex].ImageData;

        public IList<PdfTextLine> GetPageLines(int pageIndex) => 
            this[pageIndex].TextData;

        protected override long GetSizeOfValue(PdfPageData value) => 
            1L;

        public IList<PdfPage> DocumentPages =>
            this.documentPages;

        public PdfPageData this[int key]
        {
            get
            {
                PdfPageData data;
                if (base.ObjectStorage.TryGetValue(key, out data))
                {
                    base.UpdateQueue(key);
                }
                else
                {
                    data = this.GetData(key);
                    base.AddValue(key, data);
                }
                base.CheckCapacity();
                return data;
            }
        }
    }
}

