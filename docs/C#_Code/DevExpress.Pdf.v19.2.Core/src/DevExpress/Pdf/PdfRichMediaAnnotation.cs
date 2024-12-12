namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfRichMediaAnnotation : PdfAnnotation
    {
        internal const string Type = "RichMedia";
        private const string richMediaContentDictionaryKey = "RichMediaContent";
        private const string richMediaSettingsDictionaryKey = "RichMediaSettings";
        private const string assetsDictionaryKey = "Assets";
        private const string configurationsDictionaryKey = "Configurations";
        private readonly PdfDeferredSortedDictionary<string, PdfFileSpecification> assets;
        private readonly List<PdfRichMediaConfiguration> configurations;
        private readonly PdfRichMediaSettings settings;

        internal PdfRichMediaAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.configurations = new List<PdfRichMediaConfiguration>();
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("RichMediaContent");
            if (dictionary2 == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfCreateTreeElementAction<PdfFileSpecification> createElement = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                PdfCreateTreeElementAction<PdfFileSpecification> local1 = <>c.<>9__16_0;
                createElement = <>c.<>9__16_0 = (o, v) => o.GetFileSpecification(v);
            }
            this.assets = PdfNameTreeNode<PdfFileSpecification>.Parse(dictionary2.GetDictionary("Assets"), createElement);
            IList<object> array = dictionary2.GetArray("Configurations");
            IList<object> list2 = array;
            if (array == null)
            {
                IList<object> local2 = array;
                list2 = dictionary2.GetArray("Configuration");
            }
            IList<object> list = list2;
            if (list != null)
            {
                PdfObjectCollection objects = dictionary.Objects;
                foreach (object obj2 in list)
                {
                    PdfReaderDictionary dictionary4 = objects.TryResolve(obj2, null) as PdfReaderDictionary;
                    if (dictionary4 == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.configurations.Add(new PdfRichMediaConfiguration(this.assets, dictionary4));
                }
            }
            PdfReaderDictionary dictionary3 = dictionary.GetDictionary("RichMediaSettings");
            if (dictionary3 != null)
            {
                this.settings = new PdfRichMediaSettings(this, dictionary3);
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
            dictionary2.AddIfPresent("Assets", PdfNameTreeNode<PdfFileSpecification>.Write(objects, this.assets));
            dictionary2.AddList<PdfRichMediaConfiguration>("Configurations", this.configurations);
            dictionary.Add("RichMediaContent", dictionary2);
            if ((this.settings != null) || ((this.settings.Activation != null) || (this.settings.Deactivation != null)))
            {
                dictionary.Add("RichMediaSettings", this.settings);
            }
            return dictionary;
        }

        public IDictionary<string, PdfFileSpecification> Assets =>
            this.assets;

        public IList<PdfRichMediaConfiguration> Configurations =>
            this.configurations;

        public PdfRichMediaSettings Settings =>
            this.settings;

        protected override string AnnotationType =>
            "RichMedia";

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfRichMediaAnnotation.<>c <>9 = new PdfRichMediaAnnotation.<>c();
            public static PdfCreateTreeElementAction<PdfFileSpecification> <>9__16_0;

            internal PdfFileSpecification <.ctor>b__16_0(PdfObjectCollection o, object v) => 
                o.GetFileSpecification(v);
        }
    }
}

