namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FormFieldValueChangingEventArgs : RoutedEventArgs
    {
        public FormFieldValueChangingEventArgs(string fieldName, object oldValue, object newValue) : base(PdfViewerControl.FormFieldValueChangingEvent)
        {
            this.FieldName = fieldName;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public string FieldName { get; private set; }

        public object OldValue { get; private set; }

        public object NewValue { get; set; }

        public bool Cancel { get; set; }
    }
}

