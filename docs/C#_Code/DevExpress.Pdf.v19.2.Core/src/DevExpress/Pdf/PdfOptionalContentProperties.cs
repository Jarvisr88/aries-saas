namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfOptionalContentProperties
    {
        private const string groupsDictionaryKey = "OCGs";
        private const string defaultConfigurationDictionaryKey = "D";
        private const string configurationsDictionaryKey = "Configs";
        private readonly IList<PdfOptionalContentGroup> groups;
        private readonly PdfOptionalContentConfiguration defaultConfiguration;
        private readonly IList<PdfOptionalContentConfiguration> configurations;

        internal PdfOptionalContentProperties(PdfReaderDictionary dictionary)
        {
            IList<PdfOptionalContentGroup> array = dictionary.GetArray<PdfOptionalContentGroup>("OCGs", o => dictionary.Objects.GetOptionalContentGroup(o, true));
            IList<PdfOptionalContentGroup> list2 = array;
            if (array == null)
            {
                IList<PdfOptionalContentGroup> local1 = array;
                list2 = new PdfOptionalContentGroup[0];
            }
            this.groups = list2;
            this.defaultConfiguration = new PdfOptionalContentConfiguration(dictionary.GetDictionary("D"));
            Func<object, PdfOptionalContentConfiguration> create = <>c.<>9__12_1;
            if (<>c.<>9__12_1 == null)
            {
                Func<object, PdfOptionalContentConfiguration> local2 = <>c.<>9__12_1;
                create = <>c.<>9__12_1 = delegate (object d) {
                    PdfReaderDictionary dictionary = d as PdfReaderDictionary;
                    if (dictionary == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return new PdfOptionalContentConfiguration(dictionary);
                };
            }
            this.configurations = dictionary.GetArray<PdfOptionalContentConfiguration>("Configs", create);
        }

        internal PdfWriterDictionary Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddRequiredList<PdfOptionalContentGroup>("OCGs", this.groups);
            dictionary.Add("D", this.defaultConfiguration.Write(objects));
            dictionary.AddList<PdfOptionalContentConfiguration>("Configs", this.configurations, o => o.Write(objects));
            return dictionary;
        }

        public IList<PdfOptionalContentGroup> Groups =>
            this.groups;

        public PdfOptionalContentConfiguration DefaultConfiguration =>
            this.defaultConfiguration;

        public IList<PdfOptionalContentConfiguration> Configurations =>
            this.configurations;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfOptionalContentProperties.<>c <>9 = new PdfOptionalContentProperties.<>c();
            public static Func<object, PdfOptionalContentConfiguration> <>9__12_1;

            internal PdfOptionalContentConfiguration <.ctor>b__12_1(object d)
            {
                PdfReaderDictionary dictionary = d as PdfReaderDictionary;
                if (dictionary == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return new PdfOptionalContentConfiguration(dictionary);
            }
        }
    }
}

