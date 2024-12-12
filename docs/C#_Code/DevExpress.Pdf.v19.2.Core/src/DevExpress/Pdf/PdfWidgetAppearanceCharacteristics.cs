namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfWidgetAppearanceCharacteristics
    {
        private const string rotationAngleDictionaryKey = "R";
        private const string borderColorDictionaryKey = "BC";
        private const string backgroundColorDictionaryKey = "BG";
        private const string captionDictionaryKey = "CA";
        private const string rolloverCaptionDictionaryKey = "RC";
        private const string alternateCaptionDictionaryKey = "AC";
        private const string normalIconDictionaryKey = "I";
        private const string rolloverIconDictionaryKey = "RI";
        private const string alternateIconDictionaryKey = "IX";
        private const string iconFitDictionaryKey = "IF";
        private const string textPositionDictionaryKey = "TP";
        private readonly PdfColor borderColor;
        private readonly PdfColor backgroundColor;
        private readonly string rolloverCaption;
        private readonly string alternateCaption;
        private readonly PdfXObject normalIcon;
        private readonly PdfXObject rolloverIcon;
        private readonly PdfXObject alternateIcon;
        private readonly PdfIconFit iconFit;
        private readonly PdfWidgetAnnotationTextPosition textPosition;
        private string caption;
        private int rotationAngle;

        internal PdfWidgetAppearanceCharacteristics()
        {
        }

        internal PdfWidgetAppearanceCharacteristics(IPdfWidgetAppearanceCharacteristicsBuilder builder)
        {
            this.backgroundColor = builder.BackgroundColor;
            this.borderColor = builder.BorderColor;
            this.rotationAngle = builder.RotationAngle;
            this.caption = builder.Caption;
        }

        internal PdfWidgetAppearanceCharacteristics(PdfReaderDictionary dictionary)
        {
            object obj2;
            int? integer = dictionary.GetInteger("R");
            this.rotationAngle = PdfPageTreeObject.NormalizeRotate((integer != null) ? integer.GetValueOrDefault() : 0);
            if (!PdfPageTreeObject.CheckRotate(this.rotationAngle))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.borderColor = PdfAnnotation.ParseColor(dictionary, "BC");
            this.backgroundColor = PdfAnnotation.ParseColor(dictionary, "BG");
            this.caption = dictionary.GetTextString("CA");
            this.rolloverCaption = dictionary.GetTextString("RC");
            this.alternateCaption = dictionary.GetTextString("AC");
            PdfObjectCollection objects = dictionary.Objects;
            this.normalIcon = dictionary.TryGetValue("I", out obj2) ? objects.GetXObject(obj2, null, null) : null;
            this.rolloverIcon = dictionary.TryGetValue("RI", out obj2) ? objects.GetXObject(obj2, null, null) : null;
            this.alternateIcon = dictionary.TryGetValue("IX", out obj2) ? objects.GetXObject(obj2, null, null) : null;
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("IF");
            if (dictionary2 != null)
            {
                this.iconFit = new PdfIconFit(dictionary2);
            }
            this.textPosition = PdfEnumToValueConverter.Parse<PdfWidgetAnnotationTextPosition>(dictionary.GetInteger("TP"), 0);
        }

        internal PdfDictionary ToWritableObject(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("R", this.rotationAngle, 0);
            dictionary.Add("BC", this.borderColor);
            dictionary.Add("BG", this.backgroundColor);
            if (PdfDocEncoding.StringCanBeEncoded(this.caption))
            {
                dictionary.AddPDFDocEncodedString("CA", this.caption);
            }
            else
            {
                dictionary.Add("CA", this.caption, null);
            }
            dictionary.Add("RC", this.rolloverCaption, null);
            dictionary.Add("AC", this.alternateCaption, null);
            dictionary.Add("I", this.normalIcon);
            dictionary.Add("RI", this.rolloverIcon);
            dictionary.Add("IX", this.alternateIcon);
            dictionary.Add("IF", this.iconFit);
            dictionary.Add("TP", PdfEnumToValueConverter.Convert<PdfWidgetAnnotationTextPosition>(this.textPosition), 0);
            return dictionary;
        }

        internal void UpdateButtonCaption(PdfAcroFormButtonStyle buttonStyle)
        {
            switch (buttonStyle)
            {
                case PdfAcroFormButtonStyle.Circle:
                    this.caption = "l";
                    return;

                case PdfAcroFormButtonStyle.Check:
                    this.caption = "4";
                    return;

                case PdfAcroFormButtonStyle.Star:
                    this.caption = "H";
                    return;

                case PdfAcroFormButtonStyle.Cross:
                    this.caption = "8";
                    return;

                case PdfAcroFormButtonStyle.Diamond:
                    this.caption = "u";
                    return;

                case PdfAcroFormButtonStyle.Square:
                    this.caption = "n";
                    return;
            }
            this.caption = null;
        }

        public int RotationAngle =>
            this.rotationAngle;

        public PdfColor BorderColor =>
            this.borderColor;

        public PdfColor BackgroundColor =>
            this.backgroundColor;

        public string Caption =>
            this.caption;

        public string RolloverCaption =>
            this.rolloverCaption;

        public string AlternateCaption =>
            this.alternateCaption;

        public PdfXObject NormalIcon =>
            this.normalIcon;

        public PdfXObject RolloverIcon =>
            this.rolloverIcon;

        public PdfXObject AlternateIcon =>
            this.alternateIcon;

        public PdfIconFit IconFit =>
            this.iconFit;

        public PdfWidgetAnnotationTextPosition TextPosition =>
            this.textPosition;
    }
}

