namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class PdfElementTreeNode<K, T> : IEnumerable<KeyValuePair<K, T>>, IEnumerable where T: class
    {
        protected PdfElementTreeNode()
        {
        }

        internal abstract IEnumerator<KeyValuePair<K, T>> GetEnumerator();
        internal static PdfDeferredSortedDictionary<K, T> Parse(PdfReaderDictionary dictionary, PdfCreateTreeKeyAction<K> createKey, PdfCreateTreeElementAction<T> createElement, string nodeName, bool checkElementCount)
        {
            object obj2;
            if ((dictionary == null) || (dictionary.Count == 0))
            {
                return null;
            }
            PdfObjectCollection objects = dictionary.Objects;
            IList<object> elements = null;
            if (dictionary.TryGetValue(nodeName, out obj2))
            {
                obj2 = objects.TryResolve(obj2, null);
                if (obj2 != null)
                {
                    elements = obj2 as IList<object>;
                    if (elements == null)
                    {
                        return null;
                    }
                }
            }
            IList<object> array = dictionary.GetArray("Kids");
            return ((elements != null) ? (((array != null) || (!checkElementCount && (elements.Count == 1))) ? null : new PdfElementTreeLeaf<K, T>(objects, elements, createKey, createElement).Value) : ((array != null) ? new PdfElementTreeBranch<K, T>(objects, array, createKey, createElement, nodeName).Value : null));
        }

        IEnumerator<KeyValuePair<K, T>> IEnumerable<KeyValuePair<K, T>>.GetEnumerator() => 
            this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        internal static PdfWriterDictionary Write(PdfObjectCollection objects, string key, PdfDeferredSortedDictionary<K, T> dictionary, PdfConvertToKeyTreeElementAction<K> convertToKeyAction, Func<PdfObjectCollection, T, object> writeAction)
        {
            if (dictionary == null)
            {
                return null;
            }
            List<object> enumerable = new List<object>();
            foreach (KeyValuePair<K, PdfDeferredItem<T>> pair in dictionary)
            {
                enumerable.Add(convertToKeyAction(pair.Key));
                if (writeAction == null)
                {
                    enumerable.Add(objects.AddObject(pair.Value.Item as PdfObject));
                    continue;
                }
                enumerable.Add(writeAction(objects, pair.Value.Item));
            }
            PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
            dictionary2.Add(key, new PdfWritableArray(enumerable));
            return dictionary2;
        }

        protected abstract PdfDeferredSortedDictionary<K, T> Value { get; }
    }
}

