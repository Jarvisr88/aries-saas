namespace DevExpress.Xpf.Core.ThemeEditor.Interop
{
    using System;
    using System.Windows.Controls;

    public interface IPreview
    {
        string Header { get; }

        UserControl Control { get; }
    }
}

