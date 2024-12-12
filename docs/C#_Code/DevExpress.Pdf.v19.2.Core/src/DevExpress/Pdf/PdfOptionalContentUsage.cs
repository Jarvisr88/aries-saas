namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using System;
    using System.Globalization;

    public class PdfOptionalContentUsage : PdfObject
    {
        private const string creatorInfoDictionaryKey = "CreatorInfo";
        private const string languageDictionaryKey = "Language";
        private const string languagePreferredDictionaryKey = "Preferred";
        private const string exportDictionaryKey = "Export";
        private const string exportStateDictionaryKey = "ExportState";
        private const string zoomDictionaryKey = "Zoom";
        private const string minZoomDictionaryKey = "min";
        private const string maxZoomDictionaryKey = "max";
        private const string printDictionaryKey = "Print";
        private const string printStateDictionaryKey = "PrintState";
        private const string viewDictionaryKey = "View";
        private const string viewStateDictionaryKey = "ViewState";
        private const string pageElementDictionaryKey = "PageElement";
        private const string onValue = "ON";
        private const string offValue = "OFF";
        private const double defaultMinZoom = 0.0;
        private const double defaultMaxZoom = double.PositiveInfinity;
        private readonly PdfOptionalContentUsageCreatorInfo creatorInfo;
        private readonly CultureInfo languageCulture = CultureInfo.InvariantCulture;
        private readonly bool isLanguagePreferred;
        private readonly DefaultBoolean exportState;
        private readonly double minZoom;
        private readonly double maxZoom;
        private readonly string printContentKind;
        private readonly DefaultBoolean printState;
        private readonly DefaultBoolean viewState;
        private readonly PdfPageElement pageElement;

        internal PdfOptionalContentUsage(PdfReaderDictionary dictionary)
        {
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("CreatorInfo");
            if (dictionary2 != null)
            {
                this.creatorInfo = new PdfOptionalContentUsageCreatorInfo(dictionary2);
            }
            PdfReaderDictionary dictionary3 = dictionary.GetDictionary("Language");
            if (dictionary3 != null)
            {
                this.languageCulture = dictionary3.GetLanguageCulture();
                this.isLanguagePreferred = ParseOnOff(dictionary3, "Preferred") == DefaultBoolean.True;
            }
            PdfReaderDictionary dictionary4 = dictionary.GetDictionary("Export");
            this.exportState = (dictionary4 != null) ? ParseOnOff(dictionary4, "ExportState") : DefaultBoolean.Default;
            if (dictionary.GetDictionary("Zoom") == null)
            {
                this.minZoom = 0.0;
                this.maxZoom = double.PositiveInfinity;
            }
            else
            {
                double? number = dictionary.GetNumber("min");
                this.minZoom = (number != null) ? number.GetValueOrDefault() : 0.0;
                number = dictionary.GetNumber("max");
                this.maxZoom = (number != null) ? number.GetValueOrDefault() : double.PositiveInfinity;
            }
            PdfReaderDictionary dictionary6 = dictionary.GetDictionary("Print");
            if (dictionary6 != null)
            {
                this.printContentKind = dictionary6.GetName("Subtype");
                this.printState = ParseOnOff(dictionary6, "PrintState");
            }
            PdfReaderDictionary dictionary7 = dictionary.GetDictionary("View");
            if (dictionary7 != null)
            {
                this.viewState = ParseOnOff(dictionary7, "ViewState");
            }
            PdfReaderDictionary dictionary8 = dictionary.GetDictionary("PageElement");
            if (dictionary8 != null)
            {
                this.pageElement = PdfEnumToStringConverter.Parse<PdfPageElement>(dictionary8.GetName("Subtype"), true);
            }
        }

        private static DefaultBoolean ParseOnOff(PdfReaderDictionary dictionary, string key)
        {
            string name = dictionary.GetName(key);
            return (!string.IsNullOrEmpty(name) ? ((name == "ON") ? DefaultBoolean.True : ((name == "OFF") ? DefaultBoolean.False : DefaultBoolean.Default)) : DefaultBoolean.Default);
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("CreatorInfo", this.creatorInfo);
            if (!ReferenceEquals(this.languageCulture, CultureInfo.InvariantCulture) || this.isLanguagePreferred)
            {
                PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
                dictionary2.Add("Lang", PdfDocumentStream.ConvertToArray(this.languageCulture.Name));
                if (this.isLanguagePreferred)
                {
                    dictionary2.Add("Preferred", new PdfName("ON"));
                }
                dictionary.Add("Language", dictionary2);
            }
            if (this.exportState != DefaultBoolean.Default)
            {
                PdfWriterDictionary dictionary3 = new PdfWriterDictionary(objects);
                WriteOnOffState(dictionary3, "ExportState", this.exportState);
                dictionary.Add("Export", dictionary3);
            }
            bool flag = !(this.minZoom == 0.0);
            bool flag2 = !double.IsPositiveInfinity(this.maxZoom);
            if (flag | flag2)
            {
                PdfWriterDictionary dictionary4 = new PdfWriterDictionary(objects);
                if (flag)
                {
                    dictionary4.Add("min", this.minZoom);
                }
                if (flag2)
                {
                    dictionary4.Add("max", this.maxZoom);
                }
                dictionary.Add("Zoom", dictionary4);
            }
            bool flag3 = !string.IsNullOrEmpty(this.printContentKind);
            if (flag3 || (this.printState != DefaultBoolean.Default))
            {
                PdfWriterDictionary dictionary5 = new PdfWriterDictionary(objects);
                if (flag3)
                {
                    dictionary5.Add("Subtype", new PdfName(this.printContentKind));
                }
                WriteOnOffState(dictionary5, "PrintState", this.printState);
                dictionary.Add("Print", dictionary5);
            }
            if (this.viewState != DefaultBoolean.Default)
            {
                PdfWriterDictionary dictionary6 = new PdfWriterDictionary(objects);
                WriteOnOffState(dictionary6, "ViewState", this.viewState);
                dictionary.Add("View", dictionary6);
            }
            if (this.pageElement != PdfPageElement.None)
            {
                PdfWriterDictionary dictionary7 = new PdfWriterDictionary(objects);
                dictionary7.Add("Subtype", new PdfName(PdfEnumToStringConverter.Convert<PdfPageElement>(this.pageElement, true)));
                dictionary.Add("PageElement", dictionary7);
            }
            return dictionary;
        }

        private static void WriteOnOffState(PdfDictionary dictionary, string key, DefaultBoolean state)
        {
            if (state != DefaultBoolean.Default)
            {
                dictionary.Add(key, new PdfName((state == DefaultBoolean.True) ? "ON" : "OFF"));
            }
        }

        public PdfOptionalContentUsageCreatorInfo CreatorInfo =>
            this.creatorInfo;

        public CultureInfo LanguageCulture =>
            this.languageCulture;

        public bool IsLanguagePreferred =>
            this.isLanguagePreferred;

        public DefaultBoolean ExportState =>
            this.exportState;

        public double MinZoom =>
            this.minZoom;

        public double MaxZoom =>
            this.maxZoom;

        public string PrintContentKind =>
            this.printContentKind;

        public DefaultBoolean PrintState =>
            this.printState;

        public DefaultBoolean ViewState =>
            this.viewState;

        public PdfPageElement PageElement =>
            this.pageElement;
    }
}

