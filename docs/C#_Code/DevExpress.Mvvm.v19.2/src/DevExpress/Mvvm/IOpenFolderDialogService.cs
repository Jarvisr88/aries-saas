namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;

    public interface IOpenFolderDialogService
    {
        bool ShowDialog(Action<CancelEventArgs> fileOK, string directoryName);

        IFolderInfo Folder { get; }

        IEnumerable<IFolderInfo> Folders { get; }

        string Title { get; set; }
    }
}

