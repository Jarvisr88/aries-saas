namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;

    public class PdfShowingEditorEventArgs : PdfEditorEventArgsBase
    {
        internal PdfShowingEditorEventArgs(string fieldName) : base(PdfViewerControl.ShowingEditorEvent, fieldName)
        {
        }

        public bool Cancel { get; set; }
    }
}

