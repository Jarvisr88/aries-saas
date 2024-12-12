namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;

    public interface IOpenFileDialogService
    {
        bool ShowDialog(Action<CancelEventArgs> fileOK, string directoryName);

        IFileInfo File { get; }

        IEnumerable<IFileInfo> Files { get; }

        string Filter { get; set; }

        int FilterIndex { get; set; }

        string Title { get; set; }
    }
}

