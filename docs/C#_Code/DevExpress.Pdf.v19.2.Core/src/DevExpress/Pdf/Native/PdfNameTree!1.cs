namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PdfNameTree<T> : IDictionary<string, T>, ICollection<KeyValuePair<string, T>>, IEnumerable<KeyValuePair<string, T>>, IEnumerable where T: class
    {
        private static readonly PdfNameTreeEncoding encoding;
        private readonly IDictionary<string, PdfDeferredItem<T>> newItems;
        private IPdfNameTreeNode<T, T> rootNode;

        static PdfNameTree()
        {
            PdfNameTree<T>.encoding = new PdfNameTreeEncoding();
        }

        public PdfNameTree()
        {
            this.newItems = new SortedDictionary<string, PdfDeferredItem<T>>(StringComparer.Ordinal);
            this.rootNode = new LeafNode<T, T>(new SortedDictionary<string, PdfDeferredItem<T>>());
        }

        public PdfNameTree(PdfReaderDictionary root, PdfCreateTreeElementAction<T> createElement)
        {
            this.newItems = new SortedDictionary<string, PdfDeferredItem<T>>(StringComparer.Ordinal);
            if (root != null)
            {
                this.rootNode = PdfNameTree<T>.Parse<T>(root, createElement);
            }
            this.rootNode ??= new LeafNode<T, T>(new SortedDictionary<string, PdfDeferredItem<T>>());
        }

        public void Add(KeyValuePair<string, T> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Add(string key, T value)
        {
            this.Add(key, new PdfDeferredItem<T>(value));
        }

        private void Add(string key, PdfDeferredItem<T> value)
        {
            if (!this.rootNode.Contains(key) && !this.newItems.ContainsKey(key))
            {
                this.newItems.Add(key, value);
            }
        }

        public void AddDeferred(string key, object value, Func<object, T> create)
        {
            this.Add(key, new PdfDeferredItem<T>(value, create));
        }

        public void Clear()
        {
            this.rootNode = new LeafNode<T, T>(new SortedDictionary<string, PdfDeferredItem<T>>());
            this.newItems.Clear();
        }

        private static int Compare(string x, string y) => 
            string.CompareOrdinal(x, y);

        public bool Contains(KeyValuePair<string, T> item) => 
            this.RootNodeContains(item) || this.NewItemsContains(item);

        public bool ContainsKey(string key) => 
            !this.rootNode.Contains(key) ? this.newItems.ContainsKey(key) : true;

        private static object ConvertFromName(string value) => 
            PdfNameTree<T>.encoding.GetBytes(value);

        private static string ConvertToName(object key)
        {
            byte[] bytes = key as byte[];
            if (bytes != null)
            {
                return PdfNameTree<T>.encoding.GetString(bytes, 0, bytes.Length);
            }
            PdfName name = key as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return name.Name;
        }

        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            int num = arrayIndex;
            foreach (KeyValuePair<string, T> pair in this)
            {
                array[num++] = pair;
            }
        }

        [IteratorStateMachine(typeof(<GetEnumerator>d__33))]
        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            <GetEnumerator>d__33<T> d__1 = new <GetEnumerator>d__33<T>(0);
            d__1.<>4__this = (PdfNameTree<T>) this;
            return d__1;
        }

        private bool NewItemsContains(KeyValuePair<string, T> item)
        {
            PdfDeferredItem<T> item2;
            return (this.newItems.TryGetValue(item.Key, out item2) && EqualityComparer<T>.Default.Equals(item2.Item, item.Value));
        }

        private static IPdfNameTreeNode<T, VType> Parse<VType>(PdfReaderDictionary root, PdfCreateTreeElementAction<VType> createElement) where VType: class
        {
            IList<object> array = root.GetArray("Kids");
            if (array == null)
            {
                IList<object> list2 = root.GetArray("Names");
                if (list2 == null)
                {
                    return null;
                }
                SortedDictionary<string, PdfDeferredItem<VType>> dictionary2 = new SortedDictionary<string, PdfDeferredItem<VType>>(StringComparer.Ordinal);
                int num2 = 0;
                for (int i = 0; num2 < (list2.Count / 2); i++)
                {
                    string key = PdfNameTree<T>.ConvertToName(root.Objects.TryResolve(list2[i++], null));
                    PdfDocumentCatalog catalog = root.Objects.DocumentCatalog;
                    if (!dictionary2.ContainsKey(key))
                    {
                        dictionary2.Add(key, new PdfDeferredItem<VType>(list2[i], v => createElement(catalog.Objects, v)));
                    }
                    num2++;
                }
                return new LeafNode<T, VType>(dictionary2);
            }
            IntermediateNodeValue<T>[] values = new IntermediateNodeValue<T>[array.Count];
            int num = 0;
            foreach (object obj2 in array)
            {
                NodeLimits<T>? nullable;
                PdfReaderDictionary dictionary = root.Objects.TryResolve(obj2, null) as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                try
                {
                    IList<object> list3 = dictionary.GetArray("Limits");
                    nullable = new NodeLimits<T>(PdfNameTree<T>.ConvertToName(list3[0]), PdfNameTree<T>.ConvertToName(list3[1]));
                }
                catch
                {
                    nullable = null;
                }
                values[num++] = new IntermediateNodeValue<T>(nullable, dictionary);
            }
            return new IntermediateNode<T, VType>(values, createElement);
        }

        public bool Remove(KeyValuePair<string, T> item)
        {
            if (this.RootNodeContains(item))
            {
                return this.rootNode.Remove(item.Key);
            }
            if (!this.NewItemsContains(item))
            {
                return false;
            }
            this.newItems.Remove(item.Key);
            return true;
        }

        public bool Remove(string key) => 
            !this.rootNode.Remove(key) ? this.newItems.Remove(key) : true;

        public void RemoveAll(Func<T, bool> condition)
        {
            this.rootNode.RemoveAll(condition);
            foreach (KeyValuePair<string, PdfDeferredItem<T>> pair in this.newItems.ToArray<KeyValuePair<string, PdfDeferredItem<T>>>())
            {
                if (condition(pair.Value.Item))
                {
                    this.newItems.Remove(pair.Key);
                }
            }
        }

        private bool RootNodeContains(KeyValuePair<string, T> item)
        {
            T local;
            return (this.rootNode.TryGetValue(item.Key, out local) && EqualityComparer<T>.Default.Equals(local, item.Value));
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public bool TryGetValue(string key, out T value)
        {
            if (!this.rootNode.TryGetValue(key, out value))
            {
                PdfDeferredItem<T> item;
                if (!this.newItems.TryGetValue(key, out item))
                {
                    return false;
                }
                value = item.Item;
            }
            return true;
        }

        public object Write(PdfObjectCollection collection)
        {
            if (this.newItems.Count == 0)
            {
                NameTreeNodeWritingResult<T> result1 = this.rootNode.Write(collection, true);
                if (result1 != null)
                {
                    return result1.Dictionary;
                }
                NameTreeNodeWritingResult<T> local1 = result1;
                return null;
            }
            List<object> enumerable = new List<object>();
            foreach (KeyValuePair<string, T> pair in this)
            {
                enumerable.Add(PdfNameTree<T>.ConvertFromName(pair.Key));
                enumerable.Add(collection.AddObject(pair.Value as PdfObject));
            }
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("Names", new PdfWritableArray(enumerable));
            return dictionary;
        }

        public T this[string key]
        {
            get
            {
                T local;
                return (!this.rootNode.TryGetValue(key, out local) ? this.newItems[key].Item : local);
            }
            set
            {
                if (!this.rootNode.TryUpdate(key, value))
                {
                    this.newItems[key] = new PdfDeferredItem<T>(value);
                }
            }
        }

        public ICollection<string> Keys =>
            this.rootNode.GetKeys().Concat<string>(this.newItems.Keys).ToArray<string>();

        public ICollection<T> Values
        {
            get
            {
                Func<KeyValuePair<string, T>, T> selector = <>c<T>.<>9__19_0;
                if (<>c<T>.<>9__19_0 == null)
                {
                    Func<KeyValuePair<string, T>, T> local1 = <>c<T>.<>9__19_0;
                    selector = <>c<T>.<>9__19_0 = v => v.Value;
                }
                return this.Select<KeyValuePair<string, T>, T>(selector).ToArray<T>();
            }
        }

        public int Count =>
            this.newItems.Count + this.rootNode.Count;

        public bool IsReadOnly =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfNameTree<T>.<>c <>9;
            public static Func<KeyValuePair<string, T>, T> <>9__19_0;

            static <>c()
            {
                PdfNameTree<T>.<>c.<>9 = new PdfNameTree<T>.<>c();
            }

            internal T <get_Values>b__19_0(KeyValuePair<string, T> v) => 
                v.Value;
        }

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__33 : IEnumerator<KeyValuePair<string, T>>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private KeyValuePair<string, T> <>2__current;
            public PdfNameTree<T> <>4__this;
            private bool <hasValue>5__1;
            private IEnumerator<KeyValuePair<string, PdfDeferredItem<T>>> <newItemEnumerator>5__2;
            private KeyValuePair<string, T> <item>5__3;
            private IEnumerator<KeyValuePair<string, T>> <>7__wrap1;

            [DebuggerHidden]
            public <GetEnumerator>d__33(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    KeyValuePair<string, PdfDeferredItem<T>> current;
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<newItemEnumerator>5__2 = this.<>4__this.newItems.GetEnumerator();
                            this.<hasValue>5__1 = this.<newItemEnumerator>5__2.MoveNext();
                            this.<>7__wrap1 = this.<>4__this.rootNode.GetEnumerator();
                            this.<>1__state = -3;
                            goto TR_000D;

                        case 1:
                            this.<>1__state = -3;
                            this.<hasValue>5__1 = this.<newItemEnumerator>5__2.MoveNext();
                            current = this.<newItemEnumerator>5__2.Current;
                            goto TR_0009;

                        case 2:
                            this.<>1__state = -3;
                            this.<item>5__3 = new KeyValuePair<string, T>();
                            goto TR_000D;

                        case 3:
                            this.<>1__state = -1;
                            this.<hasValue>5__1 = this.<newItemEnumerator>5__2.MoveNext();
                            goto TR_0005;

                        default:
                            flag = false;
                            break;
                    }
                    return flag;
                TR_0005:
                    if (!this.<hasValue>5__1)
                    {
                        flag = false;
                    }
                    else
                    {
                        this.<>2__current = new KeyValuePair<string, T>(this.<newItemEnumerator>5__2.Current.Key, this.<newItemEnumerator>5__2.Current.Value.Item);
                        this.<>1__state = 3;
                        flag = true;
                    }
                    return flag;
                TR_0008:
                    this.<>2__current = this.<item>5__3;
                    this.<>1__state = 2;
                    return true;
                TR_0009:
                    if (this.<hasValue>5__1 && (PdfNameTree<T>.Compare(current.Key, this.<item>5__3.Key) < 0))
                    {
                        this.<>2__current = new KeyValuePair<string, T>(current.Key, current.Value.Item);
                        this.<>1__state = 1;
                        return true;
                    }
                    goto TR_0008;
                TR_000D:
                    if (this.<>7__wrap1.MoveNext())
                    {
                        this.<item>5__3 = this.<>7__wrap1.Current;
                        if (!this.<hasValue>5__1)
                        {
                            goto TR_0008;
                        }
                        else
                        {
                            current = this.<newItemEnumerator>5__2.Current;
                        }
                    }
                    else
                    {
                        this.<>m__Finally1();
                        this.<>7__wrap1 = null;
                        goto TR_0005;
                    }
                    goto TR_0009;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if (((num == -3) || (num == 1)) || (num == 2))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            KeyValuePair<string, T> IEnumerator<KeyValuePair<string, T>>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private class IntermediateNode<VType> : PdfNameTree<T>.IPdfNameTreeNode<VType> where VType: class
        {
            private readonly PdfNameTree<T>.IntermediateNodeValue[] values;
            private readonly PdfNameTree<T>.IPdfNameTreeNode<VType>[] parsedNodes;
            private readonly PdfCreateTreeElementAction<VType> createElement;

            public IntermediateNode(PdfNameTree<T>.IntermediateNodeValue[] values, PdfCreateTreeElementAction<VType> createElement)
            {
                this.values = values;
                this.createElement = createElement;
                this.parsedNodes = new PdfNameTree<T>.IPdfNameTreeNode<VType>[values.Length];
            }

            public bool Contains(string key)
            {
                bool flag;
                using (IEnumerator<PdfNameTree<T>.IPdfNameTreeNode<VType>> enumerator = this.GetNodes(key).GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            PdfNameTree<T>.IPdfNameTreeNode<VType> current = enumerator.Current;
                            if (!current.Contains(key))
                            {
                                continue;
                            }
                            flag = true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                }
                return flag;
            }

            [IteratorStateMachine(typeof(<GetEnumerator>d__10<,>))]
            public IEnumerator<KeyValuePair<string, VType>> GetEnumerator()
            {
                <GetEnumerator>d__10<T, VType> d__1 = new <GetEnumerator>d__10<T, VType>(0);
                d__1.<>4__this = (PdfNameTree<T>.IntermediateNode<VType>) this;
                return d__1;
            }

            [IteratorStateMachine(typeof(<GetKeys>d__12<,>))]
            public IEnumerable<string> GetKeys()
            {
                <GetKeys>d__12<T, VType> d__1 = new <GetKeys>d__12<T, VType>(-2);
                d__1.<>4__this = (PdfNameTree<T>.IntermediateNode<VType>) this;
                return d__1;
            }

            public unsafe PdfNameTree<T>.NodeLimits? GetLimits()
            {
                string text1;
                string text2;
                PdfNameTree<T>.NodeLimits?* nullablePtr1 = &this.GetLimits(0);
                if (nullablePtr1 != null)
                {
                    text1 = nullablePtr1.GetValueOrDefault().LowLimit;
                }
                else
                {
                    PdfNameTree<T>.NodeLimits?* local1 = nullablePtr1;
                    text1 = null;
                }
                string lowLimit = text1;
                PdfNameTree<T>.NodeLimits? limits = this.GetLimits(this.values.Length - 1);
                PdfNameTree<T>.NodeLimits?* nullablePtr2 = &limits;
                if (nullablePtr2 != null)
                {
                    text2 = nullablePtr2.GetValueOrDefault().HighLimit;
                }
                else
                {
                    PdfNameTree<T>.NodeLimits?* local2 = nullablePtr2;
                    text2 = null;
                }
                string highLimit = text2;
                if ((lowLimit != null) && (highLimit != null))
                {
                    return new PdfNameTree<T>.NodeLimits(lowLimit, highLimit);
                }
                return null;
            }

            private PdfNameTree<T>.NodeLimits? GetLimits(int nodeIndex)
            {
                PdfNameTree<T>.IntermediateNodeValue value2 = this.values[nodeIndex];
                if (value2.Limits != null)
                {
                    return new PdfNameTree<T>.NodeLimits?(value2.Limits.Value);
                }
                PdfNameTree<T>.NodeLimits? limits = this.GetParsedNode(nodeIndex).GetLimits();
                this.values[nodeIndex] = new PdfNameTree<T>.IntermediateNodeValue(limits, value2.Dictionary);
                return limits;
            }

            [IteratorStateMachine(typeof(<GetNodes>d__17<,>))]
            private IEnumerable<PdfNameTree<T>.IPdfNameTreeNode<VType>> GetNodes(string key)
            {
                <GetNodes>d__17<T, VType> d__1 = new <GetNodes>d__17<T, VType>(-2);
                d__1.<>4__this = (PdfNameTree<T>.IntermediateNode<VType>) this;
                d__1.<>3__key = key;
                return d__1;
            }

            private PdfNameTree<T>.IPdfNameTreeNode<VType> GetParsedNode(int index)
            {
                this.parsedNodes[index] ??= PdfNameTree<T>.Parse<VType>(this.values[index].Dictionary, this.createElement);
                return this.parsedNodes[index];
            }

            public bool Remove(string key)
            {
                bool flag;
                using (IEnumerator<PdfNameTree<T>.IPdfNameTreeNode<VType>> enumerator = this.GetNodes(key).GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            PdfNameTree<T>.IPdfNameTreeNode<VType> current = enumerator.Current;
                            if (!current.Remove(key))
                            {
                                continue;
                            }
                            flag = true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                }
                return flag;
            }

            public void RemoveAll(Func<VType, bool> condition)
            {
                for (int i = 0; i < this.values.Length; i++)
                {
                    PdfNameTree<T>.IPdfNameTreeNode<VType> parsedNode = this.GetParsedNode(i);
                    if (parsedNode != null)
                    {
                        parsedNode.RemoveAll(condition);
                    }
                }
            }

            public bool TryGetValue(string key, out VType value)
            {
                bool flag;
                using (IEnumerator<PdfNameTree<T>.IPdfNameTreeNode<VType>> enumerator = this.GetNodes(key).GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            PdfNameTree<T>.IPdfNameTreeNode<VType> current = enumerator.Current;
                            if (!current.TryGetValue(key, out value))
                            {
                                continue;
                            }
                            flag = true;
                        }
                        else
                        {
                            value = default(VType);
                            return false;
                        }
                        break;
                    }
                }
                return flag;
            }

            public bool TryUpdate(string key, VType value)
            {
                bool flag;
                using (IEnumerator<PdfNameTree<T>.IPdfNameTreeNode<VType>> enumerator = this.GetNodes(key).GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            PdfNameTree<T>.IPdfNameTreeNode<VType> current = enumerator.Current;
                            if (!current.TryUpdate(key, value))
                            {
                                continue;
                            }
                            flag = true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                }
                return flag;
            }

            public PdfNameTree<T>.NameTreeNodeWritingResult Write(PdfObjectCollection collection, bool isRoot)
            {
                if (this.Count == 0)
                {
                    return null;
                }
                List<object> enumerable = new List<object>();
                string x = null;
                string highLimit = null;
                for (int i = 0; i < this.values.Length; i++)
                {
                    PdfNameTree<T>.NameTreeNodeWritingResult result1;
                    PdfNameTree<T>.IPdfNameTreeNode<VType> parsedNode = this.GetParsedNode(i);
                    if (parsedNode != null)
                    {
                        result1 = parsedNode.Write(collection, false);
                    }
                    else
                    {
                        PdfNameTree<T>.IPdfNameTreeNode<VType> local1 = parsedNode;
                        result1 = null;
                    }
                    PdfNameTree<T>.NameTreeNodeWritingResult result = result1;
                    if (result != null)
                    {
                        if ((x == null) || (PdfNameTree<T>.Compare(x, result.LowLimit) > 0))
                        {
                            x = result.LowLimit;
                        }
                        if ((highLimit == null) || (PdfNameTree<T>.Compare(highLimit, result.HighLimit) < 0))
                        {
                            highLimit = result.HighLimit;
                        }
                        enumerable.Add(result.Dictionary);
                    }
                }
                PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
                dictionary.Add("Kids", new PdfWritableArray(enumerable));
                if (!isRoot)
                {
                    object[] objArray1 = new object[] { PdfNameTree<T>.ConvertFromName(x), PdfNameTree<T>.ConvertFromName(highLimit) };
                    dictionary.Add("Limits", new PdfWritableArray(objArray1));
                }
                return new PdfNameTree<T>.NameTreeNodeWritingResult(x, highLimit, dictionary);
            }

            public int Count
            {
                get
                {
                    int num = 0;
                    for (int i = 0; i < this.values.Length; i++)
                    {
                        PdfNameTree<T>.IPdfNameTreeNode<VType> parsedNode = this.GetParsedNode(i);
                        if (parsedNode != null)
                        {
                            num += parsedNode.Count;
                        }
                    }
                    return num;
                }
            }

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__10 : IEnumerator<KeyValuePair<string, VType>>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private KeyValuePair<string, VType> <>2__current;
                public PdfNameTree<T>.IntermediateNode<VType> <>4__this;
                private int <i>5__1;
                private IEnumerator<KeyValuePair<string, VType>> <>7__wrap1;

                [DebuggerHidden]
                public <GetEnumerator>d__10(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    if (this.<>7__wrap1 != null)
                    {
                        this.<>7__wrap1.Dispose();
                    }
                }

                private bool MoveNext()
                {
                    bool flag;
                    try
                    {
                        int num = this.<>1__state;
                        if (num == 0)
                        {
                            this.<>1__state = -1;
                            this.<i>5__1 = 0;
                        }
                        else
                        {
                            if (num == 1)
                            {
                                this.<>1__state = -3;
                            }
                            else
                            {
                                return false;
                            }
                            goto TR_000D;
                        }
                    TR_0007:
                        if (this.<i>5__1 < this.<>4__this.values.Length)
                        {
                            PdfNameTree<T>.IPdfNameTreeNode<VType> parsedNode = this.<>4__this.GetParsedNode(this.<i>5__1);
                            if (parsedNode == null)
                            {
                                goto TR_0009;
                            }
                            else
                            {
                                this.<>7__wrap1 = parsedNode.GetEnumerator();
                                this.<>1__state = -3;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        goto TR_000D;
                    TR_0009:
                        while (true)
                        {
                            int num2 = this.<i>5__1;
                            this.<i>5__1 = num2 + 1;
                            break;
                        }
                        goto TR_0007;
                    TR_000D:
                        while (true)
                        {
                            if (this.<>7__wrap1.MoveNext())
                            {
                                KeyValuePair<string, VType> current = this.<>7__wrap1.Current;
                                this.<>2__current = current;
                                this.<>1__state = 1;
                                flag = true;
                            }
                            else
                            {
                                this.<>m__Finally1();
                                this.<>7__wrap1 = null;
                                goto TR_0009;
                            }
                            break;
                        }
                    }
                    fault
                    {
                        this.System.IDisposable.Dispose();
                    }
                    return flag;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = this.<>1__state;
                    if ((num == -3) || (num == 1))
                    {
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                    }
                }

                KeyValuePair<string, VType> IEnumerator<KeyValuePair<string, VType>>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }

            [CompilerGenerated]
            private sealed class <GetKeys>d__12 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private string <>2__current;
                private int <>l__initialThreadId;
                public PdfNameTree<T>.IntermediateNode<VType> <>4__this;
                private int <i>5__1;
                private IEnumerator<string> <>7__wrap1;

                [DebuggerHidden]
                public <GetKeys>d__12(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                    this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    if (this.<>7__wrap1 != null)
                    {
                        this.<>7__wrap1.Dispose();
                    }
                }

                private bool MoveNext()
                {
                    bool flag;
                    try
                    {
                        int num = this.<>1__state;
                        if (num == 0)
                        {
                            this.<>1__state = -1;
                            this.<i>5__1 = 0;
                        }
                        else
                        {
                            if (num == 1)
                            {
                                this.<>1__state = -3;
                            }
                            else
                            {
                                return false;
                            }
                            goto TR_000D;
                        }
                    TR_0007:
                        if (this.<i>5__1 < this.<>4__this.values.Length)
                        {
                            PdfNameTree<T>.IPdfNameTreeNode<VType> parsedNode = this.<>4__this.GetParsedNode(this.<i>5__1);
                            if (parsedNode == null)
                            {
                                goto TR_0009;
                            }
                            else
                            {
                                this.<>7__wrap1 = parsedNode.GetKeys().GetEnumerator();
                                this.<>1__state = -3;
                            }
                        }
                        else
                        {
                            return false;
                        }
                        goto TR_000D;
                    TR_0009:
                        while (true)
                        {
                            int num2 = this.<i>5__1;
                            this.<i>5__1 = num2 + 1;
                            break;
                        }
                        goto TR_0007;
                    TR_000D:
                        while (true)
                        {
                            if (this.<>7__wrap1.MoveNext())
                            {
                                string current = this.<>7__wrap1.Current;
                                this.<>2__current = current;
                                this.<>1__state = 1;
                                flag = true;
                            }
                            else
                            {
                                this.<>m__Finally1();
                                this.<>7__wrap1 = null;
                                goto TR_0009;
                            }
                            break;
                        }
                    }
                    fault
                    {
                        this.System.IDisposable.Dispose();
                    }
                    return flag;
                }

                [DebuggerHidden]
                IEnumerator<string> IEnumerable<string>.GetEnumerator()
                {
                    PdfNameTree<T>.IntermediateNode<VType>.<GetKeys>d__12 d__;
                    if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                    {
                        this.<>1__state = 0;
                        d__ = (PdfNameTree<T>.IntermediateNode<VType>.<GetKeys>d__12) this;
                    }
                    else
                    {
                        d__ = new PdfNameTree<T>.IntermediateNode<VType>.<GetKeys>d__12(0) {
                            <>4__this = this.<>4__this
                        };
                    }
                    return d__;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator() => 
                    this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = this.<>1__state;
                    if ((num == -3) || (num == 1))
                    {
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                    }
                }

                string IEnumerator<string>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }

            [CompilerGenerated]
            private sealed class <GetNodes>d__17 : IEnumerable<PdfNameTree<T>.IPdfNameTreeNode<VType>>, IEnumerable, IEnumerator<PdfNameTree<T>.IPdfNameTreeNode<VType>>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private PdfNameTree<T>.IPdfNameTreeNode<VType> <>2__current;
                private int <>l__initialThreadId;
                public PdfNameTree<T>.IntermediateNode<VType> <>4__this;
                private string key;
                public string <>3__key;
                private int <i>5__1;

                [DebuggerHidden]
                public <GetNodes>d__17(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                    this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
                }

                private bool MoveNext()
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<i>5__1 = 0;
                    }
                    else
                    {
                        if (num != 1)
                        {
                            return false;
                        }
                        this.<>1__state = -1;
                        goto TR_0009;
                    }
                TR_0007:
                    if (this.<i>5__1 >= this.<>4__this.values.Length)
                    {
                        return false;
                    }
                    PdfNameTree<T>.NodeLimits? limits = this.<>4__this.GetLimits(this.<i>5__1);
                    if ((limits != null) && ((PdfNameTree<T>.Compare(this.key, limits.Value.LowLimit) >= 0) && (PdfNameTree<T>.Compare(this.key, limits.Value.HighLimit) <= 0)))
                    {
                        PdfNameTree<T>.IPdfNameTreeNode<VType> parsedNode = this.<>4__this.GetParsedNode(this.<i>5__1);
                        if (parsedNode != null)
                        {
                            this.<>2__current = parsedNode;
                            this.<>1__state = 1;
                            return true;
                        }
                    }
                TR_0009:
                    while (true)
                    {
                        int num2 = this.<i>5__1;
                        this.<i>5__1 = num2 + 1;
                        break;
                    }
                    goto TR_0007;
                }

                [DebuggerHidden]
                IEnumerator<PdfNameTree<T>.IPdfNameTreeNode<VType>> IEnumerable<PdfNameTree<T>.IPdfNameTreeNode<VType>>.GetEnumerator()
                {
                    PdfNameTree<T>.IntermediateNode<VType>.<GetNodes>d__17 d__;
                    if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                    {
                        this.<>1__state = 0;
                        d__ = (PdfNameTree<T>.IntermediateNode<VType>.<GetNodes>d__17) this;
                    }
                    else
                    {
                        d__ = new PdfNameTree<T>.IntermediateNode<VType>.<GetNodes>d__17(0) {
                            <>4__this = this.<>4__this
                        };
                    }
                    d__.key = this.<>3__key;
                    return d__;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator() => 
                    this.System.Collections.Generic.IEnumerable<DevExpress.Pdf.Native.PdfNameTree<T>.IPdfNameTreeNode<VType>>.GetEnumerator();

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                }

                PdfNameTree<T>.IPdfNameTreeNode<VType> IEnumerator<PdfNameTree<T>.IPdfNameTreeNode<VType>>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IntermediateNodeValue
        {
            public PdfNameTree<T>.NodeLimits? Limits { get; }
            public PdfReaderDictionary Dictionary { get; }
            public IntermediateNodeValue(PdfNameTree<T>.NodeLimits? limits, PdfReaderDictionary dictionary)
            {
                this.<Limits>k__BackingField = limits;
                this.<Dictionary>k__BackingField = dictionary;
            }
        }

        private interface IPdfNameTreeNode<VType> where VType: class
        {
            bool Contains(string key);
            IEnumerator<KeyValuePair<string, VType>> GetEnumerator();
            IEnumerable<string> GetKeys();
            PdfNameTree<T>.NodeLimits? GetLimits();
            bool Remove(string key);
            void RemoveAll(Func<VType, bool> condition);
            bool TryGetValue(string key, out VType value);
            bool TryUpdate(string key, VType value);
            PdfNameTree<T>.NameTreeNodeWritingResult Write(PdfObjectCollection collection, bool isRoot);

            int Count { get; }
        }

        private class LeafNode<VType> : PdfNameTree<T>.IPdfNameTreeNode<VType> where VType: class
        {
            private readonly SortedDictionary<string, PdfDeferredItem<VType>> values;

            public LeafNode(SortedDictionary<string, PdfDeferredItem<VType>> values)
            {
                this.values = values;
            }

            public bool Contains(string key) => 
                this.values.ContainsKey(key);

            [IteratorStateMachine(typeof(<GetEnumerator>d__8<,>))]
            public IEnumerator<KeyValuePair<string, VType>> GetEnumerator()
            {
                <GetEnumerator>d__8<T, VType> d__1 = new <GetEnumerator>d__8<T, VType>(0);
                d__1.<>4__this = (PdfNameTree<T>.LeafNode<VType>) this;
                return d__1;
            }

            public IEnumerable<string> GetKeys() => 
                this.values.Keys;

            public PdfNameTree<T>.NodeLimits? GetLimits()
            {
                if (this.values.Count == 0)
                {
                    return null;
                }
                SortedDictionary<string, PdfDeferredItem<VType>>.KeyCollection source = this.values.Keys;
                return new PdfNameTree<T>.NodeLimits(source.First<string>(), source.Last<string>());
            }

            public bool Remove(string key) => 
                this.values.Remove(key);

            public void RemoveAll(Func<VType, bool> condition)
            {
                foreach (KeyValuePair<string, PdfDeferredItem<VType>> pair in this.values.ToArray<KeyValuePair<string, PdfDeferredItem<VType>>>())
                {
                    if (condition(pair.Value.Item))
                    {
                        this.values.Remove(pair.Key);
                    }
                }
            }

            public bool TryGetValue(string key, out VType value)
            {
                PdfDeferredItem<VType> item;
                if (this.values.TryGetValue(key, out item))
                {
                    value = item.Item;
                    return true;
                }
                value = default(VType);
                return false;
            }

            public bool TryUpdate(string key, VType value)
            {
                if (!this.values.ContainsKey(key))
                {
                    return false;
                }
                this.values[key] = new PdfDeferredItem<VType>(value);
                return true;
            }

            public PdfNameTree<T>.NameTreeNodeWritingResult Write(PdfObjectCollection collection, bool isRoot)
            {
                if (this.values.Count == 0)
                {
                    return null;
                }
                List<object> enumerable = new List<object>();
                string x = null;
                string str2 = null;
                foreach (KeyValuePair<string, PdfDeferredItem<VType>> pair in this.values)
                {
                    string key = pair.Key;
                    if ((x == null) || (PdfNameTree<T>.Compare(x, key) > 0))
                    {
                        x = key;
                    }
                    if ((str2 == null) || (PdfNameTree<T>.Compare(str2, key) < 0))
                    {
                        str2 = key;
                    }
                    enumerable.Add(PdfNameTree<T>.ConvertFromName(key));
                    enumerable.Add(collection.AddObject(pair.Value.Item as PdfObject));
                }
                PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
                dictionary.Add("Names", new PdfWritableArray(enumerable));
                if (!isRoot)
                {
                    object[] objArray1 = new object[] { PdfNameTree<T>.ConvertFromName(x), PdfNameTree<T>.ConvertFromName(str2) };
                    dictionary.Add("Limits", new PdfWritableArray(objArray1));
                }
                return new PdfNameTree<T>.NameTreeNodeWritingResult(x, str2, dictionary);
            }

            public int Count =>
                this.values.Count;

            [CompilerGenerated]
            private sealed class <GetEnumerator>d__8 : IEnumerator<KeyValuePair<string, VType>>, IDisposable, IEnumerator
            {
                private int <>1__state;
                private KeyValuePair<string, VType> <>2__current;
                public PdfNameTree<T>.LeafNode<VType> <>4__this;
                private SortedDictionary<string, PdfDeferredItem<VType>>.Enumerator <>7__wrap1;

                [DebuggerHidden]
                public <GetEnumerator>d__8(int <>1__state)
                {
                    this.<>1__state = <>1__state;
                }

                private void <>m__Finally1()
                {
                    this.<>1__state = -1;
                    this.<>7__wrap1.Dispose();
                }

                private bool MoveNext()
                {
                    bool flag;
                    try
                    {
                        int num = this.<>1__state;
                        if (num == 0)
                        {
                            this.<>1__state = -1;
                            this.<>7__wrap1 = this.<>4__this.values.GetEnumerator();
                            this.<>1__state = -3;
                        }
                        else if (num == 1)
                        {
                            this.<>1__state = -3;
                        }
                        else
                        {
                            return false;
                        }
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = new SortedDictionary<string, PdfDeferredItem<VType>>.Enumerator();
                            flag = false;
                        }
                        else
                        {
                            KeyValuePair<string, PdfDeferredItem<VType>> current = this.<>7__wrap1.Current;
                            this.<>2__current = new KeyValuePair<string, VType>(current.Key, current.Value.Item);
                            this.<>1__state = 1;
                            flag = true;
                        }
                    }
                    fault
                    {
                        this.System.IDisposable.Dispose();
                    }
                    return flag;
                }

                [DebuggerHidden]
                void IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                void IDisposable.Dispose()
                {
                    int num = this.<>1__state;
                    if ((num == -3) || (num == 1))
                    {
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally1();
                        }
                    }
                }

                KeyValuePair<string, VType> IEnumerator<KeyValuePair<string, VType>>.Current =>
                    this.<>2__current;

                object IEnumerator.Current =>
                    this.<>2__current;
            }
        }

        private class NameTreeNodeWritingResult
        {
            public NameTreeNodeWritingResult(string lowLimit, string highLimit, PdfWriterDictionary dictionary)
            {
                this.<LowLimit>k__BackingField = lowLimit;
                this.<HighLimit>k__BackingField = highLimit;
                this.<Dictionary>k__BackingField = dictionary;
            }

            public string LowLimit { get; }

            public string HighLimit { get; }

            public PdfWriterDictionary Dictionary { get; }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NodeLimits
        {
            public string LowLimit { get; }
            public string HighLimit { get; }
            public NodeLimits(string lowLimit, string highLimit)
            {
                this.<LowLimit>k__BackingField = lowLimit;
                this.<HighLimit>k__BackingField = highLimit;
            }
        }
    }
}

