namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Windows.Threading;

    public interface INavigationPaneUI
    {
        void FocusSearchBox();
        void SyncSearchParametersWithModel();
        void SyncSearchParametersWithSettings();

        PrintingSystemBase PrintingSystem { get; }

        ObservableRangeCollection<object> BookmarkNodes { get; }

        object SelectedBookmarkNode { get; set; }

        System.Windows.Threading.Dispatcher Dispatcher { get; }

        DocumentPresenterControl DocumentPresenter { get; }

        int CurrentPageNumber { get; }

        NavigationPaneTabType ActiveTab { get; }
    }
}

