namespace DevExpress.Mvvm
{
    using System;

    public interface ISaveFileDialogService
    {
        bool ShowDialog(Action<CancelEventArgs> fileOK, string directoryName, string fileName);

        string Filter { get; set; }

        int FilterIndex { get; set; }

        string DefaultExt { get; set; }

        string DefaultFileName { get; set; }

        IFileInfo File { get; }

        string Title { get; set; }
    }
}

