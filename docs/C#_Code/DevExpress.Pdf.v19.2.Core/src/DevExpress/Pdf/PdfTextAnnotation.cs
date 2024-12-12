namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTextAnnotation : PdfMarkupAnnotation
    {
        internal const string Type = "Text";
        private const string isOpenedDictionaryKey = "Open";
        private const string iconNameDictionaryKey = "Name";
        private const string stateDictionaryKey = "State";
        private const string stateModelDictionaryKey = "StateModel";
        private const bool defaultOpenedState = false;
        private const string defaultIconName = "Note";
        private readonly bool isOpened;
        private readonly string iconName;
        private readonly string state;
        private readonly string stateModel;

        internal PdfTextAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            bool? boolean = dictionary.GetBoolean("Open");
            this.isOpened = (boolean != null) ? boolean.GetValueOrDefault() : false;
            string name = dictionary.GetName("Name");
            PdfTextAnnotation annotation1 = this;
            if (name == null)
            {
                annotation1 = "Note";
            }
            name.iconName = (string) annotation1;
            this.state = dictionary.GetTextString("State");
            this.stateModel = dictionary.GetTextString("StateModel");
        }

        protected internal override IPdfAnnotationAppearanceBuilder CreateAppearanceBuilder(IPdfExportFontProvider fontSearch) => 
            new PdfTextAnnotationAppearanceBuilder(this);

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("Open", this.isOpened, false);
            dictionary.AddName("Name", this.iconName, "Note");
            dictionary.Add("State", this.state);
            dictionary.Add("StateModel", this.stateModel);
            return dictionary;
        }

        public bool IsOpened =>
            this.isOpened;

        public string IconName =>
            this.iconName;

        public string State =>
            this.state;

        public string StateModel =>
            this.stateModel;

        protected override string AnnotationType =>
            "Text";
    }
}

