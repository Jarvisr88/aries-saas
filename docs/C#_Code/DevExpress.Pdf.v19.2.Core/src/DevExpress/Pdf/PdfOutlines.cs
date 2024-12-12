namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfOutlines : PdfOutlineItem
    {
        internal PdfOutlines(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        internal PdfOutlines(IList<PdfBookmark> bookmarks)
        {
            base.First = PdfOutline.CreateOutlineTree(this, bookmarks);
            base.UpdateCount();
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            object obj2 = base.ToWritableObject(objects);
            if (!base.Closed)
            {
                PdfWriterDictionary dictionary = obj2 as PdfWriterDictionary;
                if (dictionary != null)
                {
                    dictionary.Add("Count", base.Count);
                }
            }
            return obj2;
        }
    }
}

