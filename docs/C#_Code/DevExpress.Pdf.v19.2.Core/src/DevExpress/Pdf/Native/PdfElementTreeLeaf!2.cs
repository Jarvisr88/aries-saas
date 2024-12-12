namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfElementTreeLeaf<K, T> : PdfElementTreeNode<K, T> where T: class
    {
        private readonly PdfDeferredSortedDictionary<K, T> values;

        internal PdfElementTreeLeaf(PdfObjectCollection objects, IList<object> elements, PdfCreateTreeKeyAction<K> createKey, PdfCreateTreeElementAction<T> createElement)
        {
            this.values = new PdfDeferredSortedDictionary<K, T>();
            int num = elements.Count / 2;
            int num2 = 0;
            int num3 = 0;
            while (num2 < num)
            {
                K key = createKey(objects.TryResolve(elements[num3++], null));
                PdfDocumentCatalog catalog = objects.DocumentCatalog;
                if (catalog == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.values.AddDeferred(key, elements[num3++], v => createElement(catalog.Objects, v));
                num2++;
            }
        }

        internal override IEnumerator<KeyValuePair<K, T>> GetEnumerator() => 
            this.values.GetEnumerator();

        protected override PdfDeferredSortedDictionary<K, T> Value =>
            this.values;
    }
}

