namespace DevExpress.Xpf.PdfViewer.Helpers
{
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class DXMessageBoxHelper
    {
        public static MessageBoxResult Show(FrameworkElement pdfViewerControl, string getString, string documentName, MessageBoxButton ok, MessageBoxImage information)
        {
            if (DesignerProperties.GetIsInDesignMode(pdfViewerControl))
            {
                return MessageBoxResult.None;
            }
            if (Skip)
            {
                return MessageBoxResult.None;
            }
            string text = getString;
            MessageBoxResult? defaultButton = null;
            bool? showActivated = null;
            return ThemedMessageBox.Show(null, documentName, text, ok, defaultButton, information, false, MessageBoxOptions.None, WindowStartupLocation.CenterOwner, WindowTitleAlignment.Left, showActivated);
        }

        public static bool Skip { get; set; }
    }
}

