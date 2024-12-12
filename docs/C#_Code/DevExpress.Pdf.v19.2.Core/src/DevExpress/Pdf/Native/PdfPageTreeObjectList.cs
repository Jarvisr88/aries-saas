namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfPageTreeObjectList : PdfDeferredList<PdfPageTreeObject>
    {
        private readonly PdfObjectCollection objects;
        private readonly PdfPageTreeNode parent;

        public PdfPageTreeObjectList(IList<object> kids, PdfObjectCollection objects, PdfPageTreeNode parent) : base(kids.GetEnumerator(), kids.Count)
        {
            this.objects = objects;
            this.parent = parent;
        }

        private PdfPageTreeObject CreatePage(PdfReaderDictionary kidDictionary)
        {
            if (kidDictionary != null)
            {
                string name = kidDictionary.GetName("Type");
                if (name == null)
                {
                    return (kidDictionary.ContainsKey("Kids") ? ((PdfPageTreeObject) new PdfPageTreeNode(this.parent, kidDictionary)) : ((PdfPageTreeObject) new PdfPage(this.parent, kidDictionary)));
                }
                if (name == "Pages")
                {
                    return new PdfPageTreeNode(this.parent, kidDictionary);
                }
                if (name == "Page")
                {
                    return new PdfPage(this.parent, kidDictionary);
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return null;
        }

        protected override PdfPageTreeObject ParseObject(object value) => 
            this.objects.GetObject<PdfPageTreeObject>(value, new Func<PdfReaderDictionary, PdfPageTreeObject>(this.CreatePage));
    }
}

