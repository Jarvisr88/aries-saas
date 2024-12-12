namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfFreeTextAnnotation : PdfMarkupAnnotation
    {
        internal const string Type = "FreeText";
        private const string defaultStyleDictionaryKey = "DS";
        private const string calloutDictionaryKey = "CL";
        private const string calloutLineEndingStyleDictionaryKey = "LE";
        private const double defaultFontSize = 12.0;
        private readonly PdfTextJustification textJustification;
        private readonly string defaultStyle;
        private readonly PdfAnnotationCallout callout;
        private readonly PdfFreeTextAnnotationIntent freeTextIntent;
        private readonly PdfAnnotationBorderEffect borderEffect;
        private readonly PdfRectangle padding;
        private readonly PdfAnnotationBorderStyle borderStyle;
        private readonly PdfAnnotationLineEndingStyle calloutLineEndingStyle;
        private readonly PdfAnnotationLineEndingStyle finishCalloutLineEndingStyle;
        private readonly byte[] commandsData;
        private readonly PdfCommandList appearanceCommands;

        internal PdfFreeTextAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            object obj2;
            try
            {
                this.commandsData = dictionary.GetBytes("DA");
                this.appearanceCommands = dictionary.GetAppearance(page.Resources);
            }
            catch
            {
            }
            this.appearanceCommands ??= new PdfCommandList();
            this.textJustification = dictionary.GetTextJustification();
            this.defaultStyle = dictionary.GetTextString("DS");
            IList<object> array = dictionary.GetArray("CL");
            if ((array != null) && (array.Count > 0))
            {
                this.callout = new PdfAnnotationCallout(array);
            }
            this.freeTextIntent = PdfEnumToStringConverter.Parse<PdfFreeTextAnnotationIntent>(base.Intent, true);
            this.borderEffect = PdfAnnotationBorderEffect.Parse(dictionary);
            this.padding = dictionary.GetPadding(base.Rect);
            this.borderStyle = PdfAnnotationBorderStyle.Parse(dictionary);
            if (dictionary.TryGetValue("LE", out obj2))
            {
                obj2 = dictionary.Objects.TryResolve(obj2, null);
                if (obj2 != null)
                {
                    PdfName name = obj2 as PdfName;
                    if (name == null)
                    {
                        IList<object> list2 = obj2 as IList<object>;
                        if ((list2 == null) || (list2.Count != 2))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        this.calloutLineEndingStyle = ParseCalloutLineEndingStyle(list2[0]);
                        this.finishCalloutLineEndingStyle = ParseCalloutLineEndingStyle(list2[1]);
                    }
                    else
                    {
                        this.calloutLineEndingStyle = PdfEnumToStringConverter.Parse<PdfAnnotationLineEndingStyle>(name.Name, true);
                    }
                }
            }
        }

        protected internal override IPdfAnnotationAppearanceBuilder CreateAppearanceBuilder(IPdfExportFontProvider fontSearch) => 
            new PdfFreeTextAnnotationAppearanceBuilder(this, fontSearch);

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("DA", this.commandsData);
            dictionary.Add("Q", PdfEnumToValueConverter.Convert<PdfTextJustification>(this.textJustification));
            dictionary.Add("DS", this.defaultStyle, null);
            if (this.callout != null)
            {
                dictionary.Add("CL", this.callout.ToWritableObject());
            }
            if (this.borderEffect != null)
            {
                dictionary.Add("BE", this.borderEffect.ToWritableObject());
            }
            dictionary.Add("RD", this.padding);
            dictionary.Add("BS", this.borderStyle);
            if (this.finishCalloutLineEndingStyle == PdfAnnotationLineEndingStyle.None)
            {
                dictionary.AddEnumName<PdfAnnotationLineEndingStyle>("LE", this.calloutLineEndingStyle);
            }
            else
            {
                object[] objArray = new object[] { new PdfName(PdfEnumToStringConverter.Convert<PdfAnnotationLineEndingStyle>(this.calloutLineEndingStyle, false)), new PdfName(PdfEnumToStringConverter.Convert<PdfAnnotationLineEndingStyle>(this.finishCalloutLineEndingStyle, false)) };
                dictionary.Add("LE", objArray);
            }
            return dictionary;
        }

        private static PdfAnnotationLineEndingStyle ParseCalloutLineEndingStyle(object value)
        {
            PdfName name = value as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfEnumToStringConverter.Parse<PdfAnnotationLineEndingStyle>(name.Name, true);
        }

        public IList<PdfCommand> AppearanceCommands =>
            this.appearanceCommands;

        public PdfTextJustification TextJustification =>
            this.textJustification;

        public string DefaultStyle =>
            this.defaultStyle;

        public PdfAnnotationCallout Callout =>
            this.callout;

        public PdfFreeTextAnnotationIntent FreeTextIntent =>
            this.freeTextIntent;

        public PdfAnnotationBorderEffect BorderEffect =>
            this.borderEffect;

        public PdfRectangle Padding =>
            this.padding;

        public PdfAnnotationBorderStyle BorderStyle =>
            this.borderStyle;

        public PdfAnnotationLineEndingStyle CalloutLineEndingStyle =>
            this.calloutLineEndingStyle;

        public PdfAnnotationLineEndingStyle FinishCalloutLineEndingStyle =>
            this.finishCalloutLineEndingStyle;

        protected override string AnnotationType =>
            "FreeText";
    }
}

