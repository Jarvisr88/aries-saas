namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class PdfAcroFormGroupField : PdfAcroFormField
    {
        private readonly PdfAcroFormGroupFieldChildrenCollection children;

        public PdfAcroFormGroupField(string name) : base(name)
        {
            this.children = new PdfAcroFormGroupFieldChildrenCollection();
        }

        internal override void CollectNameCollisionInfo(IList<PdfAcroFormFieldNameCollision> infoes)
        {
            HashSet<string> forbiddenNames = new HashSet<string>();
            foreach (PdfAcroFormField field in this.children)
            {
                if (!forbiddenNames.Add(field.Name))
                {
                    infoes.Add(new PdfAcroFormFieldNameCollision(field, forbiddenNames));
                }
                field.CollectNameCollisionInfo(infoes);
            }
        }

        protected internal override PdfInteractiveFormField CreateFormField(IPdfExportFontProvider fontSearch, PdfDocument document, PdfInteractiveFormField parent)
        {
            PdfInteractiveFormField field = new PdfInteractiveFormField(parent, document, this, true);
            foreach (PdfAcroFormField field2 in this.children)
            {
                if (field2 != null)
                {
                    field2.CreateFormField(fontSearch, document, field);
                }
            }
            return field;
        }

        public IList<PdfAcroFormField> Children =>
            this.children;

        private class PdfAcroFormGroupFieldChildrenCollection : IList<PdfAcroFormField>, ICollection<PdfAcroFormField>, IEnumerable<PdfAcroFormField>, IEnumerable
        {
            private readonly IList<PdfAcroFormField> children = new List<PdfAcroFormField>();

            public void Add(PdfAcroFormField item)
            {
                Guard.ArgumentNotNull(item, "item");
                this.children.Add(item);
            }

            public void Clear()
            {
                this.children.Clear();
            }

            public bool Contains(PdfAcroFormField item) => 
                this.children.Contains(item);

            public void CopyTo(PdfAcroFormField[] array, int arrayIndex)
            {
                this.children.CopyTo(array, arrayIndex);
            }

            public IEnumerator<PdfAcroFormField> GetEnumerator() => 
                this.children.GetEnumerator();

            public int IndexOf(PdfAcroFormField item) => 
                this.children.IndexOf(item);

            public void Insert(int index, PdfAcroFormField item)
            {
                Guard.ArgumentNotNull(item, "item");
                this.children.Insert(index, item);
            }

            public bool Remove(PdfAcroFormField item) => 
                this.children.Remove(item);

            public void RemoveAt(int index)
            {
                this.children.RemoveAt(index);
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.children.GetEnumerator();

            public int Count =>
                this.children.Count;

            public bool IsReadOnly =>
                this.children.IsReadOnly;

            public PdfAcroFormField this[int index]
            {
                get => 
                    this.children[index];
                set
                {
                    Guard.ArgumentNotNull(value, "value");
                    this.children[index] = value;
                }
            }
        }
    }
}

