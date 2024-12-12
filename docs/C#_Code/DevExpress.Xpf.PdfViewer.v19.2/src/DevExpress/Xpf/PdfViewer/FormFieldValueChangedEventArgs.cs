namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FormFieldValueChangedEventArgs : RoutedEventArgs
    {
        public FormFieldValueChangedEventArgs(string fieldName, object oldValue, object newValue) : base(PdfViewerControl.FormFieldValueChangedEvent)
        {
            this.FieldName = fieldName;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public string FieldName { get; private set; }

        public object OldValue { get; private set; }

        public object NewValue { get; private set; }
    }
}

