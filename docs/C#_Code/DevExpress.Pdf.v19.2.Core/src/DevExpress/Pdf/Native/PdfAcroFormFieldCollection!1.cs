namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Localization;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class PdfAcroFormFieldCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private readonly IList<T> fields;
        private readonly HashSet<string> names;

        protected PdfAcroFormFieldCollection()
        {
            this.fields = new List<T>();
            this.names = new HashSet<string>();
        }

        protected PdfAcroFormFieldCollection(PdfDocument document)
        {
            this.fields = new List<T>();
            this.names = new HashSet<string>();
            if ((document != null) && (document.AcroForm != null))
            {
                foreach (PdfInteractiveFormField field in document.AcroForm.Fields)
                {
                    this.names.Add(field.Name);
                }
            }
        }

        public void Add(T item)
        {
            if (!this.names.Add(this.GetFieldName(item)))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgAcroFormFieldNameDuplication), "name");
            }
            this.fields.Add(item);
        }

        public void Clear()
        {
            this.fields.Clear();
            this.names.Clear();
        }

        public bool Contains(T item) => 
            this.fields.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.fields.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator() => 
            this.fields.GetEnumerator();

        protected abstract string GetFieldName(T item);
        public bool Remove(T item)
        {
            this.names.Remove(this.GetFieldName(item));
            return this.fields.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.fields.GetEnumerator();

        public int Count =>
            this.fields.Count;

        public bool IsReadOnly =>
            false;
    }
}

