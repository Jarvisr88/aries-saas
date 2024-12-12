namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfResourceDictionary
    {
        private readonly Dictionary<string, int> dictionary = new Dictionary<string, int>();
        private readonly PdfResources resources;
        private readonly string resourceKey;
        private readonly string prefix;
        private Lazy<PdfReaderDictionary> subDictionary;
        private int nextResourceNumber;

        public PdfResourceDictionary(PdfResources resources, string resourceKey, string prefix)
        {
            this.resources = resources;
            this.resourceKey = resourceKey;
            this.prefix = prefix;
            this.subDictionary = new Lazy<PdfReaderDictionary>(delegate {
                object obj2;
                PdfReaderDictionary dictionary = resources.Dictionary;
                return ((dictionary == null) || !dictionary.TryGetValue(resourceKey, out obj2)) ? null : (resources.DocumentCatalog.Objects.TryResolve(obj2, null) as PdfReaderDictionary);
            });
        }

        public string Add(int objectNumber)
        {
            string resourceName = this.GetResourceName(objectNumber);
            if (string.IsNullOrEmpty(resourceName))
            {
                resourceName = this.NextName;
                this.dictionary.Add(resourceName, objectNumber);
            }
            return resourceName;
        }

        public string Add<T>(T resource, bool forceAdding) where T: PdfObject
        {
            string resourceName = this.GetResourceName(resource.ObjectNumber);
            if (string.IsNullOrEmpty(resourceName))
            {
                resourceName = this.NextName;
                this.dictionary.Add(resourceName, this.resources.DocumentCatalog.Objects.AddResolvedObject(resource, forceAdding));
            }
            return resourceName;
        }

        public void Add(string resourceName, int objectNumber)
        {
            this.dictionary[resourceName] = objectNumber;
        }

        public Dictionary<string, string> Add<T>(ICollection<string> names, Guid objectsId, Func<string, T> getResource) where T: PdfObject
        {
            ICollection<string> is2 = this.Names;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            PdfObjectCollection objects = this.resources.DocumentCatalog.Objects;
            foreach (string str2 in names)
            {
                string resourceName;
                T local = getResource(str2);
                int lastObjectNumber = objects.LastObjectNumber;
                int objectNumber = objects.CloneObject(local, objectsId);
                if (objectNumber <= lastObjectNumber)
                {
                    resourceName = this.GetResourceName(objectNumber);
                    if (!string.IsNullOrEmpty(resourceName))
                    {
                        dictionary.Add(str2, resourceName);
                        continue;
                    }
                }
                resourceName = this.GenerateName(str2, is2);
                is2.Add(resourceName);
                this.dictionary.Add(resourceName, objectNumber);
                if (resourceName != str2)
                {
                    dictionary.Add(str2, resourceName);
                }
            }
            return dictionary;
        }

        public void Clear()
        {
            this.dictionary.Clear();
            this.nextResourceNumber = 0;
        }

        public void CopyTo(PdfResourceDictionary destinationDictionary)
        {
            Dictionary<string, int> dictionary = destinationDictionary.dictionary;
            foreach (KeyValuePair<string, int> pair in this.dictionary)
            {
                dictionary.Add(pair.Key, pair.Value);
            }
        }

        private string GenerateName(string name, ICollection<string> names)
        {
            while (names.Contains(name))
            {
                name = this.NextUncheckedName;
            }
            return name;
        }

        public T GetResource<T>(string resourceName, Func<PdfObjectCollection, object, T> create) where T: PdfObject
        {
            int num;
            PdfObjectCollection objects = this.resources.DocumentCatalog.Objects;
            if (!this.dictionary.TryGetValue(resourceName, out num))
            {
                object obj2;
                PdfReaderDictionary dictionary = this.subDictionary.Value;
                if ((dictionary == null) || !dictionary.TryGetValue(resourceName, out obj2))
                {
                    return default(T);
                }
                PdfObjectReference reference = obj2 as PdfObjectReference;
                num = (reference == null) ? objects.AddResolvedObject(create(objects, obj2)) : reference.Number;
                this.dictionary[resourceName] = num;
            }
            return objects.GetObject<T>(new PdfObjectReference(num), o => create(objects, o));
        }

        public string GetResourceName(int objectNumber)
        {
            string key;
            using (Dictionary<string, int>.Enumerator enumerator = this.dictionary.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<string, int> current = enumerator.Current;
                        if (current.Value != objectNumber)
                        {
                            continue;
                        }
                        key = current.Key;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return key;
        }

        public void ResolveResource<T>(string resourceKey, Func<string, T> create) where T: PdfObject
        {
            if (this.subDictionary.Value != null)
            {
                foreach (KeyValuePair<string, object> pair in this.subDictionary.Value)
                {
                    string key = pair.Key;
                    if (!this.dictionary.ContainsKey(key))
                    {
                        PdfObjectReference reference = pair.Value as PdfObjectReference;
                        if (reference == null)
                        {
                            create(key);
                            continue;
                        }
                        this.dictionary.Add(key, reference.Number);
                    }
                }
            }
            Func<PdfReaderDictionary> valueFactory = <>c__21<T>.<>9__21_0;
            if (<>c__21<T>.<>9__21_0 == null)
            {
                Func<PdfReaderDictionary> local1 = <>c__21<T>.<>9__21_0;
                valueFactory = <>c__21<T>.<>9__21_0 = (Func<PdfReaderDictionary>) (() => null);
            }
            this.subDictionary = new Lazy<PdfReaderDictionary>(valueFactory);
        }

        public void WriteResources<T>(PdfWriterDictionary writerDictionary, Func<string, T> getResource) where T: PdfObject
        {
            PdfObjectCollection objects = writerDictionary.Objects;
            foreach (KeyValuePair<string, int> pair in this.dictionary)
            {
                string resourceName = pair.Key;
                if (!writerDictionary.ContainsKey(resourceName))
                {
                    writerDictionary.Add(resourceName, objects.AddObjectToWrite(pair.Value, (Func<PdfObject>) (() => getResource(resourceName))));
                }
            }
        }

        public IEnumerable<int> ObjectNumbers =>
            this.dictionary.Values;

        public ICollection<string> Names
        {
            get
            {
                HashSet<string> set = new HashSet<string>(this.dictionary.Keys);
                PdfReaderDictionary dictionary = this.subDictionary.Value;
                if (dictionary != null)
                {
                    set.UnionWith(dictionary.Keys);
                }
                PdfResources parentResources = this.resources.ParentResources;
                if (parentResources != null)
                {
                    set.UnionWith(parentResources.GetResourceNames(this.resourceKey));
                }
                return set;
            }
        }

        private string NextUncheckedName
        {
            get
            {
                int nextResourceNumber = this.nextResourceNumber;
                this.nextResourceNumber = nextResourceNumber + 1;
                return (this.prefix + nextResourceNumber);
            }
        }

        private string NextName =>
            this.GenerateName(this.NextUncheckedName, this.Names);

        [Serializable, CompilerGenerated]
        private sealed class <>c__21<T> where T: PdfObject
        {
            public static readonly PdfResourceDictionary.<>c__21<T> <>9;
            public static Func<PdfReaderDictionary> <>9__21_0;

            static <>c__21()
            {
                PdfResourceDictionary.<>c__21<T>.<>9 = new PdfResourceDictionary.<>c__21<T>();
            }

            internal PdfReaderDictionary <ResolveResource>b__21_0() => 
                null;
        }
    }
}

