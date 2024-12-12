namespace DevExpress.Mvvm
{
    using System;
    using System.Windows;

    public interface ICurrentWindowService
    {
        void Activate();
        void Close();
        void Hide();
        void SetWindowState(WindowState state);
        void Show();
    }
}

