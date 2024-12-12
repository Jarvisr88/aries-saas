namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Windows;

    internal abstract class PreviewHelper
    {
        protected PreviewHelper()
        {
        }

        protected abstract void CreateDocumentIfEmpty();
        internal Window CreateDocumentPreviewWindow(Window owner, string title) => 
            this.CreatePreviewWindow(owner, title, CommandBarStyle.Bars);

        protected virtual DocumentPreviewWindow CreatePreviewWindow(Window owner, string title, CommandBarStyle commandBarStyle)
        {
            DocumentPreviewWindow window = new DocumentPreviewWindow {
                PreviewControl = { RequestDocumentCreation = true }
            };
            window.Closed += new EventHandler(this.OnClosed);
            window.Owner = owner;
            string text1 = title;
            if (title == null)
            {
                string local1 = title;
                text1 = PrintingLocalizer.GetString(PrintingStringId.PrintPreviewWindowCaption);
            }
            window.Title = text1;
            window.PreviewControl.DocumentSource = this.DocumentSource;
            window.PreviewControl.CommandBarStyle = commandBarStyle;
            return window;
        }

        internal Window CreateRibbonDocumentPreviewWindow(Window owner, string title) => 
            this.CreatePreviewWindow(owner, title, CommandBarStyle.Ribbon);

        private void OnClosed(object sender, EventArgs e)
        {
            (sender as DocumentPreviewWindow).Closed -= new EventHandler(this.OnClosed);
            this.StopPageBuilding();
        }

        public Window ShowDocumentPreview(Window owner, string title)
        {
            Window window = this.CreateDocumentPreviewWindow(owner, title);
            window.Show();
            return window;
        }

        public void ShowDocumentPreviewDialog(Window owner, string title)
        {
            this.CreateDocumentPreviewWindow(owner, title).ShowDialog();
        }

        public Window ShowRibbonDocumentPreview(Window owner, string title)
        {
            Window window = this.CreateRibbonDocumentPreviewWindow(owner, title);
            window.Show();
            return window;
        }

        public void ShowRibbonDocumentPreviewDialog(Window owner, string title)
        {
            this.CreateRibbonDocumentPreviewWindow(owner, title).ShowDialog();
        }

        protected abstract void StopPageBuilding();

        protected abstract object DocumentSource { get; }
    }
}

