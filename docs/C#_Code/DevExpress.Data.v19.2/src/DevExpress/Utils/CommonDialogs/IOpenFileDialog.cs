namespace DevExpress.Utils.CommonDialogs
{
    using System;

    public interface IOpenFileDialog : IFileDialog, ICommonDialog, IDisposable
    {
        bool Multiselect { get; set; }

        string SafeFileName { get; }

        string[] SafeFileNames { get; }

        bool ShowReadOnly { get; set; }

        bool ReadOnlyChecked { get; set; }
    }
}

