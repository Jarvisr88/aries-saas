namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class PdfInteractiveFormFieldCollection : PdfObject, IList<PdfInteractiveFormField>, ICollection<PdfInteractiveFormField>, IEnumerable<PdfInteractiveFormField>, IEnumerable
    {
        private readonly List<PdfInteractiveFormField> fields;

        public PdfInteractiveFormFieldCollection()
        {
            this.fields = new List<PdfInteractiveFormField>();
        }

        private PdfInteractiveFormFieldCollection(int objectNumber) : base(objectNumber)
        {
            this.fields = new List<PdfInteractiveFormField>();
        }

        public PdfInteractiveFormFieldCollection(IList<object> fieldsArray, PdfInteractiveForm form, PdfInteractiveFormField parent, PdfObjectCollection objects)
        {
            this.fields = new List<PdfInteractiveFormField>();
            if (fieldsArray != null)
            {
                foreach (object obj2 in fieldsArray)
                {
                    PdfInteractiveFormField item = objects.GetInteractiveFormField(form, parent, obj2);
                    if (item != null)
                    {
                        this.fields.Add(item);
                    }
                }
            }
        }

        public void AddFieldWithAncestors(PdfInteractiveFormField formField)
        {
            if (formField != null)
            {
                if (formField.Parent == null)
                {
                    if (!this.fields.Contains(formField))
                    {
                        this.fields.Add(formField);
                    }
                }
                else if (!formField.Parent.Kids.Contains(formField))
                {
                    formField.Parent.Kids.Add(formField);
                    this.AddFieldWithAncestors(formField.Parent);
                }
            }
        }

        protected internal override PdfObject GetDeferredSavedObject(PdfObjectCollection objects, bool isClonning)
        {
            int objectNumber = objects.LastObjectNumber + 1;
            objects.LastObjectNumber = objectNumber;
            return new PdfInteractiveFormFieldCollection(objectNumber);
        }

        protected internal override bool IsDeferredObject(bool isCloning) => 
            isCloning;

        void ICollection<PdfInteractiveFormField>.Add(PdfInteractiveFormField item)
        {
            this.fields.Add(item);
        }

        void ICollection<PdfInteractiveFormField>.Clear()
        {
            this.fields.Clear();
        }

        bool ICollection<PdfInteractiveFormField>.Contains(PdfInteractiveFormField item) => 
            this.fields.Contains(item);

        void ICollection<PdfInteractiveFormField>.CopyTo(PdfInteractiveFormField[] array, int arrayIndex)
        {
            this.fields.CopyTo(array, arrayIndex);
        }

        bool ICollection<PdfInteractiveFormField>.Remove(PdfInteractiveFormField item) => 
            this.fields.Remove(item);

        IEnumerator<PdfInteractiveFormField> IEnumerable<PdfInteractiveFormField>.GetEnumerator() => 
            this.fields.GetEnumerator();

        int IList<PdfInteractiveFormField>.IndexOf(PdfInteractiveFormField item) => 
            this.fields.IndexOf(item);

        void IList<PdfInteractiveFormField>.Insert(int index, PdfInteractiveFormField item)
        {
            this.fields.Insert(index, item);
        }

        void IList<PdfInteractiveFormField>.RemoveAt(int index)
        {
            this.fields.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.fields.GetEnumerator();

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            (this.fields.Count > 0) ? new PdfWritableObjectArray((IEnumerable<PdfObject>) this.fields, objects) : null;

        public PdfInteractiveFormField this[int index]
        {
            get => 
                this.fields[index];
            set
            {
                throw new NotSupportedException();
            }
        }

        public int Count =>
            this.fields.Count;

        bool ICollection<PdfInteractiveFormField>.IsReadOnly =>
            false;
    }
}

