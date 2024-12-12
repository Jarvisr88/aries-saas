namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public interface IWindowService
    {
        void Activate();
        void Close();
        void Hide();
        void Restore();
        void SetWindowState(WindowState state);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        void Show(string documentType, object viewModel, object parameter, object parentViewModel);

        string Title { get; set; }

        bool IsWindowAlive { get; }
    }
}

