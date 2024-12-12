namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Windows;

    public class PrintingSearchPanel : SearchPanel
    {
        public static readonly DependencyProperty ModelProperty = DependencyPropertyManager.Register("Model", typeof(IDocumentPreviewModel), typeof(PrintingSearchPanel), new PropertyMetadata(null, new PropertyChangedCallback(PrintingSearchPanel.OnModelChanged)));

        public PrintingSearchPanel()
        {
            base.ViewModel = new SearchPanelViewModel();
        }

        public void FocusSearchBox()
        {
            if (base.SearchStringEdit != null)
            {
                base.SearchStringEdit.Focus();
            }
        }

        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SearchPanelViewModel viewModel = ((PrintingSearchPanel) d).ViewModel as SearchPanelViewModel;
            if (viewModel != null)
            {
                viewModel.Model = (IDocumentPreviewModel) e.NewValue;
            }
        }

        public IDocumentPreviewModel Model
        {
            get => 
                (IDocumentPreviewModel) base.GetValue(ModelProperty);
            set => 
                base.SetValue(ModelProperty, value);
        }
    }
}

