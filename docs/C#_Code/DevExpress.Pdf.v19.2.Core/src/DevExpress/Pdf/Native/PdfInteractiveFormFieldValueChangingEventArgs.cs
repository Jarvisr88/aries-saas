namespace DevExpress.Pdf.Native
{
    using System;
    using System.ComponentModel;

    public class PdfInteractiveFormFieldValueChangingEventArgs : CancelEventArgs
    {
        private readonly string fieldName;
        private readonly object oldValue;
        private object newValue;

        public PdfInteractiveFormFieldValueChangingEventArgs(string fieldName, object oldValue, object newValue)
        {
            this.fieldName = fieldName;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public string FieldName =>
            this.fieldName;

        public object OldValue =>
            this.oldValue;

        public object NewValue
        {
            get => 
                this.newValue;
            set => 
                this.newValue = value;
        }
    }
}

