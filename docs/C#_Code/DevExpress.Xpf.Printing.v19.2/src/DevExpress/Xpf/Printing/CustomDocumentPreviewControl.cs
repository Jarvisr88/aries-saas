namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Windows;

    internal class CustomDocumentPreviewControl : DocumentPreviewControl
    {
        internal override void CreateDocumentIfNeeded()
        {
            if (!base.Document.IsCreated)
            {
                base.Dispatcher.BeginInvoke(new Action(this.CreateDocumentIfNeeded), new object[0]);
            }
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            this.ActualPreview.Do<BackstagePrintPreview>(x => x.AttachPreview(this));
        }

        protected internal BackstagePrintPreview ActualPreview =>
            BackstagePrintPreview.GetActualPreview(this);
    }
}

