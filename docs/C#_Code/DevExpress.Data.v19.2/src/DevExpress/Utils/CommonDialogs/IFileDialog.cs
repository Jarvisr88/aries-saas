namespace DevExpress.Utils.CommonDialogs
{
    using DevExpress.Utils.CommonDialogs.Internal;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public interface IFileDialog : ICommonDialog, IDisposable
    {
        event CancelEventHandler FileOk;

        bool AddExtension { get; set; }

        bool CheckFileExists { get; set; }

        bool CheckPathExists { get; set; }

        FileDialogCustomPlacesCollection CustomPlaces { get; }

        string DefaultExt { get; set; }

        bool DereferenceLinks { get; set; }

        string FileName { get; set; }

        string[] FileNames { get; }

        string Filter { get; set; }

        int FilterIndex { get; set; }

        string InitialDirectory { get; set; }

        bool RestoreDirectory { get; set; }

        bool ShowHelp { get; set; }

        bool SupportMultiDottedExtensions { get; set; }

        string Title { get; set; }

        bool ValidateNames { get; set; }
    }
}

