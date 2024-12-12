namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using System;

    public class PdfLaunchAction : PdfAction
    {
        internal const string Name = "Launch";
        private const string fileDictionaryKey = "F";
        private const string windowsDictionaryKey = "Win";
        private const string fileNameDictionaryKey = "F";
        private const string defaultDirectoryDictionaryKey = "D";
        private const string operationDictionaryKey = "O";
        private const string parametersDictionaryKey = "P";
        private const string newWindowDictionaryKey = "NewWindow";
        private readonly PdfFileSpecification fileSpecification;
        private readonly string fileName;
        private readonly string defaultDirectory;
        private readonly PdfLaunchOperation operation;
        private readonly string parameters;
        private readonly DefaultBoolean newWindow;

        internal PdfLaunchAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            this.fileSpecification = PdfFileSpecification.Parse(dictionary, "F", false);
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Win");
            if (dictionary2 != null)
            {
                this.fileName = dictionary2.GetString("F");
                this.defaultDirectory = dictionary2.GetString("D");
                this.operation = PdfEnumToStringConverter.Parse<PdfLaunchOperation>(dictionary2.GetString("O"), true);
                this.parameters = dictionary2.GetString("P");
            }
            bool? boolean = dictionary.GetBoolean("NewWindow");
            if (boolean != null)
            {
                this.newWindow = boolean.Value ? DefaultBoolean.True : DefaultBoolean.False;
            }
            else
            {
                this.newWindow = DefaultBoolean.Default;
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("F", this.fileSpecification);
            if (!string.IsNullOrEmpty(this.fileName) || (!string.IsNullOrEmpty(this.defaultDirectory) || ((this.operation != PdfLaunchOperation.None) || !string.IsNullOrEmpty(this.parameters))))
            {
                PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
                dictionary2.AddIfPresent("F", this.fileName);
                dictionary2.AddIfPresent("D", this.defaultDirectory);
                dictionary2.AddASCIIString("O", PdfEnumToStringConverter.Convert<PdfLaunchOperation>(this.operation, true));
                dictionary2.AddIfPresent("P", this.parameters);
                dictionary.Add("Win", dictionary2);
            }
            if (this.newWindow != DefaultBoolean.Default)
            {
                dictionary.Add("NewWindow", this.newWindow == DefaultBoolean.True);
            }
            return dictionary;
        }

        protected override string ActionType =>
            "Launch";

        public PdfFileSpecification FileSpecification =>
            this.fileSpecification;

        public string FileName =>
            this.fileName;

        public string DefaultDirectory =>
            this.defaultDirectory;

        public PdfLaunchOperation Operation =>
            this.operation;

        public string Parameters =>
            this.parameters;

        public DefaultBoolean NewWindow =>
            this.newWindow;
    }
}

