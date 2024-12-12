namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public interface IDocumentPreviewModel : IPreviewModel, INotifyPropertyChanged
    {
        event EventHandler<PreviewClickEventArgs> PreviewClick;

        event EventHandler<PreviewClickEventArgs> PreviewDoubleClick;

        event EventHandler<PreviewClickEventArgs> PreviewMouseMove;

        int CurrentPageIndex { get; set; }

        int CurrentPageNumber { get; set; }

        bool ProgressVisibility { get; }

        int ProgressMaximum { get; }

        int ProgressValue { get; }

        bool ProgressMarqueeVisibility { get; }

        DocumentMapTreeViewNode DocumentMapRootNode { get; }

        DocumentMapTreeViewNode DocumentMapSelectedNode { get; set; }

        bool IsDocumentMapVisible { get; set; }

        bool IsParametersPanelVisible { get; set; }

        DevExpress.Xpf.Printing.Parameters.Models.ParametersModel ParametersModel { get; }

        bool IsScaleVisible { get; }

        bool IsSearchVisible { get; }

        bool IsEmptyDocument { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        BrickInfo FoundBrickInfo { get; set; }

        ICommand PrintCommand { get; }

        ICommand FirstPageCommand { get; }

        ICommand PreviousPageCommand { get; }

        ICommand NextPageCommand { get; }

        ICommand LastPageCommand { get; }

        ICommand ExportCommand { get; }

        ICommand WatermarkCommand { get; }

        ICommand StopCommand { get; }

        ICommand ToggleDocumentMapCommand { get; }

        ICommand ToggleParametersPanelCommand { get; }

        ICommand ToggleSearchPanelCommand { get; }

        ICommand PageSetupCommand { get; }

        ICommand ScaleCommand { get; }

        ICommand PrintDirectCommand { get; }

        ICommand SendCommand { get; }

        ICommand OpenCommand { get; }

        ICommand SaveCommand { get; }
    }
}

