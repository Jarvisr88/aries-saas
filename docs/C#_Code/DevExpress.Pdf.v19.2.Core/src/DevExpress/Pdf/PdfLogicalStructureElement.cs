namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class PdfLogicalStructureElement : PdfLogicalStructureEntry
    {
        private const string dictionaryType = "StructElem";
        private const string structureTypeDictionaryKey = "S";
        private const string parentDictionaryKey = "P";
        private const string idDictionaryKey = "ID";
        private const string pageDictionaryKey = "Pg";
        private const string attributesDictionaryKey = "A";
        private const string attributeClassesDictionaryKey = "C";
        private const string revisionNumberDictionaryKey = "R";
        private const string titleDictionaryKey = "T";
        private const string alternateDescriptionDictionaryKey = "Alt";
        private const string abbreviationDictionaryKey = "E";
        private const string actualTextDictionaryKey = "ActualText";
        private readonly PdfLogicalStructureEntry parent;
        private readonly string structureType;
        private readonly int? structureTypeObject;
        private readonly byte[] id;
        private readonly PdfPage page;
        private readonly IList<PdfLogicalStructureElementAttribute> attributes;
        private readonly IList<string> attributeClasses;
        private readonly int revisionNumber;
        private readonly string title;
        private readonly CultureInfo languageCulture;
        private readonly string alternateDescription;
        private readonly string abbreviation;
        private readonly string actualText;

        private PdfLogicalStructureElement(PdfLogicalStructure logicalStructure, PdfLogicalStructureEntry parent, PdfReaderDictionary dictionary, string structureType, int? structureTypeObject) : base(logicalStructure, dictionary)
        {
            object obj2;
            this.parent = parent;
            this.structureType = structureType;
            this.structureTypeObject = structureTypeObject;
            this.id = dictionary.GetBytes("ID");
            PdfObjectCollection objects = dictionary.Objects;
            PdfObjectReference objectReference = dictionary.GetObjectReference("Pg");
            if (objectReference != null)
            {
                this.page = logicalStructure.DocumentCatalog.FindPage(objectReference);
            }
            if (dictionary.TryGetValue("A", out obj2))
            {
                this.attributes = PdfLogicalStructureElementAttribute.Parse(objects, obj2);
            }
            if (dictionary.TryGetValue("C", out obj2))
            {
                obj2 = objects.TryResolve(obj2, null);
                PdfName name = obj2 as PdfName;
                if (name != null)
                {
                    List<string> list1 = new List<string>();
                    list1.Add(name.Name);
                    this.attributeClasses = list1;
                }
                else
                {
                    IList<object> list = obj2 as IList<object>;
                    if (list == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.attributeClasses = new List<string>(list.Count);
                    foreach (object obj3 in list)
                    {
                        name = obj3 as PdfName;
                        if (name == null)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        this.attributeClasses.Add(name.Name);
                    }
                }
            }
            int? integer = dictionary.GetInteger("R");
            this.revisionNumber = (integer != null) ? integer.GetValueOrDefault() : 0;
            this.title = dictionary.GetString("T");
            this.languageCulture = dictionary.GetLanguageCulture();
            this.alternateDescription = dictionary.GetString("Alt");
            this.abbreviation = dictionary.GetString("E");
            this.actualText = dictionary.GetString("ActualText");
        }

        internal static PdfLogicalStructureElement Create(PdfLogicalStructure logicalStructure, PdfLogicalStructureEntry parent, PdfReaderDictionary dictionary)
        {
            object obj2;
            if (!dictionary.TryGetValue("S", out obj2))
            {
                return null;
            }
            obj2 = dictionary.Objects.TryResolve(obj2, null);
            PdfName name = obj2 as PdfName;
            string structureType = null;
            int? structureTypeObject = null;
            if (name != null)
            {
                structureType = name.Name;
            }
            else
            {
                structureTypeObject = obj2 as int?;
            }
            return (((structureType != null) || (structureTypeObject != null)) ? new PdfLogicalStructureElement(logicalStructure, parent, dictionary, structureType, structureTypeObject) : null);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            if (this.structureTypeObject != null)
            {
                dictionary.Add("S", this.structureTypeObject.Value);
            }
            else
            {
                dictionary.AddName("S", this.structureType);
            }
            dictionary.Add("P", this.parent);
            dictionary.AddIfPresent("ID", this.id);
            dictionary.Add("Pg", this.page);
            base.WriteKids(dictionary, collection);
            if (this.attributes != null)
            {
                dictionary.Add("A", new PdfWritableObjectArray((IEnumerable<PdfObject>) this.attributes, collection));
            }
            if (this.attributeClasses != null)
            {
                if (this.attributeClasses.Count == 1)
                {
                    dictionary.AddName("C", this.attributeClasses[0]);
                }
                else
                {
                    Func<string, object> convert = <>c.<>9__53_0;
                    if (<>c.<>9__53_0 == null)
                    {
                        Func<string, object> local1 = <>c.<>9__53_0;
                        convert = <>c.<>9__53_0 = o => new PdfName(o);
                    }
                    dictionary.Add("C", new PdfWritableConvertibleArray<string>(this.attributeClasses, convert));
                }
            }
            dictionary.Add("R", this.revisionNumber, 0);
            dictionary.AddIfPresent("T", this.title);
            dictionary.AddLanguage(this.languageCulture);
            dictionary.AddIfPresent("Alt", this.alternateDescription);
            dictionary.AddNotNullOrEmptyString("E", this.abbreviation);
            dictionary.AddNotNullOrEmptyString("ActualText", this.actualText);
            return dictionary;
        }

        public PdfLogicalStructureEntry Parent =>
            this.parent;

        public string StructureType =>
            this.structureType;

        public byte[] ID =>
            this.id;

        public PdfPage Page =>
            this.page;

        public IEnumerable<PdfLogicalStructureElementAttribute> Attributes =>
            this.attributes;

        public IList<string> AttributeClasses =>
            this.attributeClasses;

        public int RevisionNumber =>
            this.revisionNumber;

        public string Title =>
            this.title;

        public CultureInfo LanguageCulture =>
            this.languageCulture;

        public string AlternateDescription =>
            this.alternateDescription;

        public string Abbreviation =>
            this.abbreviation;

        public string ActualText =>
            this.actualText;

        protected internal override PdfPage ContainingPage =>
            this.page;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfLogicalStructureElement.<>c <>9 = new PdfLogicalStructureElement.<>c();
            public static Func<string, object> <>9__53_0;

            internal object <CreateDictionary>b__53_0(string o) => 
                new PdfName(o);
        }
    }
}

