namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    public interface IPreviewModel : INotifyPropertyChanged
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        void HandlePreviewDoubleClick(MouseEventArgs e, FrameworkElement source);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void HandlePreviewMouseLeftButtonDown(MouseButtonEventArgs e, FrameworkElement source);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void HandlePreviewMouseLeftButtonUp(MouseButtonEventArgs e, FrameworkElement source);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void HandlePreviewMouseMove(MouseEventArgs e, FrameworkElement source);
        [EditorBrowsable(EditorBrowsableState.Never)]
        void SetZoom(double value);

        int PageCount { get; }

        FrameworkElement PageContent { get; }

        double PageViewWidth { get; }

        double PageViewHeight { get; }

        double Zoom { get; set; }

        ZoomItemBase ZoomMode { get; set; }

        string ZoomDisplayFormat { get; set; }

        string ZoomDisplayText { get; }

        IEnumerable<ZoomItemBase> ZoomModes { get; }

        bool IsCreating { get; }

        bool IsLoading { get; }

        bool IsIncorrectPageContent { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        IDialogService DialogService { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        ICursorService CursorService { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool UseSimpleScrolling { get; set; }

        DevExpress.Xpf.Printing.InputController InputController { get; }

        ICommand ZoomOutCommand { get; }

        ICommand ZoomInCommand { get; }
    }
}

