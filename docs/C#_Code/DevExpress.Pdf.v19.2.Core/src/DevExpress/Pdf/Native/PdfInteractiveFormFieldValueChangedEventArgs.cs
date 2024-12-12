namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfInteractiveFormFieldValueChangedEventArgs : EventArgs
    {
        private readonly string fieldName;
        private readonly object oldValue;
        private readonly object newValue;

        public PdfInteractiveFormFieldValueChangedEventArgs(string fieldName, object oldValue, object newValue)
        {
            this.fieldName = fieldName;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public string FieldName =>
            this.fieldName;

        public object OldValue =>
            this.oldValue;

        public object NewValue =>
            this.newValue;
    }
}

