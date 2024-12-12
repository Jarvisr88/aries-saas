namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DocumentPreviewEditingEventArgs : RoutedEventArgs
    {
        public DocumentPreviewEditingEventArgs(DevExpress.XtraPrinting.EditingField editingField)
        {
            this.EditingField = editingField;
        }

        public DevExpress.XtraPrinting.EditingField EditingField { get; private set; }
    }
}

