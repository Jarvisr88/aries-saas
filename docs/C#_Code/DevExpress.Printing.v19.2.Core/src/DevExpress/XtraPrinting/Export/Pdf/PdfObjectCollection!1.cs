namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class PdfObjectCollection<T> where T: PdfDocumentObject
    {
        private List<T> list;

        public PdfObjectCollection()
        {
            this.list = new List<T>();
        }

        public void Add(T pdfDocumentObject)
        {
            if (pdfDocumentObject != null)
            {
                this.list.Add(pdfDocumentObject);
            }
        }

        public void AddUnique(T pdfDocumentObject)
        {
            if ((pdfDocumentObject != null) && (this.list.IndexOf(pdfDocumentObject) == -1))
            {
                this.list.Add(pdfDocumentObject);
            }
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public PdfDictionary CreateDictionary()
        {
            if (this.Count <= 0)
            {
                return null;
            }
            PdfDictionary dictionary = new PdfDictionary();
            for (int i = 0; i < this.Count; i++)
            {
                this[i].AddToDictionary(dictionary);
            }
            return dictionary;
        }

        public void FillUp()
        {
            foreach (T local in this.list)
            {
                local.FillUp();
            }
        }

        public void Register(PdfXRef xRef)
        {
            foreach (T local in this.list)
            {
                local.Register(xRef);
            }
        }

        public void Write(StreamWriter writer)
        {
            foreach (T local in this.list)
            {
                local.Write(writer);
            }
        }

        public int Count =>
            this.list.Count;

        public T this[int index] =>
            this.list[index];

        protected IList<T> List =>
            this.list;
    }
}

