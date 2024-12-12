namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public interface IColumnChooser
    {
        void ApplyState(IColumnChooserState state);
        void Destroy();
        void Hide();
        void SaveState(IColumnChooserState state);
        void Show();

        UIElement TopContainer { get; }
    }
}

