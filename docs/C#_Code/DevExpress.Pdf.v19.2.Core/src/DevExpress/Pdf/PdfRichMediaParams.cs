namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfRichMediaParams : PdfObject
    {
        private const string flashVarsKey = "FlashVars";
        private const string bindingMaterialNameKey = "BindingMaterialName";
        private const string bindingKey = "Binding";
        private const string cuePointsKey = "CuePoints";
        private const string settingsKey = "Settings";
        private readonly string flashVars;
        private readonly PdfRichMediaBinding binding;
        private readonly string bindingMaterialName;
        private readonly List<PdfCuePoint> cuePoints;
        private readonly string settings;

        internal PdfRichMediaParams(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.cuePoints = new List<PdfCuePoint>();
            this.flashVars = dictionary.GetStringAdvanced("FlashVars");
            this.binding = PdfEnumToStringConverter.Parse<PdfRichMediaBinding>(dictionary.GetName("Binding"), true);
            this.bindingMaterialName = dictionary.GetString("BindingMaterialName");
            if ((this.binding == PdfRichMediaBinding.Material) && string.IsNullOrEmpty(this.bindingMaterialName))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            IList<object> array = dictionary.GetArray("CuePoints");
            if (array != null)
            {
                PdfObjectCollection objects = dictionary.Objects;
                foreach (object obj2 in array)
                {
                    PdfReaderDictionary dictionary2 = objects.TryResolve(obj2, null) as PdfReaderDictionary;
                    if (dictionary2 == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.cuePoints.Add(new PdfCuePoint(dictionary2));
                }
            }
            this.settings = dictionary.GetStringAdvanced("Settings");
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddNotNullOrEmptyString("FlashVars", this.flashVars);
            dictionary.AddNotNullOrEmptyString("BindingMaterialName", this.bindingMaterialName);
            dictionary.AddEnumName<PdfRichMediaBinding>("Binding", this.binding);
            if (this.cuePoints.Count > 0)
            {
                dictionary.Add("CuePoints", new PdfWritableConvertibleArray<PdfCuePoint>(this.cuePoints, value => value.Write(objects)));
            }
            dictionary.AddNotNullOrEmptyString("Settings", this.settings);
            return dictionary;
        }

        public string FlashVars =>
            this.flashVars;

        public PdfRichMediaBinding Binding =>
            this.binding;

        public string BindingMaterialName =>
            this.bindingMaterialName;

        public IList<PdfCuePoint> CuePoints =>
            this.cuePoints;

        public string Settings =>
            this.settings;
    }
}

