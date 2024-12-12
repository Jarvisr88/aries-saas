namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PdfEditorEventArgsBase : RoutedEventArgs
    {
        protected PdfEditorEventArgsBase(RoutedEvent e, string fieldName) : base(e)
        {
            this.FieldName = fieldName;
        }

        public string FieldName { get; private set; }
    }
}

