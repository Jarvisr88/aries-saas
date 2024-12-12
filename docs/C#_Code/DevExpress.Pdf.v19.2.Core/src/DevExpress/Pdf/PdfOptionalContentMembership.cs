namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfOptionalContentMembership : PdfOptionalContent
    {
        internal const string Type = "OCMD";
        private const string groupsDictionaryKey = "OCGs";
        private const string visibilityPolicyDictionaryName = "P";
        private const string visibilityExpressionDictionaryName = "VE";
        private readonly IList<PdfOptionalContentGroup> groups;
        private readonly PdfOptionalContentVisibilityPolicy visibilityPolicy;
        private readonly PdfOptionalContentVisibilityExpression visibilityExpression;

        internal PdfOptionalContentMembership(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            object obj2;
            if (dictionary.TryGetValue("OCGs", out obj2))
            {
                this.groups = new List<PdfOptionalContentGroup>();
                PdfObjectCollection objects = dictionary.Objects;
                obj2 = objects.TryResolve(obj2, null);
                PdfReaderDictionary dictionary2 = obj2 as PdfReaderDictionary;
                if (dictionary2 != null)
                {
                    this.AddGroup(dictionary2);
                }
                else
                {
                    IList<object> list2 = obj2 as IList<object>;
                    if (list2 == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    foreach (object obj3 in list2)
                    {
                        dictionary2 = objects.TryResolve(obj3, null) as PdfReaderDictionary;
                        if (dictionary2 == null)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        this.AddGroup(dictionary2);
                    }
                }
            }
            this.visibilityPolicy = PdfEnumToStringConverter.Parse<PdfOptionalContentVisibilityPolicy>(dictionary.GetName("P"), true);
            IList<object> array = dictionary.GetArray("VE");
            if (array != null)
            {
                this.visibilityExpression = new PdfOptionalContentVisibilityExpression(dictionary.Objects, array);
            }
        }

        private void AddGroup(PdfReaderDictionary dictionary)
        {
            PdfOptionalContentGroup item = ParseOptionalContent(dictionary) as PdfOptionalContentGroup;
            if (item == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.groups.Add(item);
        }

        protected internal override object Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "OCMD");
            if (this.groups != null)
            {
                dictionary["OCGs"] = (this.groups.Count == 1) ? ((object) objects.AddObject((PdfObject) this.groups[0])) : ((object) new PdfWritableObjectArray((IEnumerable<PdfObject>) this.groups, objects));
            }
            dictionary.AddEnumName<PdfOptionalContentVisibilityPolicy>("P", this.visibilityPolicy);
            dictionary.Add("VE", this.visibilityExpression);
            return dictionary;
        }

        public IList<PdfOptionalContentGroup> Groups =>
            this.groups;

        public PdfOptionalContentVisibilityPolicy VisibilityPolicy =>
            this.visibilityPolicy;

        public PdfOptionalContentVisibilityExpression VisibilityExpression =>
            this.visibilityExpression;
    }
}

