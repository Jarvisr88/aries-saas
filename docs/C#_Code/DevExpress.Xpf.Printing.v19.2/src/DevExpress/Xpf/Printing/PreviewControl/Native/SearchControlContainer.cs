namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Windows.Input;

    public class SearchControlContainer : DevExpress.Xpf.DocumentViewer.SearchControlContainer
    {
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            base.OnMouseDown(e);
        }
    }
}

