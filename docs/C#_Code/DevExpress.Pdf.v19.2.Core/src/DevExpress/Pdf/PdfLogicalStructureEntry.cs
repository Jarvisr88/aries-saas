namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfLogicalStructureEntry : PdfLogicalStructureItem
    {
        private const string kidsDictionaryKey = "K";
        private readonly PdfLogicalStructure logicalStructure;
        private readonly PdfDocumentCatalog documentCatalog;
        private IList<PdfLogicalStructureItem> kids;
        private object kidsValue;

        protected PdfLogicalStructureEntry(PdfLogicalStructure logicalStructure, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.documentCatalog = dictionary.Objects.DocumentCatalog;
            PdfLogicalStructure structure1 = logicalStructure;
            if (logicalStructure == null)
            {
                PdfLogicalStructure local1 = logicalStructure;
                structure1 = (PdfLogicalStructure) this;
            }
            this.logicalStructure = structure1;
            dictionary.TryGetValue("K", out this.kidsValue);
        }

        protected internal override void Resolve()
        {
            if (this.kidsValue != null)
            {
                PdfObjectCollection objects = this.documentCatalog.Objects;
                this.kids = new List<PdfLogicalStructureItem>();
                object obj2 = objects.TryResolve(this.kidsValue, null);
                if (obj2 is PdfReaderDictionary)
                {
                    PdfLogicalStructureItem item = objects.GetLogicalStructureItem(this.logicalStructure, this, this.kidsValue);
                    if (item != null)
                    {
                        this.kids.Add(item);
                    }
                }
                else
                {
                    IList<object> list = obj2 as IList<object>;
                    if (list == null)
                    {
                        PdfLogicalStructureItem item = objects.GetLogicalStructureItem(this.logicalStructure, this, obj2);
                        if (item != null)
                        {
                            this.kids.Add(item);
                        }
                    }
                    else
                    {
                        foreach (object obj3 in list)
                        {
                            PdfLogicalStructureItem item = objects.GetLogicalStructureItem(this.logicalStructure, this, obj3);
                            if (item != null)
                            {
                                this.kids.Add(item);
                            }
                        }
                    }
                }
                if (this.kids.Count == 0)
                {
                    this.kids = null;
                }
                else
                {
                    foreach (PdfLogicalStructureItem item4 in this.kids)
                    {
                        item4.Resolve();
                    }
                }
                this.kidsValue = null;
            }
        }

        protected void WriteKids(PdfWriterDictionary dictionary, PdfObjectCollection collection)
        {
            if (this.kids != null)
            {
                dictionary.Add("K", (this.kids.Count == 1) ? this.kids[0].Write(collection) : new PdfWritableConvertibleArray<PdfLogicalStructureItem>(this.kids, value => value.Write(collection)));
            }
        }

        public IList<PdfLogicalStructureItem> Kids
        {
            get
            {
                this.Resolve();
                return this.kids;
            }
        }

        protected PdfLogicalStructure LogicalStructure =>
            this.logicalStructure;

        internal PdfDocumentCatalog DocumentCatalog =>
            this.documentCatalog;
    }
}

