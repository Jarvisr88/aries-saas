namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfInteractiveFormFieldActions
    {
        internal const string CharacterChangedDictionaryKey = "K";
        internal const string FieldFormattingDictionaryKey = "F";
        internal const string FieldValueChangedDictionaryKey = "V";
        internal const string FieldValueRecalculatingDictionaryKey = "C";

        internal PdfInteractiveFormFieldActions(PdfReaderDictionary dictionary)
        {
            this.<CharacterChanged>k__BackingField = dictionary.GetJavaScriptAction("K");
            this.<FieldFormatting>k__BackingField = dictionary.GetJavaScriptAction("F");
            this.<FiledValueChanged>k__BackingField = dictionary.GetJavaScriptAction("V");
            this.<FieldValueRecalculating>k__BackingField = dictionary.GetJavaScriptAction("C");
        }

        internal PdfInteractiveFormFieldActions(PdfAcroFormValueFormat format, PdfDocument document)
        {
            if (format.FormatScript != null)
            {
                this.<FieldFormatting>k__BackingField = new PdfJavaScriptAction(format.FormatScript, document);
            }
            if (format.KeystrokeScript != null)
            {
                this.<CharacterChanged>k__BackingField = new PdfJavaScriptAction(format.KeystrokeScript, document);
            }
            if (format.ValidateScript != null)
            {
                this.<FiledValueChanged>k__BackingField = new PdfJavaScriptAction(format.ValidateScript, document);
            }
            if (format.CalculateScript != null)
            {
                this.<FieldValueRecalculating>k__BackingField = new PdfJavaScriptAction(format.CalculateScript, document);
            }
        }

        internal PdfWriterDictionary FillDictionary(PdfWriterDictionary dictionary)
        {
            dictionary.Add("K", this.CharacterChanged);
            dictionary.Add("F", this.FieldFormatting);
            dictionary.Add("V", this.FiledValueChanged);
            dictionary.Add("C", this.FieldValueRecalculating);
            return dictionary;
        }

        public PdfJavaScriptAction CharacterChanged { get; }

        public PdfJavaScriptAction FieldFormatting { get; }

        public PdfJavaScriptAction FiledValueChanged { get; }

        public PdfJavaScriptAction FieldValueRecalculating { get; }
    }
}

