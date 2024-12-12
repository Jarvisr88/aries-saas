namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public interface IMessageBoxButtonLocalizer
    {
        string Localize(MessageBoxResult button);
    }
}

