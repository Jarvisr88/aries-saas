namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfDictionary : Dictionary<string, object>
    {
        public const string DictionaryTypeKey = "Type";
        public const string DictionarySubtypeKey = "Subtype";
        public const string DictionaryLanguageKey = "Lang";
        public const string DictionaryJustificationKey = "Q";
        public const string DictionaryAppearanceKey = "DA";
        public const string DictionaryActionKey = "A";
        public const string DictionaryAnnotationHighlightingModeKey = "H";
        public const string DictionaryPaddingKey = "RD";

        protected PdfDictionary()
        {
        }
    }
}

