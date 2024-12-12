namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfAdditionalActions : PdfObject
    {
        internal const string DictionaryAdditionalActionsKey = "AA";
        private static readonly string[] InteractiveFormFieldActionsDictionaryKeys = new string[] { "K", "F", "V", "C" };
        private static readonly string[] AnnotationActionsDictionaryKeys;
        private readonly PdfAnnotationActions annotationActions;
        private readonly PdfInteractiveFormFieldActions interactiveFormFieldActions;

        static PdfAdditionalActions()
        {
            string[] textArray2 = new string[10];
            textArray2[0] = "E";
            textArray2[1] = "X";
            textArray2[2] = "D";
            textArray2[3] = "U";
            textArray2[4] = "Fo";
            textArray2[5] = "Bl";
            textArray2[6] = "PO";
            textArray2[7] = "PC";
            textArray2[8] = "PV";
            textArray2[9] = "PI";
            AnnotationActionsDictionaryKeys = textArray2;
        }

        internal PdfAdditionalActions(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.annotationActions = isDictionaryContainsActions(dictionary, AnnotationActionsDictionaryKeys) ? new PdfAnnotationActions(dictionary) : null;
            this.interactiveFormFieldActions = isDictionaryContainsActions(dictionary, InteractiveFormFieldActionsDictionaryKeys) ? new PdfInteractiveFormFieldActions(dictionary) : null;
        }

        internal PdfAdditionalActions(PdfInteractiveFormFieldActions interactiveFormFieldActions)
        {
            this.interactiveFormFieldActions = interactiveFormFieldActions;
        }

        private static bool isDictionaryContainsActions(PdfReaderDictionary dictionary, IEnumerable<string> actionDictionaryKeys)
        {
            bool flag;
            using (IEnumerator<string> enumerator = actionDictionaryKeys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        string current = enumerator.Current;
                        if (!dictionary.ContainsKey(current))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            if (this.annotationActions != null)
            {
                this.annotationActions.FillDictionary(dictionary);
            }
            if (this.interactiveFormFieldActions != null)
            {
                this.interactiveFormFieldActions.FillDictionary(dictionary);
            }
            return dictionary;
        }

        public PdfAnnotationActions AnnotationActions =>
            this.annotationActions;

        public PdfInteractiveFormFieldActions InteractiveFormFieldActions =>
            this.interactiveFormFieldActions;
    }
}

