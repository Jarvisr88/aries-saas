namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface IWindowSurrogate
    {
        event EventHandler Activated;

        event EventHandler Closed;

        event CancelEventHandler Closing;

        event EventHandler Deactivated;

        bool Activate();
        void Close();
        void Hide();
        void Show();
        bool? ShowDialog();

        Window RealWindow { get; }
    }
}

