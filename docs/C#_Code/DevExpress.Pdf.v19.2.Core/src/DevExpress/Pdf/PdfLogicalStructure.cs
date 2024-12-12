namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfLogicalStructure : PdfLogicalStructureEntry
    {
        private const string dictionaryType = "StructTreeRoot";
        private const string idTreeDictionaryKey = "IDTree";
        private const string parentTreeDictionaryKey = "ParentTree";
        private const string roleMapDictionaryKey = "RoleMap";
        private const string attributeClassMapDictionaryKey = "ClassMap";
        private readonly IDictionary<string, string> roleMap;
        private readonly IDictionary<string, PdfLogicalStructureElementAttribute[]> attributeClassMap;
        private PdfDeferredSortedDictionary<string, PdfLogicalStructureItem> elements;
        private PdfDeferredSortedDictionary<int, PdfLogicalStructureElementList> parents;
        private PdfReaderDictionary idTreeDictionary;
        private PdfReaderDictionary parentTreeDictionary;

        internal PdfLogicalStructure(PdfReaderDictionary dictionary) : base(null, dictionary)
        {
            this.idTreeDictionary = dictionary.GetDictionary("IDTree");
            this.parentTreeDictionary = dictionary.GetDictionary("ParentTree");
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("RoleMap");
            if (dictionary2 != null)
            {
                this.roleMap = new Dictionary<string, string>();
                foreach (KeyValuePair<string, object> pair in dictionary2)
                {
                    PdfName name = pair.Value as PdfName;
                    if (name != null)
                    {
                        this.roleMap.Add(pair.Key, name.Name);
                    }
                }
            }
            PdfReaderDictionary dictionary3 = dictionary.GetDictionary("ClassMap");
            if (dictionary3 != null)
            {
                PdfObjectCollection objects = dictionary.Objects;
                this.attributeClassMap = new Dictionary<string, PdfLogicalStructureElementAttribute[]>();
                foreach (KeyValuePair<string, object> pair2 in dictionary3)
                {
                    string key = pair2.Key;
                    object obj2 = pair2.Value;
                    if ((key != "Type") || !(obj2 is PdfName))
                    {
                        this.attributeClassMap.Add(key, PdfLogicalStructureElementAttribute.Parse(objects, obj2));
                    }
                }
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            this.Resolve();
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Type", new PdfName("StructTreeRoot"));
            base.WriteKids(dictionary, objects);
            dictionary.AddIfPresent("IDTree", PdfNameTreeNode<PdfLogicalStructureItem>.Write(objects, this.elements));
            Func<PdfObjectCollection, PdfLogicalStructureElementList, object> writeAction = <>c.<>9__23_0;
            if (<>c.<>9__23_0 == null)
            {
                Func<PdfObjectCollection, PdfLogicalStructureElementList, object> local1 = <>c.<>9__23_0;
                writeAction = <>c.<>9__23_0 = (collection, value) => WriteParents(collection, value);
            }
            dictionary.AddIfPresent("ParentTree", PdfNumberTreeNode<PdfLogicalStructureElementList>.Write(objects, this.parents, writeAction));
            if (this.roleMap != null)
            {
                PdfWriterDictionary dictionary2 = new PdfWriterDictionary(null);
                foreach (KeyValuePair<string, string> pair in this.roleMap)
                {
                    dictionary2.AddName(pair.Key, pair.Value);
                }
                dictionary.Add("RoleMap", dictionary2);
            }
            if (this.attributeClassMap != null)
            {
                PdfWriterDictionary dictionary3 = new PdfWriterDictionary(objects);
                foreach (KeyValuePair<string, PdfLogicalStructureElementAttribute[]> pair2 in this.attributeClassMap)
                {
                    PdfLogicalStructureElementAttribute[] attributeArray = pair2.Value;
                    if (attributeArray == null)
                    {
                        dictionary3.Add(pair2.Key, null);
                        continue;
                    }
                    if (attributeArray.Length == 1)
                    {
                        dictionary3.Add(pair2.Key, attributeArray[0]);
                        continue;
                    }
                    dictionary3.Add(pair2.Key, new PdfWritableObjectArray(attributeArray, objects));
                }
                dictionary.Add("ClassMap", dictionary3);
            }
            return dictionary;
        }

        private PdfLogicalStructureElementList GetParents(PdfObjectCollection collection, object value)
        {
            PdfLogicalStructureElementList list = new PdfLogicalStructureElementList();
            object obj2 = collection.TryResolve(value, null);
            PdfReaderDictionary dictionary = obj2 as PdfReaderDictionary;
            if (dictionary != null)
            {
                PdfLogicalStructureElement resolvedObject = collection.GetResolvedObject<PdfLogicalStructureElement>(dictionary.Number);
                if (resolvedObject != null)
                {
                    list.Add(resolvedObject);
                }
            }
            IList<object> list2 = obj2 as IList<object>;
            if (list2 != null)
            {
                foreach (object obj3 in list2)
                {
                    if (obj3 != null)
                    {
                        PdfObjectReference reference = obj3 as PdfObjectReference;
                        PdfLogicalStructureElement item = (reference != null) ? collection.GetResolvedObject<PdfLogicalStructureElement>(reference.Number) : null;
                        if (item != null)
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        protected internal override void Resolve()
        {
            base.Resolve();
            if (this.idTreeDictionary != null)
            {
                this.elements = PdfNameTreeNode<PdfLogicalStructureItem>.Parse(this.idTreeDictionary, (collection, value) => collection.GetLogicalStructureItem(this, this, value));
                if (this.elements != null)
                {
                    this.elements.ResolveAll();
                }
                this.idTreeDictionary = null;
            }
            if (this.parentTreeDictionary != null)
            {
                this.parents = PdfNumberTreeNode<PdfLogicalStructureElementList>.Parse(this.parentTreeDictionary, (collection, value) => this.GetParents(collection, value), false);
                if (this.parents != null)
                {
                    this.parents.ResolveAll();
                }
                this.parentTreeDictionary = null;
            }
        }

        private static object WriteParents(PdfObjectCollection collection, List<PdfLogicalStructureElement> parents) => 
            (parents.Count != 1) ? ((object) new PdfWritableObjectArray((IEnumerable<PdfObject>) parents, collection)) : ((object) collection.AddObject((PdfObject) parents[0]));

        internal IDictionary<string, PdfLogicalStructureItem> Elements
        {
            get
            {
                this.Resolve();
                return this.elements;
            }
        }

        public IDictionary<int, PdfLogicalStructureElementList> Parents
        {
            get
            {
                this.Resolve();
                return this.parents;
            }
        }

        public IDictionary<string, string> RoleMap =>
            this.roleMap;

        public IDictionary<string, PdfLogicalStructureElementAttribute[]> AttributeClassMap =>
            this.attributeClassMap;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfLogicalStructure.<>c <>9 = new PdfLogicalStructure.<>c();
            public static Func<PdfObjectCollection, PdfLogicalStructureElementList, object> <>9__23_0;

            internal object <CreateDictionary>b__23_0(PdfObjectCollection collection, PdfLogicalStructureElementList value) => 
                PdfLogicalStructure.WriteParents(collection, value);
        }
    }
}

