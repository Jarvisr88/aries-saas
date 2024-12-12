namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfOptionalContentUsageApplication
    {
        private const string eventDictionaryKey = "Event";
        private const string groupsDictionaryKey = "OCGs";
        private const string categoryDictionaryKey = "Category";
        private readonly PdfOptionalContentUsageApplicationEvent usageEvent;
        private readonly IList<PdfOptionalContentGroup> groups;
        private readonly IList<string> category;

        internal PdfOptionalContentUsageApplication(PdfReaderDictionary dictionary)
        {
            this.usageEvent = dictionary.GetEnumName<PdfOptionalContentUsageApplicationEvent>("Event");
            this.groups = dictionary.GetArray<PdfOptionalContentGroup>("OCGs", oc => dictionary.Objects.GetOptionalContentGroup(oc, false));
            this.category = dictionary.GetArray<string>("Category", o => this.ReadCategory(dictionary.Objects, o));
        }

        private string ReadCategory(PdfObjectCollection objects, object o)
        {
            PdfName name = objects.TryResolve(o, null) as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return name.Name;
        }

        internal object Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddEnumName<PdfOptionalContentUsageApplicationEvent>("Event", this.usageEvent);
            dictionary.AddList<PdfOptionalContentGroup>("OCGs", this.groups);
            Func<string, object> converter = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<string, object> local1 = <>c.<>9__14_0;
                converter = <>c.<>9__14_0 = o => new PdfName(o);
            }
            dictionary.AddList<string>("Category", this.category, converter);
            return dictionary;
        }

        public PdfOptionalContentUsageApplicationEvent Event =>
            this.usageEvent;

        public IList<PdfOptionalContentGroup> Groups =>
            this.groups;

        public IList<string> Category =>
            this.category;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfOptionalContentUsageApplication.<>c <>9 = new PdfOptionalContentUsageApplication.<>c();
            public static Func<string, object> <>9__14_0;

            internal object <Write>b__14_0(string o) => 
                new PdfName(o);
        }
    }
}

